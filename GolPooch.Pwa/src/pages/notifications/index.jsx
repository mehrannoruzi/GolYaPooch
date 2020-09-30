import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Container } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import notificationSrv from '../../services/notificationSrv';
import toastState from '../../atom/state/toastState';
import Item from './comps/item';

const useStyles = makeStyles({
    notificationsComp: {
        paddingTop: 7.5,
        paddingBottom: 7.5,
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


    function handleScroll() {
        const scrollTop = (document.documentElement && document.documentElement.scrollTop) ||
            document.body.scrollTop;
        const scrollHeight = (document.documentElement && document.documentElement.scrollHeight) ||
            document.body.scrollHeight;
        const clientHeight = document.documentElement.clientHeight || window.innerHeight;
        console.log(`scrollTop:${scrollTop}-scrollHeight:${scrollHeight}-clientHeight:${clientHeight}`)
        if (Math.ceil(scrollTop + clientHeight) + 10 >= scrollHeight) {
            setIsBottom(true);
        }
    }
    //infinit scroll
    useEffect(() => {
        window.addEventListener('scroll', handleScroll);
        return () => window.removeEventListener('scroll', handleScroll);
    }, []);

    useEffect(() => {
        console.log('fired');
        if (isBottom && (items.length === 0 || items.length > 10)) {
            const getDate = async () => {
                setInProgress(true);
                let get = await notificationSrv.get(12, pageNumber);
                console.log(get);
                if (get.isSuccessful) {
                    setItems([...items, ...get.result.items]);
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
        <div id='comp-notifications' className={classes.notificationsComp}>
            {items.map((item, idx) => <Item key={idx} item={item} />)}
            {inProgress ? [0, 1, 2].map((x, idx) => <Container key={idx} className={classes.loaderItem}>
                <Skeleton variant='rect' height={35} width={50} className='avatar' />
                <Skeleton className='subject' /></Container>) : null}
        </div>

    );
};

export default Notifications;