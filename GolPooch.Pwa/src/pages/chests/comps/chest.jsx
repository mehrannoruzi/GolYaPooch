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
import fullBottomUpModalState from '../../../atom/state/fullBottomUpModalState';

const useStyles = makeStyles({
    chestComp: {
        '& .heading': {
            textAlign: 'center'
        },
        '& .chance': {
            textAlign: 'center',
            fontWeight: 800,
            fontSize: '1.5rem'
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
    }
});

const Chest = (props) => {
    //Hooks
    const classes = useStyles();
    const [agreed, setAgreement] = useState(null);
    const [inProgress, setInProgress] = useState(true);
    const [item, setItem] = useState([]);

    //recoil
    const [toast, setToastState] = useRecoilState(toastState);
    const [modal, setModalState] = useRecoilState(fullBottomUpModalState);

    //functions
    const getData = async () => {
        setInProgress(true);
        let get = await chestSrv.getSingle(parseInt(props.id));
        if (get.isSuccessful)
            setItem(get.result.items);

        else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
        setInProgress(false);
    }

    useEffect(() => {
        getData();
    }, []);

    return (
        <div id='comp-chest' className={classes.chestComp}>
            <Container>
                <h4 className='heading'>
                    {inProgress ? <Skeleton className='w-100' /> : 'میزان شانس شما در این قرعه کشی'}
                </h4>
            </Container>
            <h5 className='chance'>
                {inProgress ? <Skeleton className='w-100' /> : 2}
            </h5>
            <Divider component="div" />
            <Container>
                <Grid container>
                    <Heading>برای افزایش شانس بسته خود را انتخاب کنید</Heading>
                </Grid>
                <Chances />
            </Container>
            <Paper className='btns'>
                <Container>
                    <Grid container>
                        <Grid item xs={6} className='agree'>
                            <Button>{strings.iAgree}</Button>
                        </Grid>
                        <Grid item xs={6} className='reject'>
                            <Button onClick={() => setModalState({ ...modal, open: false })}>{strings.iReject}</Button>
                        </Grid>
                    </Grid>
                </Container>
            </Paper>
        </div>
    );
};

export default Chest;