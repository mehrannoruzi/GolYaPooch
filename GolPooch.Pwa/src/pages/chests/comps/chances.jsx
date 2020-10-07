import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Box } from '@material-ui/core';
import Slider from "react-slick";
import chestAtom from '../../../atom/state/chestState';
import chanceSrv from '../../../services/chanceSrv';
import { useRecoilState } from 'recoil';
import toastState from '../../../atom/state/toastState';
import { useHistory } from 'react-router-dom';
import { BsCheckCircle } from 'react-icons/bs';
import fullBottomUpModalState from '../../../atom/state/fullBottomUpModalState';

const useStyles = makeStyles({
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
    loaderChance: {
        padding: 10,
        with: 170
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
        let get = await chanceSrv.getPurchase(12, pageNumber);
        if (get.isSuccessful) {
            if (get.result.items.length === 0 && pageNumber === 1) {
                setModalState({ ...modal, open: false });
                history.push('/nl/store');
            }
            else {
                setChestState({ ...chestState, purchaseId: get.result.items[0].purchaseId });
                setItems([...items, ...get.result.items]);
                if (get.result.items.length > 0)
                    setPageNumber(pageNumber + 1);
            }
        }
        else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
        setInProgress(false);
    }

    useEffect(() => {
        getChances();
    }, []);

    const _handleSelect = (id) => {
        console.log(id);
        setChestState({ ...chestState, gatewatId: id });
    }

    const settings = {
        infinite: false,
        slidesToShow: 2,
        // swipeToSlide: true,
        className: "center mb-15",
        infinite: false,
        rtl: true,
        afterChange: function (index) {
            if (index + 2 >= items.length)
                getChances();
        }
    };
    return (
        <Slider {...settings}>
            {items.map((item, idx) => <Box className={classes.chance} key={idx} onClick={() => _handleSelect(item.purchaseId)}>
                <figure className={item.purchaseId === chestState.purchaseId ? 'selected' : null}>
                    {item.purchaseId === chestState.purchaseId ? <BsCheckCircle /> : null}
                    <img className='img-main' src={item.productOffer.imageUrl} alt={item.productOffer.product.text} />
                    <figcaption>
                        <label className='total'>تعداد کل شانس: {item.chance}</label>
                        <label className='remained'>شانس باقیمانده: {(item.chance - item.usedChance)}</label>
                    </figcaption>
                </figure>
            </Box>)}
            {inProgress ? [0, 1, 2].map((x, idx) => <div key={idx} className={classes.loaderChance}>
                <Skeleton variant='rect' className='w-100' height={320} />
            </div>) : null}
        </Slider>
    );
}
export default Chances;