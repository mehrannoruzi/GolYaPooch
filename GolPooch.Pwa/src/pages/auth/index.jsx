import React, { useState } from 'react';
import Grid from '@material-ui/core/Grid';
import logoImage from './../../assets/images/logo.jpeg';
import Login from './comps/login';
import Verify from './comps/verify';
import { useRecoilValue } from 'recoil';
import authPageState from '../../atom/state/authPageState';

const Authorization = () => {
    const authState = useRecoilValue(authPageState);
    return (
        <div id='page-auth' className='page flex-center'>
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
            </Grid>
        </div>
    );
}
export default Authorization;