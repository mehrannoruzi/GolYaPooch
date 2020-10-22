import React, { useState, useEffect } from 'react';
import { BsPlus } from 'react-icons/bs';
import { useRecoilState } from 'recoil';
import bLState from '../../atom/state/bLState';
import { useSetRecoilState } from 'recoil';
import { makeStyles, Container, Fab } from '@material-ui/core';
import toastState from '../../atom/state/toastState';
import strings from './../../core/strings';
import ticketSrv from '../../services/ticketSrv';
import EmptyRecord from '../../atom/comps/EmptyRecord';
import Item from './comps/ticketListItem';
import { Skeleton } from '@material-ui/lab';
import { useHistory } from 'react-router-dom';

const useStyles = makeStyles({
    root: {
        padding: '7.5px 10px',
        maxHeight: 'calc(100vh - 50px)',
        boxSizing: 'border-box',
        overflowY: 'auto'
    },
    loaderItem: {
        width: '100%',
        margin: '10px 0',
        display: 'flex',
        flexDirection: 'row',
        alignItems: 'center'
    },
    btnAdd: {
        position: 'fixed',
        bottom: '15px',
        right: '15px',
        '& svg': {
            fontSize: '20px'
        }
    }
});

const TicketList = () => {
    const classes = useStyles();
    const history = useHistory();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [isBottom, setIsBottom] = useState(true);
    const setBLState = useSetRecoilState(bLState);

    //recoil
    const [toast, setToastState] = useRecoilState(toastState);

    useEffect(() => {
        setBLState((state) => ({ ...state, title: strings.tickets }));
        if (isBottom && (items.length === 0 || items.length > 10)) {
            const getDate = async () => {
                setInProgress(true);
                let get = await ticketSrv.get(12, pageNumber);
                console.log(get);
                if (get.isSuccessful) {
                    setItems([...items, ...get.result.items]);
                    if (get.result.items.length > 0)
                        setPageNumber(pageNumber + 1);
                }
                else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
                setInProgress(false);
                setIsBottom(false);
            }
            getDate();
        }

    }, [isBottom]);

    function _handleScroll(e) {
        let element = e.target;
        if (!inProgress && element.scrollHeight - element.scrollTop === element.clientHeight) {
            setIsBottom(true);
        }
    }

    return (
        <div className={classes.root} onScroll={_handleScroll}>
            {!inProgress && items.length === 0 ? <EmptyRecord text={strings.thereIsNoTicket} /> : null}
            {items.map((item, idx) => <Item key={idx} item={item} />)}
            {(inProgress && pageNumber === 1) ? [0, 1, 2, 3, 4, 5, 6, 7, 9, 10].map((x, idx) => <Container key={idx} className={classes.loaderItem}>
                <Skeleton className='w-100 ' height={100} />
            </Container>) : null}
            <Fab className={classes.btnAdd} color="primary" aria-label="add" onClick={() => history.push('/bl/addTicket?add=true')}>
                <BsPlus />
            </Fab>
        </div>
    );
};

export default TicketList