﻿import React, { useState, useReducer, useEffect } from 'react';
//import DatePicker from 'react-datepicker2';
import EditIcon from '@material-ui/icons/Edit';
import { useRecoilState, useSetRecoilState } from 'recoil';
import { makeStyles, Paper, Container, Grid, TextField, InputLabel, Select, MenuItem, FormControl, FormHelperText, Box } from '@material-ui/core';
import toastState from '../../atom/state/toastState';
import strings, { validationStrings } from './../../core/strings';
import bLState from '../../atom/state/bLState';
import { validate } from './../../core/utils';
import Button from './../../atom/comps/Button';
import userSrv from '../../services/userSrv';

const useStyles = makeStyles({
    root: {

    }
});


const inputs = ["firstName", "lastName", "email", "region", "gender", "birthdateSh", "intruducerId"];
const Profile = () => {
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [items, setItems] = useState([]);
    const [pageNumber, setPageNumber] = useState(1);
    const [isBottom, setIsBottom] = useState(true);
    const [toast, setToastState] = useRecoilState(toastState);
    const [inProgress, setInProgress] = useState(false);
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);

    useEffect(() => {
        if (isBottom && (items.length === 0 || items.length > 10)) {
            const getDate = async () => {
                setInProgress(true);
                let get = await notificationSrv.get(12, pageNumber);

                if (get.isSuccessful) {
                    setItems([...items, ...get.result.items]);
                    if (get.result.items.length > 0)
                        setPageNumber(pageNumber + 1);
                }
                else setToastState({ ...toast, open: true, severity: 'error', message: get.message });
                setInProgress(false);
                setIsBottom(false);
            }
            getDate();
        }

    }, [isBottom]);

    function handleScroll(e) {
        let element = e.target
        if (!inProgress && element.scrollHeight - element.scrollTop === element.clientHeight) {
            console.log('fired');
            setIsBottom(true);
        }
    }

    return (
        <div id='page-profile' className={classes.profilePage}>
            {!inProgress && items.length === 0 ? <EmptyRecord text={strings.thereIsNoNotification} /> : null}
            {items.map((item, idx) => <Item key={idx} item={item} expanded={expanded === `panel${item.notificationId}`} onClick={_handleItemClick} />)}
            {(inProgress && pageNumber === 1) ? [0, 1, 2, 3, 4, 5, 6, 7, 9, 10].map((x, idx) => <Container key={idx} className={classes.loaderItem}>
                <Skeleton variant='rect' height={36} width={48} className='avatar' />
                <Skeleton className='subject' /></Container>) : null}
        </div>
    );
};

export default Profile;