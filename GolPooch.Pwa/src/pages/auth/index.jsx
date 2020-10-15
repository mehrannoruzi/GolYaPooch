import React, { useState } from 'react';
import { makeStyles, Grid } from '@material-ui/core';
import logoImage from './../../assets/images/logo.jpeg';
import Login from './comps/login';
import Verify from './comps/verify';
import { useRecoilValue } from 'recoil';
import authPageState from '../../atom/state/authPageState';
import strings from '../../core/strings';
import packageJson from '../../../package.json';
const useStyles = makeStyles({
    root: {
        '& .version': {
            paddingTop: '30px',
            position: 'absolute',
            bottom: '15px',
            left: '50%',
            fontSize: '0.9rem',
            transform: 'translateX(-50%)'
        }
    }
});
const Authorization = () => {
    //Hooks
    const classes = useStyles();
    //Recoil
    const authState = useRecoilValue(authPageState);
    return (
        <div id='page-auth' className={`page flex-center ${classes.root}`}>
            <Grid container spacing={0}>
                <Grid item xs={12}>
                    <div className='flex-center'>
                        <img src={logoImage} alt='logo image' className='logo' />
                    </div>
                </Grid>

                <Grid item xs={12} sm={3} md={4}></Grid>
                <Grid item xs={12} sm={6} md={4}>
                    {authState.activePanel === 'login' ? <Login /> : <Verify />}
                </Grid>
                <div className='version'>
                    <span>{strings.appVersion} {packageJson.version}</span>
                </div>
            </Grid>
        </div>
    );
}
export default Authorization;