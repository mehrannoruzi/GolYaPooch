import React, { useState, useEffect } from 'react';
import { useRecoilState } from 'recoil';
import { makeStyles } from '@material-ui/core/styles';
import { Paper } from '@material-ui/core';
import { commaThousondSeperator } from '../../../core/utils';
import strings from '../../../core/strings';
import userSrv from '../../../services/userSrv';
import toastState from '../../../atom/state/toastState';
import { Skeleton } from '@material-ui/lab';

const useStyles = makeStyles(() => ({
    root: {
        marginBottom: 15,
        padding:10,
        display: 'flex',
        justifyContent: 'space-between'
    }
}));


const Balance = () => {
    //Hooks
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [info, setInfo] = useState({ balance: 0 });
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);
    const getDate = async () => {
        setInProgress(true);
        let call = await userSrv.getProfile();
        if (call.isSuccessful) setInfo(call.result);
        else setToastState({ ...toast, open: true, severity: 'error', message: call.message });
        setInProgress(false);
    }

    useEffect(() => {
        getDate();
    }, []);

    return (
        <Paper className={classes.root}>
            <span>{strings.balance}:</span>
            {inProgress ? <Skeleton width={50} /> :
                <span>{commaThousondSeperator(info.balance)} {strings.moneyCurrency}</span>}
        </Paper>
    );
};

export default Balance;