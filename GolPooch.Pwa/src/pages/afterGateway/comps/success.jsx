import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { Link } from 'react-router-dom';
import strings from '../../../core/strings';
import { Grid, makeStyles } from '@material-ui/core';
import { FaCheckCircle } from 'react-icons/fa';

const useStyles = makeStyles({
    successComp: {
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
        '& .info': {
            paddingBottom: 50
        },
        '& .lbl, & .val': {
            marginBottom: 15
        },
        '& .lbl': {
            paddingLeft: 50
        },
        '& .val': {
            paddingRight: 50,
            textAlign: 'right'
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
        padding: 10

    },
    traceId: {
        paddingBottom: '50px'
    }
});

const Success = (props) => {
    //HOOKS
    const classes = useStyles();
    //recoil
    const { info } = props;
    // useEffect(() => {
    //     const getDate = async () => {
    //         setInProgress(true);
    //         let info = await orderSrv.getChances();
    //         console.log(info);
    //         if (info.isSuccessful) {
    //             setChanceInfo(info.result);
    //         }
    //         else setToastState({ ...toast, open: true, severity: 'error', message: info.message });
    //         setInProgress(false);
    //     }
    //     getDate();
    // }, [query]);

    return (
        <Grid container className={classes.successComp}>
            <Grid xs={12} item className='heading'>
                <FaCheckCircle />
                <h3 className='hx'>{strings.successfulPayment}</h3>
            </Grid>
            <Grid item xs={12} className={classes.traceId}>
                <Grid container>
                    <Grid xs={7} item className='lbl'>
                        کد رهگیری:
                     </Grid>
                    <Grid xs={5} item className='val'>{info.TrackingId}</Grid>
                </Grid>
            </Grid>
            <Grid xs={12} item className='info mb-15'>
                <Grid container>
                    <Grid xs={7} item className='lbl'>
                        تاریخ پرداخت:
                    </Grid>
                    <Grid xs={5} item className='val'>{info.Date}</Grid>
                    <Grid xs={7} item className='lbl'>
                        ساعت پرداخت:
                    </Grid>
                    <Grid xs={5} item className='val'>{info.Time}</Grid>
                </Grid>
            </Grid>
            <Grid xs={12} item className='chance'>
                <Grid container>
                    <Grid xs={7} item className='lbl'>
                        تعداد شانس قبلی:
                     </Grid>
                    <Grid xs={5} item className='val'>{info.BeforeChance}</Grid>
                    <Grid xs={7} item className='lbl'>
                        تعداد شانس اضافه شده:
                    </Grid>
                    <Grid xs={5} item className='val'>{info.NewChance}</Grid>
                    <Grid xs={7} item className='lbl'>
                        تعداد شانس موجود:
                    </Grid>
                    <Grid item xs={5} className='val'>{info.TotalChance}</Grid>
                </Grid>
            </Grid>
            <Link to='/nl/chest' className={classes.linkToStore}>شانست رو دوباره امتحان کن</Link>
        </Grid>
    );
}
export default Success;