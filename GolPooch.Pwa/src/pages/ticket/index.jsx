import React, { useState, useEffect } from 'react';
import { Skeleton } from '@material-ui/lab';
import { makeStyles, Container, Paper, TextareaAutosize } from '@material-ui/core';
import strings from '../../core/strings';
import { useLocation } from "react-router-dom";
import { useRecoilState } from 'recoil';
import ticketSrv from '../../services/ticketSrv';
import Button from '../../atom/comps/Button';
import toastState from '../../atom/state/toastState';
import queryString from 'query-string';
import { BiSend } from 'react-icons/bi';
import bLState from '../../atom/state/bLState';
import { useSetRecoilState } from 'recoil';
import SupportImage from '../../assets/images/support.png';

const useStyles = makeStyles({
    root: {
        backgroundColor: '#eee',
        paddingBottom: 60,
        minHeight: 'calc(100vh - 50px)',
        boxSizing: 'border-box',
        backgroundImage: 'url(/ticket-bg.png)',
        '& .container': {
            overflow: 'hidden',
            display: 'flex',
            flexDirection: 'column'
        },
        '& .comment': {
            margin: '15px 0',
            padding: 10,
            position: 'relative',
            display: 'inline-flex',
            '& p': {
                whiteSpace: 'pre-line',
                lineHeight: '22px'
            }
        },
        '& .answer-wrapper': {
            display: 'flex',
            justifyContent: 'flex-end',
        },
        '& .answer': {
            backgroundColor: '#FFFDE7',
            color: '#000'
        },
        '& .add-box': {
            position: 'fixed',
            left: 0,
            right: 0,
            bottom: 0,
            display: 'flex',
            flexDirection: 'row',
            height: 40,
            background: '#fff',
            padding: 5,

            '& textarea': {
                flex: 1,
                border: 'none',
                float: 'none',
                boxShadow: 'none',
                borderRadius: 0,
                padding: 8,
                resize: 'none',

            },
            '& .btn-add': {
                backgroundColor: '#fff',
                border: 'none',
                color: '#666',
                width: 50,
                padding: 5,
                boxShadow: 'none',
                '& svg': {
                    fontSize: 20,
                    transform: 'rotate(180deg)'
                }
            }
        },
        '& .img-support': {
            width: '48px',
            height: '48px',
            margin: '15px 0 15px 10px'
        }
    },
    arrowRight: {
        width: 0,
        height: 0,
        borderStyle: 'solid',
        borderWidth: '0 0 15px 12px',
        borderColor: 'transparent transparent #fff transparent',
        position: 'absolute',
        left: '-9px',
        bottom: 0
    },
    arrowLeft: {
        width: 0,
        height: 0,
        borderStyle: 'solid',
        borderWidth: '15px 0 0 12px',
        borderColor: 'transparent transparent transparent #d4edda',
        position: 'absolute',
        right: '-9px',
        bottom: 0
    }
});

const Ticket = () => {
    //HOOKS
    const location = useLocation();
    const qParams = queryString.parse(location.search);
    const classes = useStyles();
    const [isEmpty, setIsEmpty] = useState(qParams.text ? false : true);
    const [text, setText] = useState(qParams.text || '');
    const [answer, setAnswer] = useState(qParams.answer || '');
    const [inProgress, setInProgress] = useState(false);

    //recoil
    const [toast, setToastState] = useRecoilState(toastState);
    const setBLState = useSetRecoilState(bLState);

    useEffect(() => {
        setBLState((state) => ({ ...state, title: text ? strings.viewTicket : strings.addTicket }));
    }, []);

    const _handleChange = (e) => {
        let v = e.target.value;
        setText(v);
    }

    const _handleSubmit = async () => {
        setInProgress(true);
        let call = await ticketSrv.add(text);
        console.log(call);
        if (call.isSuccessful) {
            setIsEmpty(false);
            setAnswer(strings.yourAnswerAppearsHere);
        }
        else setToastState({ ...toast, open: true, severity: 'error', message: call.message });
        setInProgress(false);
    }

    return (
        <div className={classes.root}>
            <Container className='container'>
                <div>
                    <Paper className='comment text'>
                        <p>{isEmpty ? strings.defaultTicketText : text}</p>
                        {!isEmpty ? <div className={classes.arrowRight}></div> : null}
                    </Paper>
                </div>
                {answer ? <div className='answer-wrapper'>
                    <Paper className='comment answer'>
                        <p>{answer}</p>
                    </Paper>
                    <img className='img-support' src={SupportImage} />
                </div> : null}
            </Container>
            {isEmpty ?
                <div className='add-box'>
                    <textarea onChange={_handleChange} placeholder={strings.addTicketPlaceHolder} />
                    <Button disabled={inProgress} loading={inProgress} className='btn-add' onClick={_handleSubmit}>
                        <BiSend />
                    </Button>
                </div> : null}
        </div>
    );
};

export default Ticket;