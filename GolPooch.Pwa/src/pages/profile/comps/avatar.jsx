import React, { useState } from 'react';
import { makeStyles, Badge, Avatar, Link, CircularProgress } from '@material-ui/core';
import { useRecoilState } from 'recoil';
import toastState from '../../../atom/state/toastState';
import avatarImage from '../../../assets/images/avatar.png';
import { validationStrings } from '../../../core/strings';
import userSrv from '../../../services/userSrv';
import { BiPlusCircle } from 'react-icons/bi';

const useStyles = makeStyles({
    avatar: {
        display: 'flex',
        alignItems: 'center',
        textDecoration: 'none',
        '& .MuiBadge-badge': {
            width: '27px',
            height: '27px',
            borderRadius: '50%',
            '& .MuiCircularProgress-root': {
                width: '12px!important',
                height: '12px!important'
            }
        },
        '& .MuiAvatar-root': {
            width: 48,
            height: 48,
            boxShadow: " 0px 0px 3px 1px #d4d4d4",
            backgroundColor: '#eee',
            marginRight:10
        }
    }
});

export default function (props) {
    const maxFileSize = 5;
    const classes = useStyles();
    const [inProgress, setInProgress] = useState(false);
    const [avatar, setAvatar] = useState(props.avatar || avatarImage);
    const [toast, setToastState] = useRecoilState(toastState);
    let inputFile = null;

    const _uploaderChanged = async (event) => {
        let file = event.target.files[0];
        if (file.size > 1024 * 1024 * maxFileSize) {
            setToastState({ ...toast, open: true, severity: 'error', message: validationStrings.maxFileSizeExceeded(maxFileSize) });
            return;
        }
        setInProgress(true);
        let upload = await userSrv.updateAvatar(file);
        setInProgress(false);
        if (upload.isSuccessful) {
            let reader = new FileReader();
            reader.onload = function (e) {
                setAvatar(e.target.result);
                userSrv.saveInfo({ avatar: e.target.result });
            }.bind(this);
            reader.readAsDataURL(file);
        }
        else setToastState({ ...toast, open: true, severity: 'error', message: upload.message });

    }


    return (
        <div id='comp-avatar' className='container'>
            <Link onClick={() => inputFile.click()} color="inherit" className={classes.avatar}>
                <Badge anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'left',
                }} badgeContent={inProgress ? <CircularProgress /> : <BiPlusCircle style={{ fontSize: 15 }} />}>
                    <Avatar src={avatar} alt='avatar'  />
                </Badge>
                {props.mobileNumber}
            </Link>
            <input type='file'
                className='d-none'
                id='file-avatar'
                name='file-avatar'
                ref={c => { inputFile = c; }}
                onChange={_uploaderChanged}
                accept="image/*" />
        </div>
    );
}