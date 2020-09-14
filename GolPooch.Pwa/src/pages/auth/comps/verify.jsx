import React, { useState } from 'react';
import { TextField, Box, makeStyles } from '@material-ui/core';
import strings from './../../../core/strings';
import Button from './../../../atom/comps/Button';

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
    const classes = useStyles();

    const [inProgress, setInProgress] = useState(false);
    const [verifyCode, setVerifyCode] = useState({
        value: '',
        error: false,
        errorMessage: ''
    });
    return (
        <>
            <div id='comp-login' className='container'>
                <Box mb={4} lineHeight={2}>
                    کد 4 رقمی به شماره
                     <span className={classes.spanNumber}>09393393079</span>
                  ارسال شد. کد را اینجا وارد کنید.
                     <a><span className={classes.changeNumber}>تغییر شماره تلفن همراه</span></a>
                </Box>
                <div className="form-group">
                    <TextField
                        className='ltr-elm'
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
                    <Button loading={inProgress} disabled={inProgress} className='btn-primary'>{strings.signInToSystem}</Button>
                </div>
            </div>
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