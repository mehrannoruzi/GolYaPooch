import React, { useState } from 'react';
import { TextField, FormControlLabel, Checkbox, Link, Box } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import strings, { validationStrings } from './../../../core/strings';
import Button from './../../../core/comps/Button';
import authSrv from '../../../services/authSrv';
import { validate } from './../../../core/utils';
import toastState from '../../../atom/state/toastState';
import bottomUpModalState from '../../../atom/state/bottomUpModalState';

export default function () {
    const [inProgress, setInProgress] = useState(false);
    const [mobileNumber, setMobileNumber] = useState({
        value: '',
        error: false,
        errorMessage: ''
    });
    const [ruleAgreement, setRuleAgreement] = useState(false);
    const [toast, setToastState] = useRecoilState(toastState);
    const [bottomUpModal, setBottomUpModalState] = useRecoilState(bottomUpModalState);
    const _submit = async () => {
        if (!mobileNumber.value) {
            setMobileNumber({ ...mobileNumber, error: true, errorMessage: validationStrings.required });
            return;
        }
        if (validate.mobileNumber()) {
            setMobileNumber({ ...mobileNumber, error: true, errorMessage: validationStrings.invalidMobileNumber });
            return;
        }
        if (!ruleAgreement) {
            setToastState({ ...toast, open: true, severity: 'warning', message: strings.ruleAgreementRequired });
            return;
        }

        setInProgress(true);
        await authSrv.login(mobileNumber.value);
        setInProgress(false);
    }

    const showRules = () => {
        setBottomUpModalState({ ...bottomUpModal, open: true, title: strings.rules, children: function () { return <p className='rules'>{strings.ruelsText}</p> } })
    }
    return (<div id='comp-login'>

        <Box mb={4} lineHeight={2}>
            {strings.loginHelp}
        </Box>
        <div className="form-group">
            <TextField

                className='ltr-elm'

                error={mobileNumber.error}
                id="mobileNumber"
                name="mobileNumber"
                placeholder="9xxxxxxxxx"
                label={strings.mobileNumber}
                value={mobileNumber.value}
                onChange={(e) => setMobileNumber({ value: e.target.value, error: false, errorMessage: '' })}
                helperText={mobileNumber.errorMessage}
                style={{ fontFamily: 'iransans' }}
            />
        </div>

        <div className='form-group'>
            <FormControlLabel
                control={<Checkbox color="primary" checked={ruleAgreement} onChange={() => setRuleAgreement(!ruleAgreement)} name="ruleAgreement" />}
                label={strings.aggreedWithRules} />
            <Link href="#" onClick={showRules}>
                <small>({strings.show})</small>
            </Link>
        </div>
        <Box textAlign="right" className="form-group">
            <Button onClick={() => _submit()} loading={inProgress} disabled={inProgress} className='btn-primary'>{strings.signInToSystem}</Button>
        </Box>
    </div>);
}