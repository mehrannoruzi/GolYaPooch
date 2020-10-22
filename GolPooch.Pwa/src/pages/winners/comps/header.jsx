import React from 'react';
import { makeStyles, Typography, Grid } from '@material-ui/core';
import { FcExpand } from 'react-icons/fc';
import notificationSrv from './../../../services/notificationSrv';
import { BiTimeFive } from 'react-icons/bi';
import nLAtom from '../../../atom/state/nLState';
import { useRecoilState } from 'recoil';
import strings from '../../../core/strings';

const useStyles = makeStyles({
    root: {
        '& .cols-wrapper': {
            '& > div': {
                padding: '10px 0',
                marginBottom: 5,
                borderBottom:'solid 1px #ccc'
            }
        }
    }
});


export default function (props) {
    //Hooks
    const classes = useStyles();

    return (<Grid item xs={12} className={classes.root}>
        <Grid container className='cols-wrapper'>
            <Grid item xs={1}>#</Grid>
            <Grid item xs={4}>{strings.mobileNumber}</Grid>
            <Grid item xs={4}>{strings.chance}</Grid>
            <Grid item xs={3}>{strings.winCount}</Grid>
        </Grid>
    </Grid>);
}