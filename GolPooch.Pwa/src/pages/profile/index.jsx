import React, { useState, useReducer, useEffect } from 'react';
import EditIcon from '@material-ui/icons/Edit';
import { Paper, Container, Grid, TextField, InputLabel, Select, MenuItem, FormControl, Box } from '@material-ui/core';
import strings, { validationStrings } from './../../core/strings';
import { validate } from './../../core/utils';
import Button from './../../atom/comps/Button';
const regions = [
    { id: 1, name: "آذربایجان شرقی" },
    { id: 2, name: "آذربایجان غربی" },
    { id: 3, name: "تهران" },
    { id: 4, name: "اصفهان" },
    { id: 5, name: "تبریز" }];

const inputs = ["firstName", "lastName", "email", "region", "gender", "birthdateSh", "intruducerId"];
const Profile = () => {
    var states = {};
    for (let i = 0; i < inputs.length; i++)
        states[inputs[i]] = {
            value: '',
            error: false,
            errorMessage: ''
        };
    const [input, setInput] = useReducer((state, newState) => ({ ...state, ...newState }), states);
    const [query, setQuery] = useState('');

    const [inProgress, setInProgress] = useState(false);
    useEffect(() => {

    }, [query]);

    const _submit = async () => {
        // if (!mobileNumber.value) {
        //     setMobileNumber({ ...mobileNumber, error: true, errorMessage: validationStrings.required });
        //     return;
        // }
        // if (validate.mobileNumber()) {
        //     setMobileNumber({ ...mobileNumber, error: true, errorMessage: validationStrings.invalidMobileNumber });
        //     return;
        // }
        // if (!ruleAgreement) {
        //     setToastState({ ...toast, open: true, severity: 'warning', message: strings.ruleAgreementRequired });
        //     return;
        // }

        // setInProgress(true);
        // var response = await authSrv.login(mobileNumber.value).finally(() => {
        //     setInProgress(false);
        // });
        // if (!response.isSuccessful)
        //     setToastState({ ...toast, open: true, severity: 'error', message: response.message });
        // else setAuthPageState({ activePanel: 'verify', mobileNumber: mobileNumber.value, transactionId: response.result });
    }
    const _handleChange = (evt) => {
        const name = evt.target.name;
        const newValue = evt.target.value;
        setInput({ [name]: { value: newValue, error: false, errorMessage: '' } });
    }
    return (
        <Paper id='page-profile' className='page'>
            <Container>
                <Grid container spacing={1}>
                    <Grid item xs={6}>
                        <div className="form-group">
                            <TextField
                                id="firstName"
                                name="firstName"
                                label={strings.firstName}
                                onChange={_handleChange}
                                style={{ fontFamily: 'iransans' }}
                                value={input.firstName.value}
                                error={input.firstName.error}
                                helperText={input.firstName.errorMessage}
                            />
                        </div>
                    </Grid>
                    <Grid item xs={6}>
                        <div className="form-group">
                            <TextField
                                id="lastName"
                                name="lastName"
                                label={strings.lastName}
                                onChange={_handleChange}
                                style={{ fontFamily: 'iransans' }}
                                value={input.lastName.value}
                                error={input.lastName.error}
                                helperText={input.lastName.errorMessage}
                            />
                        </div>
                    </Grid>
                    <Grid item xs={12}>
                        <div className="form-group">
                            <TextField
                                className='ltr-elm'
                                id="email"
                                name="email"
                                placeholder="me@company.com"
                                label={strings.email}
                                onChange={_handleChange}
                                style={{ fontFamily: 'iransans' }}
                                value={input.email.value}
                                error={input.email.error}
                                helperText={input.email.errorMessage}
                            />
                        </div>
                    </Grid>
                    <Grid item xs={6}>
                        <div className="form-group">
                            <FormControl>
                                <InputLabel id="lbl-region">{strings.gender}</InputLabel>
                                <Select
                                    labelId="lbl-region"
                                    id="region"
                                    name="region"
                                    value={input.region.value}
                                    onChange={_handleChange}>
                                    {regions.map((x, idx) => <MenuItem key={idx} value={x.id}>{x.name}</MenuItem>)}
                                </Select>
                            </FormControl>

                        </div>
                    </Grid>
                    <Grid item xs={6}>
                        <div className="form-group">
                            <FormControl>
                                <InputLabel id="demo-simple-select-label">{strings.gender}</InputLabel>
                                <Select
                                    labelId="lbl-gender"
                                    id="gender"
                                    name="gender"
                                    value={input.gender.value}
                                    onChange={_handleChange}>
                                    <MenuItem value="0">{strings.women}</MenuItem>
                                    <MenuItem value="1">{strings.men}</MenuItem>
                                </Select>
                            </FormControl>

                        </div>
                    </Grid>
                    <Grid item xs={6}>
                        <div className="form-group">
                            <TextField
                                id="birthdateSh"
                                name="birthdateSh"
                                label={strings.birthday}
                                onChange={_handleChange}
                                style={{ fontFamily: 'iransans' }}
                                value={input.birthdateSh.value}
                                error={input.birthdateSh.error}
                                helperText={input.birthdateSh.errorMessage}
                            />
                        </div>
                    </Grid>
                    <Grid item xs={6}>
                        <div className="form-group">
                            <TextField
                                id="lastName"
                                name="lastName"
                                label={strings.intruducerId}
                                onChange={_handleChange}
                                style={{ fontFamily: 'iransans' }}
                                value={input.intruducerId.value}
                                error={input.intruducerId.error}
                                helperText={input.intruducerId.errorMessage}
                            />
                        </div>
                    </Grid>
                </Grid>

                <Box textAlign="right" className="form-group">
                    <Button icon={EditIcon} onClick={() => _submit()} loading={inProgress} disabled={inProgress} className='btn-primary'>{strings.edit}</Button>
                </Box>
            </Container>

        </Paper>
    );
};

export default Profile;