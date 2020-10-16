import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Paper, Box, CircularProgress } from '@material-ui/core';
import Slider from "react-slick";
import chestAtom from '../../../atom/state/chestState';
import purchaseSrv from '../../../services/purchaseSrv';
import { useRecoilState } from 'recoil';
import toastState from '../../../atom/state/toastState';
import { useHistory } from 'react-router-dom';
import { BsCheckCircle } from 'react-icons/bs';
import fullBottomUpModalState from '../../../atom/state/fullBottomUpModalState';
import strings from './../../../core/strings';
import Button from '../../../atom/comps/Button';
import { BiCheck } from 'react-icons/bi';

const useStyles = makeStyles({
    root: {
        position: 'relative',
        minHeight: 220
    },
    chance: {
        padding: 10,
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        boxSizing: 'border-box',

        '& .wrapper': {
            boxShadow: "0px 1px 8px 0px #d2d2d2",
            position: 'relative',
            display: 'flex',
            flexDirection: 'column',
            maxWidth: 160,
            '&.selected': {
                boxShadow: '0px 1px 8px 0px #76b132'
            },
            '& .select-icon': {
                top: '-8px',
                right: '-5px',
                color: 'green',
                fontSize: '20px',
                position: 'absolute',
                top: '-10px',
                left: '-10px',
                backgroundColor: '#fff'
            },
            '& .hx': {
                padding: '7.5px 5px',
                margin: '0 0 15px 0',
                fontSize: '11px',
                textAlign: 'center',
                borderBottom: '1px solid #ccc',
                borderRadius: '5px',
                borderBottomRightRradius: 0,
                borderBottomLeftRadius: 0,

            },
            '& .btn': {
                textAlign: 'center',
                color: '#8BC34A',
                minWidth: 120,
                marginBottom: 15,
                '& .btnPurchase': {
                    backgroundColor: '#8BC34A',
                    minWidth: 120,
                }
            },
            '& label': {
                padding: 5,
                '& .chk-icon': {
                    fontSize: '20px',
                    color: 'green',
                    verticalAlign: 'middle'
                }
            }
        }

    },
    loaderWrapper: {
        color: '#666',
        display: 'flex',
        flexDirection: 'column',
        position: 'absolute',
        left: 0,
        right: 0,
        bottom: 15,
        top: 0,
        backgroundColor: 'rgba(0,0,0,0.1)',
        alignItems: 'center',
        justifyContent: 'center',
        '& .MuiCircularProgress-root': {
            color: '#666!important'
        },
        '& span': {
            paddingTop: 10,
            fontSize: '0.9rem'
        }
    }
});

const Chances = (props) => {
    const classes = useStyles();
    //HOOKS
    const [inProgress, setInProgress] = useState(true);
    const [pageNumber, setPageNumber] = useState(1);
    const [items, setItems] = useState([]);
    const history = useHistory();
    //recoil
    const [chestState, setChestState] = useRecoilState(chestAtom);
    const [toast, setToastState] = useRecoilState(toastState);
    const [modal, setModalState] = useRecoilState(fullBottomUpModalState);

    const getChances = async () => {
        setInProgress(true);
        let get = await purchaseSrv.getActive(12, pageNumber);
        if (get.isSuccessful) {
            if (get.result.items.length === 0 && pageNumber === 1) {
                setChestState({ ...chestState, withoutChance: true });
            }
            else if (get.result.items.length > 0) {
                if (pageNumber === 1)
                    setChestState({ ...chestState, purchase: get.result.items.length === 2 ? get.result.items[1] : get.result.items[0] });
                setItems([...items, ...get.result.items]);
                //setItems([get.result.items[0],get.result.items[1]]);
                setPageNumber(pageNumber + 1);
            }
        }
        else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
        setInProgress(false);
    }

    useEffect(() => {
        getChances();
    }, []);

    const _handleSelect = (item) => {
        setChestState({ ...chestState, purchase: item, disabled: item.chance === item.usedChance });
    }

    const settings = {
        slidesToShow: 2,
        slidesToScroll: 1,
        rtl: true,
        afterChange: function (index) {
            if (index <= 2)
                getChances();
        }
    };
    return (
        <div className={classes.root}>
            {inProgress ? <div className={classes.loaderWrapper}>
                <CircularProgress size={20} />
                <span>{strings.pleaseWait}</span>
            </div> : null}
            <Slider {...settings} infinite={items.length > 1} style={{ visibility: inProgress ? 'hidden' : 'visible' }}>
                {items.map((item, idx) => <Box className={classes.chance} key={idx} onClick={() => _handleSelect(item)}>
                    <Paper className={`wrapper ${item.purchaseId === chestState.purchase.purchaseId ? 'selected' : null}`}>
                        {item.purchaseId === chestState.purchase.purchaseId ? <BsCheckCircle className='select-icon' /> : null}
                        <h4 className='hx'>{item.productOffer.product.text}</h4>
                        <div className='btn'><Button className='btnPurchase'>{strings.select}</Button></div>
                        <label className='remained'><BiCheck className='chk-icon' /> شانس باقیمانده: {(item.chance - item.usedChance)}</label>
                        <label className='total'><BiCheck className='chk-icon' /> تعداد کل شانس: {item.chance}</label>
                        <label className='exp-date'><BiCheck className='chk-icon' />انقضا: {item.expireDateSh}</label>
                    </Paper>
                </Box>)}

            </Slider>

        </div >

    );
}
export default Chances;