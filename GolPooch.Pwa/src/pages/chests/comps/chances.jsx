import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Box, CircularProgress } from '@material-ui/core';
import Slider from "react-slick";
import chestAtom from '../../../atom/state/chestState';
import purchaseSrv from '../../../services/purchaseSrv';
import { useRecoilState } from 'recoil';
import toastState from '../../../atom/state/toastState';
import { useHistory } from 'react-router-dom';
import { BsCheckCircle } from 'react-icons/bs';
import fullBottomUpModalState from '../../../atom/state/fullBottomUpModalState';

const useStyles = makeStyles({
    root: {
        position: 'relative',
        minHeight: 295
    },
    chance: {
        padding: 10,
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        boxSizing: 'border-box',
        '& figure': {
            margin: 0,
            width: '100%',
            boxSizing: 'border-box',
            border: 'solid 1px #ccc',
            padding: '12px',
            borderRadius: '3px',
            position: 'relative',
            '&.selected': {
                color: 'green',
                border: 'solid 1px green',
                boxShadow: '0px 0px 3px 0px #8BC34A'
            },
            '& svg': {
                top: '-8px',
                right: '-5px',
                color: 'green',
                fontSize: '20px',
                position: 'absolute',
                top: '-10px',
                left: '-10px',
                backgroundColor: '#fff'
            },
            '& .img-main': {
                width: '100%',
                maxHeight: '180px',
                marginBottom: 5
            },
            '& figcaption': {
                display: 'flex',
                flexDirection: 'column',
                '& label': {
                    padding: '5px 0',
                    fontSize: '1.1rem'
                }
            }
        }

    },
    loaderWrapper: {
        display: 'flex',
        position: 'absolute',
        left: 0,
        right: 0,
        bottom: 15,
        top: 0,
        backgroundColor: 'rgba(0,0,0,0.1)',
        alignItems: 'center',
        justifyContent: 'center'
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
        console.log('chances-------');
        console.log(get);
        if (get.isSuccessful) {
            if (get.result.items.length === 0 && pageNumber === 1) {
                setModalState({ ...modal, open: false });
                history.push('/nl/store');
            }
            else if (get.result.items.length > 0) {
                if (pageNumber === 1)
                    setChestState({ ...chestState, purchase: get.result.items[0] });
                setItems([...items, ...get.result.items]);
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
        slidesToScroll: 2,
        className: "center mb-15",
        infinite: true,
        rtl: true,
        afterChange: function (index) {
            if (index <= 2)
                getChances();
        }
    };
    return (
        <div className={classes.root}>
            {inProgress ? <div className={classes.loaderWrapper}>
                <CircularProgress size={30} />
            </div> : null}
            <Slider {...settings} style={{ visibility: inProgress ? 'hidden' : 'visible' }}>
                {items.map((item, idx) => <Box className={classes.chance} key={idx} onClick={() => _handleSelect(item)}>
                    <figure className={item.purchaseId === chestState.purchaseId ? 'selected' : null}>
                        {item.purchaseId === chestState.purchase.purchaseId ? <BsCheckCircle /> : null}
                        <img className='img-main' src={item.productOffer.imageUrl} alt={item.productOffer.product.text} />
                        <figcaption>
                            <label className='total'>تعداد کل شانس: {item.chance}</label>
                            <label className='remained'>شانس باقیمانده: {(item.chance - item.usedChance)}</label>
                        </figcaption>
                    </figure>
                </Box>)}

            </Slider>

        </div>

    );
}
export default Chances;