import React, { useState,useEffect } from 'react';
import { useRecoilState } from 'recoil';
import { useHistory } from 'react-router-dom';
import { makeStyles, Container} from '@material-ui/core';
import { Alert, AlertTitle } from '@material-ui/lab';
import Button from '../../../atom/comps/Button';
import fullBottomUpModalAtom from '../../../atom/state/fullBottomUpModalState';
import Items from '../../store/comps/items';
import productSrv from '../../../services/productSrv';
import toastState from '../../../atom/state/toastState';

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
    }
});

export default function (props) {
    //Hooks
    const classes = useStyles();
    const history = useHistory();
    const [inProgress, setInProgress] = useState(true);
    const [items1, setItems1] = useState([]);
    const [items2, setItems2] = useState([]);
    //Recoil
    const [modal, setModalState] = useRecoilState(fullBottomUpModalAtom);
    const [toast, setToastState] = useRecoilState(toastState);

    useEffect(() => {
        const getData = async () => {
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
            else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
        }
        getData();
    }, []);

    const _handleClick = () => {
        setModalState({ ...modal, open: false, children: null });
        history.push('/nl/store');
    }

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
            {/* <Box component='div' className='btn-wrapper'>
                <Button className='btn-purchase' onClick={_handleClick}>برو به فروشگاه</Button>
            </Box> */}
            <Container className={classes.products}>
                <div className={`r-col ${classes.col2}`}>
                    {<Items items={items1} inProgress={inProgress} />}
                </div>
                <div className={`l-col ${classes.col2}`}>
                    {<Items items={items2} inProgress={inProgress} />}
                </div>
            </Container>
        </Container>
    </div>)
}