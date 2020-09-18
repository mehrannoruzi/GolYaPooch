import React, { useState, useEffect } from 'react';
import Banners from '../../atom/comps/Banners';
import { Skeleton } from '@material-ui/lab';
import { Button, makeStyles, Paper, Container, Grid, Typography } from '@material-ui/core';
import strings from '../../core/strings';
import productSrv from '../../services/productSrv';

const useStyles = makeStyles({
    product: {
        marginBottom: 15,
        '& .MuiPaper-root': {
            display: 'flex',
            flexDirection: 'column',
            padding: '0 15px',

            '& .name': {
                textAlign:'center',
                fontWeight: 800,
                padding: '15px 0',
            }
        }
    },
});

const Store = () => {
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    const [query, setQuery] = useState('');

    useEffect(() => {
        const getDate = async () => {
            setInProgress(true);
            let get = await productSrv.get();
            setInProgress(false);
            if (get.isSuccessful) {
                setItems(get.result);
            }
        }
        getDate();
    }, [query]);

    const _handleClick = () => {

    }
    return (
        <div id='page-store'>
            <Banners pageName="Store" location="top" />
            <Container>
                <Grid container spacing={0}>
                    {inProgress ? [0, 1, 2].map((x, idx) => <Grid key={idx} item xs={12}><Skeleton variant='rect' className='w-100 mb-15' height={200} /></Grid>) :
                        items.map((item, idx) => <Grid key={idx} item xs={12} sm={6} md={4} className={classes.product}>
                            <Paper onClick={_handleClick}>
                                <label className='name'>{item.product.text}</label>
                                <label className='chance'>شانس برنده شدن: {item.chance}</label>
                                <label className='discount'>تخفیف: {item.discount}{strings.moneyCurrency}</label>
                                <label className='price'>قیمت: {item.totalPrice}{strings.moneyCurrency}</label>
                            </Paper>
                        </Grid>)}
                </Grid>
            </Container>
            <Banners pageName="Store" location="bottom" />

        </div>
    );
};

export default Store;