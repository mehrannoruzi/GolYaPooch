import React from 'react';
import { makeStyles, Typography, Accordion, AccordionSummary, AccordionDetails, Button } from '@material-ui/core';
import { FcExpand } from 'react-icons/fc';
import notificationSrv from './../../../services/notificationSrv';
import { BiTimeFive } from 'react-icons/bi';
import nLAtom from '../../../atom/state/nLState';
import { useRecoilState } from 'recoil';

const useStyles = makeStyles({
    notificationComp: {
        margin: 10,
        boxShadow: 'none',
        '& .heading': {
            display: 'flex',
            alignItems: 'center',
            '& .img-icon': {
                width: 36
            },
            '& .subject': {
                margin: '0 10px'
            }
        },
        '& .MuiAccordionDetails-root': {
            flexDirection: 'column',
            justifyContent: 'center',
            alignItems: 'center',
            '& .txt': {
                textAlign: 'justify',
                width: '100%',
                marginBottom: '15px',
                fontSize: '11px'
            },
            '& .img-full': {
                width: '100%',
                marginBottom: '15px'
            }
        },

    },
    time: {
        textAlign: 'right',
        width: '100%',
        direction: 'rtl',
        '& svg': {
            verticalAlign: 'middle'
        }
    }
});


export default function (props) {
    //Hooks
    const classes = useStyles();
    //Recoil
    const { item } = props;

    const _handleClick = async (id, isRead) => {

    }
    
    return (<Grid item xs={12} className={classes.root} onClick={}>
        <Typography className='heading' component='h4'>
            <img className='img-icon' alt="Remy Sharp" src={item.iconUrl || item.imageUrl} />
            <span className='subject'>{item.subject}</span>
        </Typography>
    </Grid>);
}