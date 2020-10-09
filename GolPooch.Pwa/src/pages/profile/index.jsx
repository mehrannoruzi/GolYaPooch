import React, { useState, useReducer, useEffect } from 'react';
import DatePicker from 'react-datepicker2';
import EditIcon from '@material-ui/icons/Edit';
import { useRecoilState, useSetRecoilState } from 'recoil';
import { makeStyles, Paper, Container, Grid, TextField, InputLabel, Select, MenuItem, FormControl, FormHelperText, Box } from '@material-ui/core';
import toastState from '../../atom/state/toastState';
import strings, { validationStrings } from './../../core/strings';
import bLState from '../../atom/state/bLState';
import { validate } from './../../core/utils';
import Button from './../../atom/comps/Button';
import Avatar from './comps/avatar';
import userSrv from '../../services/userSrv';
import { toPersianDate } from '../../core/utils';
import momentJalaali  from 'moment-jalaali';

const useStyles = makeStyles({
    profilePage: {
        padding: '50px 0',
        background: 'transparent',
        '& .date-picker': {
            marginTop: '15px',
            overflow: 'hidden',
            position: 'relative',

            '&::after': {
                content: '"\\00a0"',
                right: 0,
                left: 0,
                bottom: 0,
                position: 'absolute',
                transition: 'border-bottom-color 200ms cubic-bezier(0.4, 0, 0.2, 1) 0ms',
                borderBottom: '1px solid rgba(0, 0, 0, 0.42)',
                pointerEvents: 'none'
            },
            '& input': {
                direction: 'ltr',
                maxWidth: '100%',
                boxSizing: 'border-box',
                border: 'none',
                padding: '3px 5px'
            },
        }
    }
});

const regions = [
    { id: 1, name: "آذربایجان شرقی" },
    { id: 2, name: "آذربایجان غربی" },
    { id: 3, name: "تهران" },
    { id: 4, name: "اصفهان" },
    { id: 5, name: "تبریز" }];

const inputs = ["firstName", "lastName", "email", "region", "gender", "birthdateSh", "intruducerId"];
const Profile = () => {
    const classes = useStyles();
    const [toast, setToastState] = useRecoilState(toastState);
    const setBLState = useSetRecoilState(bLState);
    const info = userSrv.getInfo();
    var states = {};
    for (let i = 0; i < inputs.length; i++)
        states[inputs[i]] = {
            value: info[inputs[i]] || '',
            error: false,
            errorMessage: ''
        };
    const [input, setInput] = useReducer((state, newState) => ({ ...state, ...newState }), states);
    const [inProgress, setInProgress] = useState(false);

    useEffect(() => {
        setBLState((state) => ({ ...state, title: strings.profile }))
    }, []);

    const _submit = async () => {
        if (!input.firstName.value) {
            setInput({ firstName: { ...input.firstName, error: true, errorMessage: validationStrings.required } });
            return;
        }
        if (!input.lastName.value) {
            setInput({ lastName: { ...input.lastName, error: true, errorMessage: validationStrings.required } });
            return;
        }
        if (!input.email.value) {
            setInput({ email: { ...input.email, error: true, errorMessage: validationStrings.required } });
            return;
        }
        if (!validate.email(input.email.value)) {
            setInput({ lastName: { ...input.email, error: true, errorMessage: validationStrings.required } });
            return;
        }
        if (!input.region.value) {
            setInput({ region: { ...input.region, error: true, errorMessage: validationStrings.required } });
            return;
        }
        if (!input.gender.value) {
            setInput({ gender: { ...input.gender, error: true, errorMessage: validationStrings.required } });
            return;
        }
        setInProgress(true);
        let model = {};
        for (let k in input)
            model[k] = input[k].value;
        var response = await userSrv.updateProfile({
            ...model,
            region: parseInt(model.region),
            gender: parseInt(model.gender)
        });
        setInProgress(false);
        if (!response.isSuccessful)
            setToastState({ ...toast, open: true, severity: 'error', message: response.message });
        else {
            console.log(model);
            userSrv.saveInfo(model);
            setToastState({ ...toast, open: true, severity: 'success', message: strings.profileSuccessUpdate });
        }
    }
    const _handleChange = (evt) => {
        const name = evt.target.name;
        const newValue = evt.target.value;
        setInput({ [name]: { value: newValue, error: false, errorMessage: '' } });
    }
    const _dateChanged = (v) => {
        setInput({ birthdateSh: { value: toPersianDate(v._d), error: false, errorMessage: '' } });
    }
    return (
        <Paper id='page-profile' className={`page ${classes.profilePage}`}>
            <Container>
                <Grid container spacing={1}>
                    <Grid item xs={12} className='mb-15'>
                        <Avatar mobileNumber={info.mobileNumber} avatar={info.avatar || null} />
                    </Grid>
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
                            <FormControl error={input.region.error}>
                                <InputLabel id="lbl-region">{strings.region}</InputLabel>
                                <Select
                                    labelId="lbl-region"
                                    id="region"
                                    name="region"
                                    value={input.region.value}
                                    onChange={_handleChange}>
                                    {regions.map((x, idx) => <MenuItem key={idx} value={x.id}>{x.name}</MenuItem>)}
                                </Select>
                                <FormHelperText>{input.region.errorMessage}</FormHelperText>
                            </FormControl>

                        </div>
                    </Grid>
                    <Grid item xs={6}>
                        <div className="form-group">
                            <FormControl error={input.gender.error}>
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
                                <FormHelperText>{input.gender.errorMessage}</FormHelperText>
                            </FormControl>

                        </div>
                    </Grid>
                    <Grid item xs={6}>
                        <div className="form-group date-picker">

                            <DatePicker
                                value={momentJalaali(input.birthdateSh.value, 'jYYYY/jM/jD')}
                                placeholder='تاریخ تولد'
                                isGregorian={false}
                                timePicker={false}
                                inputFormat='jYYYY/jMM/jDD'
                                onChange={_dateChanged} />
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
            </Container>
            <Box textAlign="right" className='btn-wrapper btn-BottomFixed'>
                <Button icon={EditIcon} onClick={() => _submit()} loading={inProgress} disabled={inProgress} className='btn-primary'>{strings.edit}</Button>
            </Box>
        </Paper>
    );
};

export default Profile;