import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Container } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import transactionSrv from '../../services/transactionSrv';
import toastState from '../../atom/state/toastState';
import Item from './comps/item';
import strings from '../../core/strings';
import EmptyRecord from '../../atom/comps/EmptyRecord';
import bLAtom from '../../atom/state/bLState';
import { useSetRecoilState } from 'recoil';

const useStyles = makeStyles({
    root: {
        overflowY: 'auto',
        maxHeight: 'calc(100vh - 50px)',
        boxSizing: 'border-box',
        '& .items-wrapper': {
            margin: 0,
            padding: 0,
            listStyle: 'none'
        }
    },
    loaderItem: {
        margin: 10,
        height: 117
    }
});

const Notifications = () => {
    //Hooks
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [isBottom, setIsBottom] = useState(true);
    const [expanded, setExpanded] = React.useState(false);
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);
    const setBLState = useSetRecoilState(bLAtom);
    
    useEffect(() => {
        setBLState((state) => ({ ...state, title: strings.transactions }));
        if (isBottom && (items.length === 0 || items.length > 12)) {
            const getDate = async () => {
                setInProgress(true);
                let get = await transactionSrv.get(12, pageNumber);
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
        let element = e.target;
        if (!inProgress && element.scrollHeight - element.scrollTop === element.clientHeight) {
            setIsBottom(true);
        }
    }

    return (
        <div className={classes.root} onScroll={handleScroll}>
            {!inProgress && items.length === 0 ? <EmptyRecord text={strings.thereIsNoTransaction} /> : null}
            <ul className='items-wrapper'>{items.map((item, idx) => <Item key={idx} item={item} />)}</ul>
            {(inProgress && pageNumber === 1) ? [0, 1, 2].map((x, idx) => <Container key={idx} className={classes.loaderItem}><Skeleton variant='rect' className={classes.loaderItem} /></Container>) : null}
        </div>

    );
};

export default Notifications;