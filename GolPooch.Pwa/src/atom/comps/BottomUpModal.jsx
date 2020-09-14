import React from 'react';
import { useRecoilState } from 'recoil';
import buttomUpModalState from './../state/bottomUpModalState';

export default function () {
    const [rState, setRState] = useRecoilState(buttomUpModalState);
    const handleClose = () => {
        setRState({ ...rState, open: false, message: '' });
    }
    return (<div id="comp-bottom-up-modal" className={rState.open ? '' : 'd-none'}>
        <div className='content animated fadeInUp '>
            <div className='header'>
                <h4 className='hx'>{rState.title}</h4>
                <button className='btn-close' onClick={handleClose}><i className='icon zmdi zmdi-close'></i></button>
            </div>

            {rState.children ? <rState.children /> : null}
        </div>

    </div>);
}
