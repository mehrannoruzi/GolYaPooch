import React, { useState } from 'react';
import { Redirect } from 'react-router-dom';
import { TextField, Box, makeStyles, Link } from '@material-ui/core';
import strings, { validationStrings } from './../../../core/strings';
import Button from './../../../atom/comps/Button';
import { useRecoilState } from 'recoil';
import toastState from '../../../atom/state/toastState';
import authPageState from '../../../atom/state/authPageState';
import authSrv from '../../../services/authSrv';

const useStyles = makeStyles({
    spanNumber: {
        margin: 5,
        fontWeight: "bold"
    },
    changeNumber: {
        color: "#4caf50",
        margin: 5,
        fontWeight: "bold"
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
    //Recoil
    const [rState, setAuthPageState] = useRecoilState(authPageState);
    const [toast, setToastState] = useRecoilState(toastState);

    const _submit = async () => {
        // console.log(verifyCode.value);
        // console.log(rState.transactionId);
        // return;
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
    if (redirectTo) return <Redirect to={redirectTo} />
    return (
        <>
            <div id='comp-login' className='container'>
                <Box mb={4} lineHeight={2}>
                    کد 4 رقمی به شماره
                  <span className={classes.spanNumber}>{rState.mobileNumber}</span>
                  ارسال شد. کد را اینجا وارد کنید.
                  <Link href="#" onClick={() => setAuthPageState({ ...rState, activePanel: 'login' })}><span className={classes.changeNumber}>تغییر شماره تلفن همراه</span></Link>
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
            </div >
            <div className="footer">
                <Box p={2}>
                    <span>
                        30
                    </span>
                    ثانیه تا درخواست مجدد کد
                </Box>
                <Box display="none" p={2}>
                    <Box>{strings.doesntGiveCode}</Box>
                    <Box>{strings.verifyCode_sendAgain}</Box>
                </Box>
            </div>
        </>
    );
}