import React, { useEffect } from 'react';
import { makeStyles, Grid } from '@material-ui/core';
import { useRecoilState, useRecoilValue } from 'recoil';
import logoImage from './../../assets/images/logo.jpeg';
import Login from './comps/login';
import Verify from './comps/verify';
import authPageState from '../../atom/state/authPageState';
import strings from '../../core/strings';
import nLAtom from '../../atom/state/nLState';
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
    const [nlState, setNLState] = useRecoilState(nLAtom);

    useEffect(() => {
        setNLState({ ...nlState, activeBotton: 2 });
    }, []);
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
                {authState.activePanel === 'login' ? <div className='version'>
                    <span>{strings.appVersion} {packageJson.version}</span>
                </div> : null}

            </Grid>
        </div>
    );
}
export default Authorization;