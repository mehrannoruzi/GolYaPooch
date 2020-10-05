import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Container } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import notificationSrv from '../../services/notificationSrv';
import toastState from '../../atom/state/toastState';
import Item from './comps/item';

const useStyles = makeStyles({
    notificationsComp: {
        overflowY: 'auto',
        maxHeight: 'calc(100vh - 50px)'
    },
    inline: {
        display: 'inline',
    },
    loaderItem: {
        display: 'flex',
        alignItems: 'center',
        marginBottom: 15,
        '& .subject': {
            margin: '0 10px',
            width: '100%'
        }
    }
});

const Notifications = () => {
    //Hooks
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [isBottom, setIsBottom] = useState(true);
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);


    function handleScroll(e) {
        let element = e.target
        if (element.scrollHeight - element.scrollTop === element.clientHeight) {
            setIsBottom(true);
        }

    }

    useEffect(() => {
        if (isBottom && (items.length === 0 || items.length > 10)) {
            const getDate = async () => {
                setInProgress(true);
                let get = await notificationSrv.get(12, pageNumber);
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

    return (
        <div id='comp-notifications' className={classes.notificationsComp} onScroll={handleScroll}>
            {items.map((item, idx) => <Item key={idx} item={item} />)}
            {(inProgress && pageNumber === 1) ? [0, 1, 2].map((x, idx) => <Container key={idx} className={classes.loaderItem}>
                <Skeleton variant='rect' height={36} width={36} className='avatar' />
                <Skeleton className='subject' /></Container>) : null}
        </div>

    );
};

export default Notifications;