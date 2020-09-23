import React, { useState, useEffect } from 'react';
import Banners from '../../atom/comps/Banners';

import { makeStyles, Container } from '@material-ui/core';
import productSrv from '../../services/productSrv';
import notificationSrv from '../../services/notificationSrv';
import Items from './comps/items';

const useStyles = makeStyles({
    storePage: {
        paddingTop: 7.5,
        paddingBottom: 7.5
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
});

const Store = () => {
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items1, setItems1] = useState([]);
    const [items2, setItems2] = useState([]);
    const [query, setQuery] = useState('');

    useEffect(() => {
        const getDate = async () => {
            setInProgress(true);
            let get = await productSrv.get();
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
        }
        const requestNotification = async () => {
            console.log(process.env.NODE_ENV);
            let res = await notificationSrv.requestPermission();
            console.log(res);
        }
        requestNotification();
        getDate();
    }, [query]);

    return (
        <div id='page-store' className={classes.storePage}>
            <Banners pageName="Store" location="top" />
            <Container className={classes.products}>
                <div className={`r-col ${classes.col2}`}>
                    {<Items items={items1} inProgress={inProgress} />}
                </div>
                <div className={`l-col ${classes.col2}`}>
                    {<Items items={items2} inProgress={inProgress} />}
                </div>
            </Container>
            <Banners pageName="Store" location="bottom" />

        </div>
    );
};

export default Store;