import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Container } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import ticketSrv from '../../services/ticketSrv';
import toastState from '../../atom/state/toastState';
import Item from './comps/item';
import EmptyRecord from '../../atom/comps/EmptyRecord';
import strings from '../../core/strings';

const useStyles = makeStyles({
    ticketsComp: {
        boxSizing: 'border-box',
        paddingBottom: '20px',
        marginTop: 20,
        maxHeight: 'calc(100vh - 50px)',
        overflowY: 'auto'
    },
    loaderItem: {
        display: 'flex',
        alignItems: 'center',
        marginBottom: 15,
        '& .subject': {
            margin: '0 15px',
            width: '100%',
            height: 20
        }
    }
});

const Tickets = () => {
    //Hooks
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [isBottom, setIsBottom] = useState(true);
    const [expanded, setExpanded] = React.useState(false);
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);

    useEffect(() => {
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

    function handleScroll(e) {
        let element = e.target
        if (!inProgress && element.scrollHeight - element.scrollTop === element.clientHeight) {
            setIsBottom(true);
        }
    }

    const _handleItemClick = (panel) => {
        if(panel===expanded) setExpanded(false);
        else setExpanded(panel);
    }

    return (
        <div id='comp-tickets' className={classes.ticketsComp} onScroll={handleScroll}>
            {!inProgress && items.length === 0 ? <EmptyRecord text={strings.thereIsNoTicket} /> : null}
            {items.map((item, idx) => <Item key={idx} item={item}  expanded={expanded === `panel${item.ticketId}`} onClick={_handleItemClick} />)}
            {(inProgress && pageNumber === 1) ? [0, 1, 2, 3, 4, 5, 6, 7, 9, 10].map((x, idx) => <Container key={idx} className={classes.loaderItem}>
                <Skeleton variant='rect' height={36} width={48} className='avatar' />
                <Skeleton className='subject' /></Container>) : null}
        </div>

    );
};

export default Tickets;