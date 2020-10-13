import React from 'react';
import { makeStyles, Typography, Accordion, AccordionSummary, AccordionDetails, Button } from '@material-ui/core';
import { FcExpand } from 'react-icons/fc';
import ticketSrv from './../../../services/ticketSrv';
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
            '& .subject': {
                margin: '0 10px'
            }
        },
        '& .MuiAccordionDetails-root': {
            flexDirection: 'column',
            justifyContent: 'center',
            alignItems: 'center',
            '& .answer': {
                textAlign: 'justify',
                width: '100%',
                marginBottom: '15px',
                fontSize: '11px'
            },
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


    const _handleClick = async (item) => {
        if (item.answer && !item.isRead) {
            let call = await ticketSrv.read(item.ticketId);
            if (call.isSuccessful)
                setNLState({ ...nLState, newTicketCount: call.result });
        }
    }

    const _handleChange = (panel) => {
        if (props.onClick) props.onClick(panel);
    }
    
    return (<Accordion className={`${classes.notificationComp} ${(item.isRead ? 'read' : 'not-read')}`}
        expanded={props.expanded} onChange={() => _handleChange(`panel${item.ticketId}`)}>
        <AccordionSummary
            onClick={() => _handleClick(item)}
            expandIcon={<FcExpand fontSize={15} style={{ color: '#eee' }} fill="#EEE" />}
            id={`summary_${item.ticketId}`}
        >
            <Typography className='heading' component='h4'>
                <span className='subject'>{item.text}</span>
            </Typography>
        </AccordionSummary>
        {item.answer ? <AccordionDetails>

            <p className='answer'>{item.answer}</p>
            <div className={classes.time}>
                <BiTimeFive fontSize={16} />
                <span style={{ marginLeft: 10 }}>
                    {item.insertDateSh}
                </span>
            </div>
        </AccordionDetails> : null}

    </Accordion>);
}