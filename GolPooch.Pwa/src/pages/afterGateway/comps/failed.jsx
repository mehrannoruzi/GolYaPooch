import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { Link } from 'react-router-dom';
import strings from '../../../core/strings';
import { Grid, makeStyles } from '@material-ui/core';
import { CgCloseO } from 'react-icons/cg';
import { useRecoilState } from 'recoil';
import toastState from '../../../atom/state/toastState';
import orderSrv from '../../../services/orderSrv';
import { commaThousondSeperator } from '../../../core/utils';
const useStyles = makeStyles({
    failedComp: {
        paddingTop: 15,
        paddingBottom: 15,

        '& .heading': {
            textAlign: 'center',
            '& svg': {
                fontSize: 50
            },
            '& .hx': {
                padding: '30px 0px 80px 0px',
                textAlign: 'center',
            }
        },
        '& .product': {
            padding: '0 30px',
            '& .img': {
                height: '150px',
                width: '100%'
            },
            '& .info': {
                display: 'flex',
                flexDirection: 'column',
                justifyContent: 'center',
                paddingLeft: 15
            },
            '& .price': {
                paddingTop: 50
            }
        }

    },
    linkToStore: {
        backgroundColor: '#ffffff',
        borderRadius: '3px',
        position: 'fixed',
        left: '50%',
        zIndex: 999,
        bottom: '30px',
        transform: 'translateX(-50%)',
        boxShadow: '-1px 2px 2px 2px rgba(0,0,0,0.5)',
        padding: '10px 20px'

    },
    traceId: {
        paddingBottom: '50px'
    }
});

const Failed = (props) => {
    //HOOKS
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [product, setProduct] = useState(null);
    const [query, setQuery] = useState('');
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);

    useEffect(() => {
        const getDate = async () => {
            setInProgress(true);
            let info = await orderSrv.getProductInfo();
            console.log(info);
            if (info.isSuccessful) {
                setProduct(info.result);
            }
            else setToastState({ ...toast, open: true, severity: 'error', message: info.message });
            setInProgress(false);
        }
        getDate();
    }, [query]);

    return (
        <Grid container className={classes.failedComp}>
            <Grid xs={12} item className='heading'>
                <CgCloseO />
                <h3 className='hx'>{strings.failedPayment}</h3>
            </Grid>
            <Grid xs={12} item className='product mb-15'>
                <Grid container>
                    <Grid item xs={12}>
                        <h4>جزییات خرید</h4>
                    </Grid>
                    <Grid xs={4} item className='pic'>
                        {inProgress ? <Skeleton className='img' animation='wave' variant='rect' heioght={100} /> : <img className='img' src={product.imageUrl} />}
                    </Grid>
                    <Grid xs={5} item className='info'>
                        {inProgress ? [0, 1].map((x, idx) => <Skeleton key={idx} className='w-100 mb-15' />) : <><label className='mb-15'>{product.name}</label>
                            <label>تعداد شانس: {product.chance}</label></>}
                    </Grid>
                    <Grid item xs={12} className='price'>
                        {inProgress ? <Skeleton variant='rect' height={20} /> : <label>مبلغ قابل پرداخت: {commaThousondSeperator(product.price)} {strings.moneyCurrency}</label>}
                    </Grid>
                </Grid>
            </Grid>
            <Link to='/nl/chest' className={classes.linkToStore}>پرداخت مجدد</Link>
        </Grid>
    );
}
export default Failed;