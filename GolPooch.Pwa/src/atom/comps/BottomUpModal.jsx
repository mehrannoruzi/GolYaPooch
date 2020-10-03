import React from 'react';
import { useRecoilState } from 'recoil';
import buttomUpModalState from './../state/bottomUpModalState';
import { Grid, makeStyles } from '@material-ui/core';

const useStyles = makeStyles({
    compBottomUpModal: {
        position: 'absolute',
        top: 0,
        bottom: 0,
        right: 0,
        left: 0,
        backgroundColor: 'rgba(0,0,0,0.5)',
        zIndex: 999,

        '& .content': {
            backgroundColor: '#fff',
            position: 'absolute',
            bottom: 0,
            marginTop: '20%',
            padding: '0 15px 15px 15px',
            borderTopRightRadius: '5px',
            borderTopLeftRadius: '5px',

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
                    right: 0,
                    color: '#666',
                    fontSize: '25px'
                }
            }

        }

    }
});

export default function () {
    const classes = useStyles();
    const [rState, setRState] = useRecoilState(buttomUpModalState);
    return (<div id="comp-bottom-up-modal" className={`${classes.compBottomUpModal} ${(rState.open ? '' : 'd-none')}`}>
        <div className='content '>
            <div className='header'>
                <h4 className='hx'>{rState.title}</h4>
                <button className='btn-close' onClick={() => setRState({ ...rState, open: false, message: '' })}><i className='icon zmdi zmdi-close'></i></button>
            </div>
            {rState.children ? <rState.children /> : null}
        </div>
    </div>);
}
