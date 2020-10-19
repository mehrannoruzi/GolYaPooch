import React from 'react';
import { makeStyles, Typography, Grid } from '@material-ui/core';
import { FcExpand } from 'react-icons/fc';
import notificationSrv from './../../../services/notificationSrv';
import { BiTimeFive } from 'react-icons/bi';
import nLAtom from '../../../atom/state/nLState';
import { useRecoilState } from 'recoil';

const useStyles = makeStyles({
    root: {}
});


export default function (props) {
    //Hooks
    const classes = useStyles();
    //Recoil
    const { item } = props;

    return (<Grid item xs={12} className={classes.root}>
        <Typography className='heading' component='h4'>
            <img className='img-icon' alt="Remy Sharp" src={item.iconUrl || item.imageUrl} />
            <span className='subject'>{item.subject}</span>
        </Typography>
    </Grid>);
}