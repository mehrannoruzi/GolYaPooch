import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, List, ListItem, ListItemAvatar, ListItemText } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import notificationSrv from '../../services/notificationSrv';
import toastState from '../../atom/state/toastState';
import Item from './comps/item';
import Slider from "react-slick";

const useStyles = makeStyles({
    notificationComp: {
        paddingTop: 7.5,
        paddingBottom: 7.5,
        '& .MuiDivider-inset': {
            marginRight: 16
        }
    },
    inline: {
        display: 'inline',
    }
});

const Chest = () => {
    //Hooks
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(true);
    const [item, setItem] = useState([]);
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);

    useEffect(() => {


    }, []);

    return (
        <div id='comp-chest'>
            <h4 classsName='heading'>
                {inProgress ? <Skeleton className='w-100' /> : 'میزان شانس شما در این قرعه کشی'}
            </h4>
            <h5 classsName='heading'>
                {inProgress ? <Skeleton className='w-100' /> : item.chance}
            </h5>

        </div>

    );
};

export default Chest;