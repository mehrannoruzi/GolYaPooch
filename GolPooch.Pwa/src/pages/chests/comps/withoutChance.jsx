import React, { useState, useEffect } from 'react';
import { useRecoilState } from 'recoil';
import { useHistory } from 'react-router-dom';
import { makeStyles, Container } from '@material-ui/core';
import { Alert, AlertTitle } from '@material-ui/lab';
import fullBottomUpModalAtom from '../../../atom/state/fullBottomUpModalState';
import productSrv from '../../../services/productSrv';
import { Skeleton } from '@material-ui/lab';
import toastState from '../../../atom/state/toastState';
import Slider from "react-slick";
import Product from './../../store/comps/product';
const useStyles = makeStyles({
    withoutChanceComp: {
        '& .heading': {
            textAlign: 'center'
        },
        '& .MuiAlert-message': {
            flexDirection: 'column',
            lineHeight: '20px',
            alignItems: 'flex-start',
            '& p': {
                textAlign: 'justify',
            }
        },
        '& .btn-wrapper': {
            textAlign: 'center',
            padding: '10px 0',
            position: 'absolute',
            bottom: 0,
            left: 0,
            right: 0,
            border: '1px solid #eee',
            borderRadius: 0,
        }
    },
    product: {
        padding: '0 5px',
        boxSizing: 'border-box'
    }
});

export default function (props) {
    //Hooks
    const classes = useStyles();
    const history = useHistory();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    //Recoil
    const [modal, setModalState] = useRecoilState(fullBottomUpModalAtom);
    const [toast, setToastState] = useRecoilState(toastState);

    useEffect(() => {
        const getData = async () => {
            setInProgress(true);
            let get = await productSrv.get();
            setInProgress(false);
            if (get.isSuccessful) {
                setItems(get.result);

            }
            else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
        }
        getData();
    }, []);


    const settings = {
        slidesToShow: 2,
        slidesToScroll: 1,
        autoplay: true,
        autoplaySpeed: 3000,
        rtl: true
    };
    return (<div className={classes.withoutChanceComp}>
        <Container>
            <h4 className='heading'>
                <Alert severity="warning">
                    <AlertTitle>کاربر گرامی</AlertTitle>
                    <p>
                        شما شانسی برای شرکت در قرعه کشی ندارید برای بدست آوردن شانس قرعه کشی یکی از محصولات ما را خرید کنید
                    </p>
                </Alert>
            </h4>
            <Slider {...settings}  infinite={items.length > 1}>
                {inProgress ? [0, 1, 2].map((x, idx) => <Skeleton key={idx} variant='rect' className='w-100 mb-15' height={110} />) :
                    items.map((item, idx) => <div className={classes.product} key={idx}><Product item={item} /></div>)}
            </Slider>
        </Container>
    </div>)
}