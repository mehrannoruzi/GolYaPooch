import React, { useState, useEffect } from 'react';
import { useRecoilState } from 'recoil';
import { makeStyles, Container, Grid } from '@material-ui/core';
import { Skeleton } from '@material-ui/lab';
import chestSrv from '../../services/chestSrv';
import Banners from '../../atom/comps/Banners';
import Item from './comps/item';
import toastState from '../../atom/state/toastState';
import nLAtom from '../../atom/state/nLState';
import ticketSrv from '../..//services/ticketSrv';
import notificationSrv from '../..//services/notificationSrv';

const useStyles = makeStyles({
    chestPage: {
        paddingTop: 15,
        paddingBottom: 15,
    },
    rawPrice: {
        position: 'relative',
        marginRight: 10,
        '&:after': {
            content: "''",
            backgroundColor: 'red',
            position: 'absolute',
            height: '1px',
            left: 0,
            right: 0,
            top: '50%',
            transform: 'translateY(-50%)'
        }
    }
});

const Chest = () => {
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);
    const [nLState, setNLState] = useRecoilState(nLAtom);

    const getInitInfo = async () => {
        let getNotifCount = await notificationSrv.getNotReadCount();
        if (!getNotifCount.isSuccessful) return;
        let getTicketCount = await ticketSrv.getNotReadCount();
        if (!getTicketCount.isSuccessful) return;
        setNLState({ ...nLState, newNotificationsCount: getNotifCount.result, newTicketCount: getTicketCount.result });
    };
    const getDate = async () => {
        setInProgress(true);
        let get = await chestSrv.get();
        setInProgress(false);
        if (get.isSuccessful) setItems(get.result);
        else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
    }
    const requestNotification = async () => {
        let res = await notificationSrv.requestPermission();
    }
    useEffect(() => {
        getDate();
        requestNotification();
        getInitInfo();
    }, []);

    return (
        <div id='page-chest' className={classes.chestPage}>
            <Banners pageName="Chest" location="top" />
            <Container className={classes.products}>
                <Grid container>
                    {inProgress ? [0, 1, 2].map((x, idx) => <Grid item xs={12} key={idx} className='loader mb-15'>
                        <Skeleton variant='rect' className='w-100 mb-15' height={150} />
                    </Grid>) : items.map((x, idx) => <Item key={idx} item={x} />)}
                </Grid>
            </Container>
            <Banners pageName="Chest" location="bottom" />

        </div>
    );
};

export default Chest;