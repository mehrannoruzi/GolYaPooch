import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Container, Grid } from '@material-ui/core';
import strings from '../../core/strings';
import { useHistory, useParams } from "react-router-dom";
import { useRecoilState } from 'recoil';
import productSrv from '../../services/productSrv';
import { commaThousondSeperator } from '../../core/utils';
import Button from '../../atom/comps/Button';
import toastState from '../../atom/state/toastState';
import Gateways from './comps/gateways';
import productAtom from '../../atom/state/productState';
import bLState from '../../atom/state/bLState';
import { useSetRecoilState } from 'recoil';

const useStyles = makeStyles({
    productPage: {
        position: 'relative',
        paddingTop: '15px',
        '& .img-main': {
            width: '100%',
            height: 200
        },
        '& .info-wrapper': {
            display: 'flex',
            padding: '15px 15px 0 0',

            '& .info': {
                width: '100%',
                display: 'flex',
                flexDirection: 'column',
                color: '#666',

                '& label': {
                    padding: '7px 0'
                },
                '& .name': {
                    fontWeight: 800,
                    fontSize: '1.1rem',
                    padding: '15px 0',
                },
                '& .discount > span': {
                    color: '#fff',
                    backgroundColor: 'red',
                    borderRadius: '10px',
                    padding: '5px'
                },
                '& .price': {
                    fontWeight: 800,
                    fontSize: '1.3rem'
                }
            }
        },
        '& .btn-wrapper': {
            display: 'flex',
            justifyContent: 'flex-end',
            marginTop: 30,
            '& .btn-purchase': {
                minWidth: '150px'
            }
        },

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

const Product = () => {
    const classes = useStyles();
    let history = useHistory();
    let { id } = useParams();
    //HOOKS
    const [item, setProduct] = useState({});
    const [gateways, setGateways] = useState([]);
    const [query, setQuery] = useState('');
    const [inProgress, setInProgress] = useState(true);
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);
    const [rState, setProductState] = useRecoilState(productAtom);
    const setTitle = useSetRecoilState(bLState);

    const getDate = async () => {
        setInProgress(true);
        let get = await productSrv.getSingle(parseInt(id));
        if (get.isSuccessful) setProduct(get.result);
        else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
        setInProgress(false);
    }

    useEffect(() => {
        getDate();
        setTitle({ title: strings.detailProduct });
    }, [setQuery]);

    const _handlePurchase = () => {
        console.log(rState.gatewatId)
    }

    return (
        <div id='page-product' className={classes.productPage}>
            <Container>
                <Grid container className='mb-15'>
                    <Grid item xs={6} className='info-wrapper'>
                        {inProgress ? <Skeleton variant='rect' className='w-100' height={300} animation="wave" /> :
                            <img className='img-main' src={item.imageUrl} alt={item.product.text} />}
                    </Grid>
                    <Grid item xs={6} className='info-wrapper'>
                        {inProgress ? <div className='info'>
                            {[0, 1, 2].map((x, idx) => <Skeleton key={idx} className='w-100 mb-15' />)}
                        </div> : <div className='info'>
                                <label className='name'>{item.product.text}</label>
                                <label className='chance'>{item.chance} {strings.chance}</label>
                                {item.discount > 0 ? <label className='discount'><span>{item.discount}% {strings.discount}</span></label> : null}
                                {item.discount > 0 ? <label className='profit'><span>{strings.yourProfit} : {commaThousondSeperator(item.profit)}{strings.moneyCurrency}</span></label> : null}
                                <label className='price'>
                                    {item.discount > 0 ? <span className={classes.rawPrice}>{commaThousondSeperator(item.price)}</span> : null}
                                    {commaThousondSeperator(item.totalPrice)}{strings.moneyCurrency}</label>
                            </div>}
                    </Grid>
                </Grid>
                <Grid container>
                    <Grid item xs={12}>
                        {inProgress ? [0, 1, 2].map((x, idx) => <Skeleton key={idx} className='w-100' />) : <p>{`در صورتی که شانس خود را به تعداد ${item.unUseDay} روز در سبد های قرعه کشی مصرف نکنید، مبلغ ${commaThousondSeperator(item.totalPrice)} تومن حساب شما شارژ خواهد شد`}</p>}
                    </Grid>
                    <Grid item xs={12}>
                        <Gateways />
                    </Grid>

                </Grid>
            </Container>
            <div className='btn-wrapper btn-BottomFixed'>
                <Button disabled={inProgress} className='btn-purchase' onClick={_handlePurchase}>{strings.purchase}</Button>
            </div>
        </div>
    );
};

export default Product;