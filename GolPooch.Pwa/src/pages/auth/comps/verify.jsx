import React, { useState, useEffect } from 'react';
import { Redirect } from 'react-router-dom';
import { TextField, Box, makeStyles, Link, Grid } from '@material-ui/core';
import strings, { validationStrings } from './../../../core/strings';
import Button from './../../../atom/comps/Button';
import { useRecoilState } from 'recoil';
import toastState from '../../../atom/state/toastState';
import authPageState from '../../../atom/state/authPageState';
import authSrv from '../../../services/authSrv';
import Countdown from '../../../atom/comps/Countdown';

const useStyles = makeStyles({
    spanNumber: {
        margin: 5,
        fontWeight: "bold"
    },
    changeNumber: {
        color: "#4caf50",
        margin: 5,
        fontWeight: "bold"
    },
    hide: {
        display: "none"
    },
    show: {
        display: "inline"
    },
    footerBx: {
        padding: 15,
        minHeight: 50,
        paddingTop: 10,
        paddingBottom: 10,
        lineHeight: "30px"
    }
});


export default function () {
    //Hooks
    const classes = useStyles();
    const [redirectTo, setRedirectTo] = useState('');
    const [inProgress, setInProgress] = useState(false);
    const [verifyCode, setVerifyCode] = useState({
        value: '',
        error: false,
        errorMessage: ''
    });

    const [resendActive, setresendActive] = useState(false);

    //Recoil
    const [rState, setAuthPageState] = useRecoilState(authPageState);
    const [toast, setToastState] = useRecoilState(toastState);

    const _submit = async () => {
        if (!verifyCode.value) {
            setVerifyCode({ ...verifyCode, error: true, errorMessage: validationStrings.required });
            return;
        }
        if (isNaN(verifyCode.value)) {
            setVerifyCode({ ...verifyCode, error: true, errorMessage: validationStrings.mustBeNumber });
            return;
        }
        setInProgress(true);
        var response = await authSrv.verify(rState.transactionId, verifyCode.value).finally(() => {
            setInProgress(false);
        });
        if (!response.isSuccessful)
            setToastState({ ...toast, open: true, severity: 'error', message: response.message });
        else setRedirectTo('/store');
    }

    const counterSetting = {
        minutes: 0,
        seconds: 10,
        done: () => {
            setresendActive(true);
        }
    }

    if (redirectTo) return <Redirect to={redirectTo} />
    return (
        <>
            <div id='comp-login' className='container'>
                <Box mb={4} lineHeight={2}>
                    {strings.send4Digit}
                    <span className={classes.spanNumber}>{rState.mobileNumber}</span>
                    {strings.send4Digit_2}
                    <Link href="#" onClick={() => setAuthPageState({ ...rState, activePanel: 'login' })}><span className={classes.changeNumber}>{strings.changeMobileNumber}</span></Link>
                </Box>
                <div className="form-group">
                    <TextField
                        className='text-center ltr'
                        error={verifyCode.error}
                        id="verifyCode"
                        name="verifyCode"
                        value={verifyCode.value}
                        value={verifyCode.value}
                        onChange={(e) => setVerifyCode({ value: e.target.value, error: false, errorMessage: '' })}
                        helperText={verifyCode.errorMessage}
                    />
                </div>
                <div className="form-group">
                    <Button onClick={() => _submit()} loading={inProgress} disabled={inProgress} className='btn-primary'>{strings.confirm}</Button>
                </div>
            </div>
            <div className="footer">
                <Box className={resendActive ? classes.hide : classes.show}>
                    <Grid container className={classes.footerBx} >
                        <Grid item xs={12}>
                            <span>
                                <Countdown {...counterSetting} />
                            </span>
                            {strings.resendCodeCountDown}
                        </Grid>
                    </Grid>
                </Box>
                <Box className={resendActive ? classes.show : classes.hide}>
                    <Grid container   className={classes.footerBx} >
                        <Grid item xs={8}>
                            <Box>{strings.doesntGiveCode}</Box>
                        </Grid>
                        <Grid item xs={4}>
                            <Box>
                                <Link className={classes.changeNumber}>
                                    {strings.verifyCode_sendAgain}
                                </Link>
                            </Box>
                        </Grid>
                    </Grid>
                </Box>
            </div>
        </>
    );
}