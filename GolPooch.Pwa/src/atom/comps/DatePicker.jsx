import React, { useState } from 'react';
import { makeStyles, FormControl, Select, InputLabel, MenuItem } from '@material-ui/core';

const useStyles = makeStyles({
    root: {
        position: 'relative',
        display: 'inline-block',
        '& .validation-message': {
            color: 'red'
        },
        '& .inputs': {
            marginTop: 16
        }
    },
    label: {
        fontSize: '0.75rem',
        transformOrigin: 'top right',
        color: 'black',
        lineHeight: 0,
        position: 'absolute',
        top: 0,
        left: 0,
    },
    dayFormControl: {
        width: 40,
        marginRight: 5
    },
    monthFormControl: {
        width: 40,
        marginRight: 5
    },
    yearFormControl: {
        width: 60
    }
});

const toJalali = (gy, gm, gd) => {
    let g_d_m = [0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334];
    let jy;
    if (gy > 1600) {
        jy = 979;
        gy -= 1600;
    } else {
        jy = 0;
        gy -= 621;
    }
    let gy2 = (gm > 2) ? (gy + 1) : gy;
    let days = (365 * gy) + (parseInt((gy2 + 3) / 4)) - (parseInt((gy2 + 99) / 100)) + (parseInt((gy2 + 399) / 400)) - 80 + gd + g_d_m[gm - 1];
    jy += 33 * (parseInt(days / 12053));
    days %= 12053;
    jy += 4 * (parseInt(days / 1461));
    days %= 1461;
    if (days > 365) {
        jy += parseInt((days - 1) / 365);
        days = (days - 1) % 365;
    }
    let jm = (days < 186) ? 1 + parseInt(days / 31) : 7 + parseInt((days - 186) / 30);
    let jd = 1 + ((days < 186) ? (days % 31) : ((days - 186) % 30));
    return [jy, jm, jd];
}
const checkKabise = (year) => year % 4 == 3;
const DatePicker = (props) => {
    const classes = useStyles();
    const dt = new Date();
    const jDt = toJalali(dt.getFullYear(), dt.getMonth() + 1, dt.getDate());
    const defaultValue = `${jDt[0]}/${jDt[1] < 10 ? '0' : ''}${jDt[1]}/${jDt[2] < 10 ? '0' : ''}${jDt[2]}`;
    let val = (props.value || defaultValue).split('/');
    const [year, setYear] = useState(val[0]);
    const [month, setMonth] = useState(val[1]);
    const [day, setDay] = useState(val[2]);
    const [valMsg, setValMsg] = useState('');
    const [isKabise, setIsKabise] = useState(checkKabise(year));
    const _handleChange = (event) => {
        let name = event.target.name;
        setIsKabise(checkKabise(year));
        let v = event.target.value;
        let vals = [year, month, day];
        switch (name) {
            case "day":
                vals[2] = v;
                setDay(v);
                break;
            case "month":
                vals[1] = v;
                setMonth(v);
                break;
            case "year":
                vals[0] = v;
                setYear(v);
                break;
        }
        if (props.onChange)
            props.onChange(`${vals[0]}/${vals[1]}/${vals[2]}`);
    }

    let days = [], months = [], years = [];
    for (let i = jDt[0] - 120; i <= jDt[0]; i++) years.push(`${i}`);
    for (let i = 1; i < 12; i++) months.push(`${(i < 10 ? '0' : '')}${i}`);
    for (let i = 1; i < 29; i++) days.push(`${(i < 10 ? '0' : '')}${i}`);
    return (<div className={classes.root}>
        <label className={classes.label} id={`lbl-${props.id}`}>{props.label}</label>
        <div className='inputs'>
            <FormControl className={classes.dayFormControl}>
                <Select
                    id={`${props.id}-day`}
                    displayEmpty
                    name="day"
                    value={day}
                    onChange={_handleChange}
                >
                    <MenuItem value="" disabled>روز</MenuItem>
                    {days.map((x, idx) => <MenuItem key={idx} value={x}>{x}</MenuItem>)}
                    {(parseInt(month) !== 12 || isKabise) ? <MenuItem value='30'>30</MenuItem> : null}
                    {parseInt(month) < 6 ? <MenuItem value='31'>31</MenuItem> : null}
                </Select>
            </FormControl>
            <FormControl className={classes.monthFormControl}>
                <Select
                    id={`${props.id}-month`}
                    displayEmpty
                    name="month"
                    value={month}
                    onChange={_handleChange}
                >
                    <MenuItem value="" disabled>ماه</MenuItem>
                    {months.map((x, idx) => <MenuItem key={idx} value={x}>{x}</MenuItem>)}
                </Select>
            </FormControl>
            <FormControl className={classes.yearFormControl}>
                <Select
                    id={`${props.id}-year`}
                    displayEmpty
                    name='year'
                    value={year}
                    onChange={_handleChange}
                >
                    <MenuItem value="" disabled>سال</MenuItem>
                    {years.map((x, idx) => <MenuItem key={idx} value={x}>{x}</MenuItem>)}
                </Select>
            </FormControl>
        </div>
        <div className='validation-message'>
            <span>{valMsg}</span>
        </div>
    </div>);
}
export default DatePicker;