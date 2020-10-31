import React, { useEffect } from 'react';
import { makeStyles } from '@material-ui/core';
import { RiWifiOffLine } from 'react-icons/ri';
import strings from '../../core/strings';
//import { useRecoilstate } from 'recoil';
import Button from './Button';

const useStyles = makeStyles({
    root: {
        display: 'flex',
        padding: 15,
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        '& svg': {
            fontSize: 50
        }
    }

});


const OffLlineModal = (props) => {
    const classes = useStyles();
    useEffect(() => {

    });
    return (<div className={classes.root}>
        <RiWifiOffLine />
        <h5>{strings.offlineText}</h5>
        <Button onClick={() => window.location.reload()}>{strings.retry}</Button>
    </div>);
}
export default OffLlineModal;