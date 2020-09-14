import React, { useState } from 'react';
import { TextField, FormControlLabel, Checkbox } from '@material-ui/core';
import strings from './../../../core/strings';
import Button from './../../../core/comps/Button';

export default function () {
    const [inProgress, setInProgress] = useState(false);
    const [verifyCode, setVerifyCode] = useState({
        value: '',
        error: false,
        errorMessage: ''
    });
    return (<div id='comp-login'>
        <p className='hint'>{strings.pleaseEnterVerifyCode}</p>
        <div className="form-group">
            <TextField
                error={verifyCode.error}
                id="verifyCode"
                name="verifyCode"
                value={verifyCode.value}
                onChange={(e) => setVerifyCode({ value: e.target.value, error: false, errorMessage: '' })}
                helperText={verifyCode.errorMessage}
                style={{ fontFamily: 'iransans' }}
                variant="outlined"
            />
        </div>
        <div className="form-group">

        </div>
        <div className="form-group">
            <Button loading={inProgress} disabled={inProgress} className='btn-primary'>{strings.signInToSystem}</Button>
        </div>
    </div>);
}