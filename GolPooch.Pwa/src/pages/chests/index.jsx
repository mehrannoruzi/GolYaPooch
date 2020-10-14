﻿import React, { useState, useEffect } from 'react';
import { useRecoilState } from 'recoil';
import { makeStyles, Container } from '@material-ui/core';
import chestSrv from '../../services/chestSrv';
import Banners from '../../atom/comps/Banners';
import Items from './comps/items';
import toastState from '../../atom/state/toastState';
import nLAtom from '../../atom/state/nLState';
import ticketSrv from '../..//services/ticketSrv';
import notificationSrv from '../..//services/notificationSrv';

const useStyles = makeStyles({
    chestPage: {
        paddingTop: 7.5,
        paddingBottom: 7.5,
    },
    col2: {
        width: '50%',
        display: 'inline-block',
        verticalAlign: 'top',
        boxSizing: 'border-box'
    },
    products: {
        '& .l-col': {
            paddingLeft: '7.5px'
        },
        '& .r-col': {
            paddingRight: '7.5px'
        }
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
    const [items1, setItems1] = useState([]);
    const [items2, setItems2] = useState([]);
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);
    const [nLState, setNLState] = useRecoilState(nLAtom);

    const getInitInfo = async () => {
        let getNewNotifCount = await notificationSrv.getNotReadCount();
        console.log(getNewNotifCount);
        if (getNewNotifCount.isSuccessful)
            setNLState({ ...nLState, newNotificationsCount: getNewNotifCount.result });
        let getTicketCount = await ticketSrv.getNotReadCount();
        if (getTicketCount.isSuccessful)
            setNLState({ ...nLState, newTicketCount: getTicketCount.result });
    };
    const getDate = async () => {
        setInProgress(true);
        let get = await chestSrv.get();
        setInProgress(false);
        if (get.isSuccessful) {
            let tempItems1 = [], tempItems2 = [];
            for (let i = 0; i < get.result.length; i++) {
                if (i % 2 === 0) tempItems1.push(get.result[i]);
                else tempItems2.push(get.result[i]);
            }
            setItems1(tempItems1);
            setItems2(tempItems2);
        }
        else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
    }

    useEffect(() => {
        getDate();
        getInitInfo();
    }, []);

    return (
        <div id='page-chest' className={classes.chestPage}>
            <Banners pageName="Chest" location="top" />
            <Container className={classes.products}>
                <div className={`r-col ${classes.col2}`}>
                    {<Items items={items1} inProgress={inProgress} />}
                </div>
                <div className={`l-col ${classes.col2}`}>
                    {<Items items={items2} inProgress={inProgress} />}
                </div>
            </Container>
            <Banners pageName="Chest" location="bottom" />

        </div>
    );
};

export default Chest;