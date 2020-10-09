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
    const [nLState, setNLState] = useRecoilState(nLAtom);
    const { item } = props;
    const _handleClick = async (id, isRead) => {
        if (!isRead) {
            let call = await notificationSrv.read(id);
            if (call.isSuccessful)
                setNLState({ ...nLState, newNotificationsCount: call.result });
        }
    }
    return (<Accordion className={`${classes.notificationComp} ${(item.isRead ? 'read' : 'not-read')}`}>
        <AccordionSummary
            onClick={() => _handleClick(item.notificationId, item.isRead)}
            expandIcon={<FcExpand fontSize={15} style={{ color: '#eee' }} fill="#EEE" />}
            id={`summary_${item.notificationId}`}
        >
            <Typography className='heading' component='h4'>
                <img className='img-icon' alt="Remy Sharp" src={item.iconUrl || item.imageUrl} />
                <span className='subject'>{item.subject}</span>
                {/* <Typography component='p'>{item.text}</Typography> */}
            </Typography>
        </AccordionSummary>
        <AccordionDetails>
            <p className='txt'>{item.text}</p>
            {/*{item.imageUrl ? <a className='w-100' href={item.actionUrl}><img className='img-full' src={item.imageUrl} alt={item.subject} /></a> : null}
            //{item.actionText ? <Button variant="contained" color="primary" href={item.actionUrl}>{item.actionText}</Button> : null} */}

            <div className={classes.time}>
                <BiTimeFive fontSize={16} />
                <span style={{ marginLeft: 10 }}>
                    {item.insertDate}
                </span>
            </div>
        </AccordionDetails>
    </Accordion>);
}