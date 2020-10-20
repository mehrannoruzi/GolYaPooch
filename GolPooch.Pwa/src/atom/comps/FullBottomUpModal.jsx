import React, { useEffect } from 'react';
import { makeStyles, Container } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import fullBottomUpModalState from './../state/fullBottomUpModalState';

const useStyles = makeStyles({
    compFullBottomUpModal: {
        position: 'fixed',
        top: 0,
        bottom: 0,
        right: 0,
        left: 0,
        backgroundColor: '#fff',
        zIndex: 999,
        overflow: 'hidden',
        '& .header': {
            position: 'relative',
            '& .MuiContainer-root': {
                borderBottom: 'solid 1px #ccc'
            },
            '& .hx': {
                padding: '15px 0',
                margin: 0
            },
            '& .btn-close': {
                position: 'absolute',
                top: '50%',
                transform: 'translateY(-50%)',
                right: 10,
                color: '#666',
                fontSize: '25px'
            }
        }
    }
});
export default function () {
    const classes = useStyles();

    const [rState, setRState] = useRecoilState(fullBottomUpModalState);
    useEffect(() => {
        document.body.style.overflow = rState.open ? 'hidden' : 'auto';
    }, [rState.open]);
    return (<div id="comp-full-bottom-up-modal" className={`animate__animated  animate__slideInUp ${classes.compFullBottomUpModal} ${(rState.open ? '' : 'd-none')}`}>
        <div className='header'>
            <Container><h4 className='hx'>{rState.title}</h4></Container>
            <button className='btn-close' onClick={() => setRState({ ...rState, open: false, message: '', children: null })}><i className='icon zmdi zmdi-close'></i></button>
        </div>
        <div className='children-wrapper'>
            {rState.children ? <rState.children {...rState.props} /> : null}
        </div>
    </div>);
}
