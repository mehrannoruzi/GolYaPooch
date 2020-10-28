import React from 'react';
import { makeStyles, Avatar } from '@material-ui/core';
import strings from '../../core/strings';
import Button from './Button';
import modalAtom from '../../atom/state/bottomUpModalState';
import { useRecoilState } from 'recoil';

const useStyles = makeStyles({
    root: {
        '& .web': {
            display: 'flex',
            flexDirection: 'row',
            alignItems: 'center',
            marginBottom: '15px',
            '& .info': {
                display: 'flex',
                flexDirection: 'column',
            }
        },
        '& .hx': {
            textAlign: 'center',
            lineHeight: '20px'
        },
        '& .install-hint': {
            color: '#666',
            textAlign: 'center',
            marginBottom: '15px'
        }
    },
    navs: {
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'space-between',
        '& .want': {
            width: 130,
            backgroundColor: '#8BC34A',
        },
        '& .dont-want': {
            width: 130,
            backgroundColor: 'red'
        }
    }

});


const Heading = (props) => {
    //Hooks
    const classes = useStyles();
    const [bottomUpModal, setBottomUpModalState] = useRecoilState(modalAtom);
    const { deferredPrompt } = props;
    const _handleAgree = () => {
        // Show the prompt
        deferredPrompt.prompt();
        setBottomUpModalState((state) => ({ ...state, open: false, children: null, message: '' }));
        // Wait for the user to respond to the prompt
        deferredPrompt.userChoice.then((choiceResult) => {
            if (choiceResult.outcome === 'accepted') {
                console.log('User accepted the A2HS prompt');
            } else {
                console.log('User dismissed the A2HS prompt');
            }
        });
    }
    const _handleReject = () => {
        setBottomUpModalState((state) => ({ ...state, open: false, children: null, message: '' }));
    }

    return (<div className={classes.root}>
        <h4 className='hx'>{strings.addToHomeScreenTitle}</h4>
        <p className='install-hint'>({strings.addToHomeScreenInstall})</p>
        <div className='web'>
            <Avatar src='https://cdn.golpooch.com/assets/logos/square-minimal-144.png' />
            <div className='info'>
                <strong>Gol Ya Pooch</strong>
                <a href="https://www.golpooch.ir/">https://www.golpooch.ir</a>
            </div>
        </div>
        <div className={classes.navs}>
            <Button className='dont-want' onClick={_handleReject}>{strings.iDontWant}</Button>
            <Button className='want' onClick={_handleAgree}>{strings.iWant}</Button>
        </div>
    </div>);
}
export default Heading;