import React, { useState } from 'react';
import { TextField, FormControlLabel, Checkbox, Link, Box } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import strings, { validationStrings } from './../../../core/strings';
import { validate } from './../../../core/utils';
import Button from './../../../atom/comps/Button';
import userSrv from '../../../services/userSrv';
import toastState from '../../../atom/state/toastState';
import bottomUpModalState from './../../../atom/state/bottomUpModalState';
import authPageState from '../../../atom/state/authPageState';
import { ExitToApp } from '@material-ui/icons';

export default function () {

    //Hooks
    const [inProgress, setInProgress] = useState(false);
    const [ruleAgreement, setRuleAgreement] = useState(false);
    const [mobileNumber, setMobileNumber] = useState({
        value: '',
        error: false,
        errorMessage: ''
    });

    //Recoil
    const [toast, setToastState] = useRecoilState(toastState);
    const [bottomUpModal, setBottomUpModalState] = useRecoilState(bottomUpModalState);
    const [rState, setAuthPageState] = useRecoilState(authPageState);

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
        var response = await userSrv.login(mobileNumber.value).finally(() => {
            setInProgress(false);
        });
        if (!response.isSuccessful)
            setToastState({ ...toast, open: true, severity: 'error', message: response.message });
        else setAuthPageState({ activePanel: 'verify', mobileNumber: mobileNumber.value, transactionId: response.result });
    }

    const _showRules = () => {
        setBottomUpModalState({ ...bottomUpModal, open: true, title: strings.rules, children: function () { return <p className='rules'>{strings.ruelsText}</p> } })
    }

    return (
        <div id='comp-login' className='container'>
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
                    type="tel"
                    label={strings.mobileNumber}
                    value={mobileNumber.value}
                    onChange={(e) => setMobileNumber({ value: e.target.value, error: false, errorMessage: '' })}
                    helperText={mobileNumber.errorMessage}
                    style={{ fontFamily: 'iransans' }}
                />
            </div>
            <div className='form-group text-right'>
                <FormControlLabel
                    control={<Checkbox color="primary" checked={ruleAgreement} onChange={() => setRuleAgreement(!ruleAgreement)} name="ruleAgreement" />}
                    label={strings.aggreedWithRules} />
                <Link onClick={_showRules}>
                    <small>({strings.show})</small>
                </Link>
            </div>
            <Box textAlign="right" className="form-group">
                <Button icon={ExitToApp} onClick={() => _submit()} loading={inProgress} disabled={inProgress} className='btn-primary'>{strings.signInToSystem}</Button>
            </Box>
        </div>
    );
}