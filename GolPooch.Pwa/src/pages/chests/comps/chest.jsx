import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Container, Grid, Divider, Paper } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import toastState from '../../../atom/state/toastState';
import Chances from './chances';
import Button from '../../../atom/comps/Button';
import strings from '../../../core/strings';
import Heading from '../../../atom/comps/Heading';
import chestSrv from '../../../services/chestSrv';
import chestState from '../../../atom/state/chestState';
import Agreed from './agreed';
import WithoutChance from './withoutChance';
import fullBottomUpModalState from '../../../atom/state/fullBottomUpModalState';

const useStyles = makeStyles({
    chestComp: {
        overflowY: 'auto',

        '& .heading': {
            textAlign: 'center'
        },
        '& .total-chance': {
            display: 'flex',
            justifyContent: 'center',
            fontWeight: 800,
            fontSize: '1.5rem',
            margin: '0 0 15px 0'
        },
        '& .btns': {
            padding: '10px 0',
            position: 'absolute',
            bottom: 0,
            left: 0,
            right: 0,
            border: '1px solid #eee',
            borderRadius: 0,

            '& button': {
                padding: '10px',
                textAlign: 'center',
                fontSize: '1.2rem',
                width: '130px'
            },
            '& .reject': {
                display: 'flex',
                justifyContent: 'flex-end',
                '& button': {
                    backgroundColor: 'red'
                }
            }
        }
    },
    counter: {
        display: 'flex',
        '& *': {
            width: '30px',
            height: '30px',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            fontSize: '20px'
        },
        '& .btn-minus,& .btn-plus':
        {
            border: 'solid 1px #ccc',
            borderRadius: '50%'
        }
    }
});

const Chest = (props) => {
    //Hooks
    const classes = useStyles();
    const [spendChanceResult, setSpendChanceResult] = useState(null);
    const [inProgress, setInProgress] = useState(true);
    const [sending, setIsSending] = useState(false);
    const [item, setItem] = useState(null);
    const [count, setCount] = useState(1);
    const [totalChance, setTotalChance] = useState(0);
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);
    const [modal, setModalState] = useRecoilState(fullBottomUpModalState);
    const [rState, setRState] = useRecoilState(chestState);
    useEffect(() => {
        const getData = async () => {
            setInProgress(true);
            let get = await chestSrv.getSingle(parseInt(props.id));
            if (get.isSuccessful) {
                setItem(get.result);
                let getChance = await chestSrv.getChance(props.id);
                if (getChance.isSuccessful) setTotalChance(getChance.result);
                else setToastState({ ...toast, open: true, severity: 'error', message: getChance.message });
            }
            else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
            setInProgress(false);
        }
        getData();
    }, []);

    const _handleSpendChance = async () => {
        setIsSending(true);
        let call = await chestSrv.spendChance({
            chestId: props.id,
            purchaseId: rState.purchaseId,
            ChanceCount: count
        });
        if (call.isSuccessful) setSpendChanceResult(call.result);
        else setToastState({ ...toast, open: true, severity: 'error', message: call.message });
        setIsSending(false);
    }
    const _handleCount = (newCount) => {
        console.log(item);
        if (newCount <= 0) return;
        if (newCount > totalChance) return;
        setCount(newCount);
    }
    if (spendChanceResult) return <Agreed info={spendChanceResult} />;
    return (
        <div id='comp-chest' className={classes.chestComp}>
            <Container>
                <h4 className='heading'>
                    {inProgress ? <Skeleton className='w-100' /> : 'میزان شانس شما در این قرعه کشی'}
                </h4>
                <h5 className='total-chance'>
                    {inProgress ? <Skeleton width={100} /> : <span>{totalChance}</span>}
                </h5>
            </Container>
            <Divider component="div" />
            {rState.withoutChance ? <WithoutChance /> : <>
                <Container>
                    <Grid container>
                        <Heading>برای افزایش شانس بسته خود را انتخاب کنید</Heading>
                    </Grid>
                    <Chances />
                    <div className={classes.counter}>
                        {inProgress ? <Skeleton className='w-100' variant='rect' height={30} width={120} /> : <>
                            <button className='btn-plus' onClick={() => _handleCount(count + 1)}>+</button>
                            <span className='count'>{count}</span>
                            <button className='btn-minus' onClick={() => _handleCount(count - 1)}>-</button>
                        </>}
                    </div>
                </Container>
                <Paper className='btns'>
                    <Container>
                        <Grid container>
                            <Grid item xs={6} className='agree'>
                                <Button loading={sending} disabled={inProgress} onClick={() => _handleSpendChance()}>{strings.iAgree}</Button>
                            </Grid>
                            <Grid item xs={6} className='reject'>
                                <Button onClick={() => setModalState({ ...modal, open: false })} disabled={inProgress}>{strings.iReject}</Button>
                            </Grid>
                        </Grid>
                    </Container>
                </Paper>
            </>}

        </div>
    );
};

export default Chest;