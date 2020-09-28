import React from 'react';
import { makeStyles } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import fullBottomUpModalState from './../state/fullBottomUpModalState';

const useStyles = makeStyles({
    compFullBottomUpModal: {
        position: 'absolute',
        top: 0,
        bottom: 0,
        right: 0,
        left: 0,
        backgroundColor: '#fff',
        zIndex: 999,
        '& .header': {
            position: 'relative',

            '& .hx': {
                padding: '15px 0',
                margin: 0
            },
            '& .btn-close': {
                position: 'absolute',
                top: '50%',
                transform: 'translateY(-50%)',
                right: 15,
                color: '#666',
                fontSize: '25px'
            }
        }

    }
});
export default function (props) {
    const classes = useStyles();

    const [rState, setRState] = useRecoilState(fullBottomUpModalState);
    return (<div id="comp-full-bottom-up-modal" className={`animate__animated  animate__slideInUp ${classes.compFullBottomUpModal} ${(rState.open ? '' : 'd-none')}`}>
        <div className='header'>
            <h4 className='hx'>{rState.title}</h4>
            <button className='btn-close' onClick={() =>setRState({ ...rState, open: false, message: '' })}><i className='icon zmdi zmdi-close'></i></button>
        </div>
        {rState.children ? <rState.children /> : null}
    </div>);
}
