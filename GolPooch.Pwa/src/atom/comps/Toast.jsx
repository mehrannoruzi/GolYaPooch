import React from 'react';
import { Snackbar } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import toastState from '../../atom/state/toastState';
import Alert from '@material-ui/lab/Alert';
 

function MuiAlert(props) {
    return <Alert elevation={6} variant="filled" {...props} />;
  }

export default function () {
    const [rState, setRState] = useRecoilState(toastState);
    const handleClose = () => {
        if (rState.callback)
            rState.callback();
        setRState({ ...rState, open: false, message: '', callback: null });
    }

    return (<Snackbar open={rState.open} autoHideDuration={rState.autoHideDuration} onClose={handleClose}>
        <MuiAlert onClose={handleClose} severity={rState.severity} >
            {rState.message}
        </MuiAlert>
    </Snackbar>);
}
