import React from 'react';
import { makeStyles, Typography, Accordion, AccordionSummary, AccordionDetails, Button } from '@material-ui/core';
import { FcExpand } from 'react-icons/fc';
import { MdNotificationsNone } from 'react-icons/md';
import { FaSms, FaRobot } from 'react-icons/fa';

import notificationSrv from './../../../services/notificationSrv';
import { BiTimeFive } from 'react-icons/bi';
import nLAtom from '../../../atom/state/nLState';
import { useRecoilState } from 'recoil';

const notifType = {
    sms: 1,
    telegramBot: 2,
    pushNotification: 3
};

const useStyles = makeStyles({
    notificationComp: {
        margin: 10,
        boxShadow: 'none',
        '&.not-read': {
            '& .heading': {
                fontWeight: '600!important'
            }
        },
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
    },
    typeBadge: {
        position: 'absolute',
        right: '13px',
        fontSize: '20px',
        top: 0,
        color: '#666'
    }
});


export default function (props) {
    const { item } = props;
    //Hooks
    const classes = useStyles();
    const [itemIsRead, setIsRead] = React.useState(item.isRead);
    //Recoil
    const [nLState, setNLState] = useRecoilState(nLAtom);
    const _handleChange = (panel) => {
        if (props.onClick) props.onClick(panel);
    }
    const _handleClick = async (id) => {
        if (props.expanded !== `panel${id}` && !itemIsRead) {
            setIsRead(true);
            let call = await notificationSrv.read(id);
            if (call.isSuccessful)
                setNLState({ ...nLState, newNotificationsCount: call.result });
        }
    }
    const getNotifTypeBadge = (type) => {
        switch (type) {
            case notifType.sms:
                return <FaSms />;
            case notifType.telegramBot:
                return <FaRobot />;
            default:
                return <MdNotificationsNone />;
        }
    }

    return (<Accordion className={`${classes.notificationComp} ${(itemIsRead ? 'read' : 'not-read')}`}
        expanded={props.expanded} onChange={() => _handleChange(`panel${item.notificationId}`)}>
        <AccordionSummary
            onClick={() => _handleClick(item.notificationId)}
            expandIcon={<FcExpand fontSize={15} style={{ color: '#eee' }} fill="#EEE" />}
            id={`summary_${item.notificationId} `}>
            <Typography className='heading' component='h4'>
                <img className='img-icon' alt="notif icon" src={item.iconUrl || item.imageUrl} />
                <span className='subject'>{item.subject}</span>
            </Typography>
            <span className={classes.typeBadge}>
                {getNotifTypeBadge(item.type)}
            </span>
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