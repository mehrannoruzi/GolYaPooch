import React from 'react';
import { useHistory } from 'react-router-dom';
import strings from '../../../core/strings';
import { Grid, makeStyles } from '@material-ui/core';
import { FaCheckCircle } from 'react-icons/fa';
import Button from '../../../atom/comps/Button';

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
        width:'230px!important',
       
        borderRadius: '3px',
        position: 'fixed',
        left: '50%',
        zIndex: 999,
        bottom: '30px',
        transform: 'translateX(-50%)',
        backgroundColor: '#2196F3 !important',
        boxShadow: '0px 0px 4px 3px rgb(255 255 255 / 50%)',
        padding: 10

    },
    traceId: {
        paddingBottom: '50px'
    }
});

const Success = (props) => {
    //HOOKS
    const classes = useStyles();
    const history = useHistory();
    //recoil
    const { info } = props;

    return (
        <Grid container className={classes.successComp}>
            <Grid xs={12} item className='heading'>
                <FaCheckCircle />
                <h3 className='hx'>{strings.successfulPayment}</h3>
            </Grid>
            <Grid item xs={12} className={classes.traceId}>
                <Grid container>
                    <Grid xs={7} item className='lbl'>
                        {strings.trackingId}
                     </Grid>
                    <Grid xs={5} item className='val'>{info.TrackingId}</Grid>
                </Grid>
            </Grid>
            <Grid xs={12} item className='info mb-15'>
                <Grid container>
                    <Grid xs={7} item className='lbl'>
                        {strings.paymentDate}
                    </Grid>
                    <Grid xs={5} item className='val'>{info.Date}</Grid>
                    <Grid xs={7} item className='lbl'>
                        {strings.paymentTime}
                    </Grid>
                    <Grid xs={5} item className='val'>{info.Time}</Grid>
                </Grid>
            </Grid>
            <Grid xs={12} item className='chance'>
                <Grid container>
                    <Grid xs={7} item className='lbl'>
                        { strings.latestChance}
                     </Grid>
                    <Grid xs={5} item className='val'>{info.BeforeChance}</Grid>
                    <Grid xs={7} item className='lbl'>
                        {strings.addedChance}
                    </Grid>
                    <Grid xs={5} item className='val'>{info.NewChance}</Grid>
                    <Grid xs={7} item className='lbl'>
                        {strings.currentChance}
                    </Grid>
                    <Grid item xs={5} className='val'>{info.TotalChance}</Grid>
                </Grid>
            </Grid>
            <Button onClick={() => history.push('/nl/chests')} className={`btn-purchase ${classes.linkToStore}`}>{ strings.tryChanceButton}</Button>
        </Grid>
    );
}
export default Success;