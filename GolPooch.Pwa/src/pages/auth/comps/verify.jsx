import React, { useState } from 'react';
import { Redirect } from 'react-router-dom';
import { TextField, Box, makeStyles, Link, Grid } from '@material-ui/core';
import strings, { validationStrings } from './../../../core/strings';
import Button from './../../../atom/comps/Button';
import { useRecoilState } from 'recoil';
import toastState from '../../../atom/state/toastState';
import authPageState from '../../../atom/state/authPageState';
import userSrv from '../../../services/userSrv';
import Countdown from '../../../atom/comps/Countdown';
import ReplayIcon from '@material-ui/icons/Replay';
import { Check } from '@material-ui/icons';
import { convertToEn } from './../../../core/utils';

const useStyles = makeStyles({
    spanNumber: {
        margin: 5,
        fontWeight: "bold"
    },
    changeNumber: {
        color: "#4caf50",
        margin: 5,
        fontWeight: "bold",
        backgroundColor: "#FFF !important",
        padding: 3,
        boxShadow: "none !important",
        border: "none"
    },
    footerBx: {
        padding: 10,
        minHeight: 50,
        paddingTop: 10,
        paddingBottom: 10,
        lineHeight: "30px"
    },
    countdown: {
        marginRight: "7px",
        fontSize: "15px"
    }
});

export default function () {
    const classes = useStyles();

    //Hooks
    const [counterSetting, setCountSetting] = useState({
        minutes: 0,
        seconds: 59,
        done: () => { setResendActive(true); }
    });
    const [redirectTo, setRedirectTo] = useState('');
    const [inProgress, setInProgress] = useState(false);
    const [verifyCode, setVerifyCode] = useState({
        value: '',
        error: false,
        errorMessage: ''
    });
    const [resendActive, setResendActive] = useState(false);

    //Recoil
    const [authPage, setAuthPageState] = useRecoilState(authPageState);
    const [toast, setToastState] = useRecoilState(toastState);

    //Events 
    const _submit = async (val) => {
        val = val || verifyCode.value;
        if (!val) {
            setVerifyCode({ ...verifyCode, error: true, errorMessage: validationStrings.required });
            return;
        }
        if (isNaN(val)) {
            setVerifyCode({ ...verifyCode, error: true, errorMessage: validationStrings.mustBeNumber });
            return;
        }
        setInProgress(true);
        var response = await userSrv.verify(authPage.transactionId, val).finally(() => {
            setInProgress(false);
        });
        if (!response.isSuccessful)
            setToastState({ ...toast, open: true, severity: 'error', message: response.message });
        else {
            userSrv.saveInfo({ mobileNumber: authPage.mobileNumber });
            setRedirectTo('/nl/chests');
        }
    }

    const _resent = async () => {
        var response = await userSrv.login(authPage.mobileNumber);
        if (response.isSuccessful) {
            setResendActive(false);
            setCountSetting({
                ...counterSetting,
                minutes: 1,
                seconds: 0,
            });
            setToastState({ ...toast, open: true, severity: 'info', message: strings.resentSuccess });
        }
    }
    const _handleVerifyCodeChange = async (e) => {
        let v = e.target.value;
        if(v) v = convertToEn(v);
        setVerifyCode({ value: v, error: false, errorMessage: '' });
        if (v.length === 4)
            await _submit(v);
    }

    if (redirectTo) return <Redirect to={redirectTo} />
    return (
        <>
            <div id='comp-login' className='container'>
                <Box mb={4} lineHeight={2}>
                    {strings.send4Digit}
                    <span className={classes.spanNumber}>{authPage.mobileNumber}</span>
                    {strings.send4Digit_2}
                    <Link href="#" onClick={() => setAuthPageState({ ...authPage, activePanel: 'login' })}><span className={classes.changeNumber}>{strings.changeMobileNumber}</span></Link>
                </Box>
                <div className="form-group inputVerifyCodePanel">
                    <TextField
                        className='text-center ltr verificationCode'
                        error={verifyCode.error}
                        id="verifyCode"
                        name="verifyCode"
                        type="text"
                        value={verifyCode.value}
                        onChange={(e) => _handleVerifyCodeChange(e)}
                        helperText={verifyCode.errorMessage}
                    />
                </div>
                <div className="form-group">
                    <Button icon={Check} onClick={() => _submit()} loading={inProgress} disabled={inProgress} className='btn-primary'>{strings.confirm}</Button>
                </div>
            </div>
            <div className="footer">
                {
                    resendActive ?
                        <Box>
                            <Grid container className={classes.footerBx} >
                                <Grid item xs={7}>
                                    <Box>{strings.doesntGiveCode}</Box>
                                </Grid>
                                <Grid item xs={5}>
                                    <Box className='text-center'>
                                        <Button className={classes.changeNumber} onClick={() => _resent()}>
                                            <i className="resentIcon"> <ReplayIcon /></i> {strings.verifyCode_sendAgain}
                                        </Button>
                                    </Box>
                                </Grid>
                            </Grid>
                        </Box>
                        :
                        <Box>
                            <Grid container className={classes.footerBx} >
                                <Grid item xs={12}>
                                    <span className={classes.countdown}>
                                        <Countdown {...counterSetting} />
                                    </span>
                                    {strings.resendCodeCountDown}
                                </Grid>
                            </Grid>
                        </Box>
                }
            </div>
        </>
    );
}