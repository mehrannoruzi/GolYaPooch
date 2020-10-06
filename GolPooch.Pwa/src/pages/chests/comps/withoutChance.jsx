import React from 'react';
import { useHistory } from 'react-router-dom';
import { makeStyles, Container, Divider, Box } from '@material-ui/core';
import { Alert, AlertTitle } from '@material-ui/lab';
import Button from '../../../atom/comps/Button';
import { useRecoilState } from 'recoil';
import fullBottomUpModalAtom from '../../../atom/state/fullBottomUpModalState';

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
    }
});

export default function (props) {
    //Hooks
    const classes = useStyles();
    const history = useHistory();
    //Recoil
    const [modal, setModalState] = useRecoilState(fullBottomUpModalAtom);

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
            <Box component='div' className='btn-wrapper'>
                <Button className='btn-purchase' onClick={_handleClick}>برو به فروشگاه</Button>
            </Box>
        </Container>
    </div>)
}