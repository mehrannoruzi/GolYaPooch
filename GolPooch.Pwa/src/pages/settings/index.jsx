import React, { useState, useEffect } from 'react';
import Banners from '../../atom/comps/Banners';
import { makeStyles, Container, Avatar } from '@material-ui/core';
import userSrv from '../../services/userSrv';
import { Link } from 'react-router-dom';
import toastState from '../../atom/state/toastState';
import { useRecoilState } from 'recoil';
import { FiChevronLeft } from 'react-icons/fi';
import { BsFileEarmarkRuled } from 'react-icons/bs';

const useStyles = makeStyles({
    settingsPage: {
        backgroundColor: '#f8faf9',
        '& ul': {
            listStyle: 'none',
            '& li': {
                '&.profile': {
                    marginBottom: 30
                },
                borderBottom: 'solid 1px #eee',
                '& .MuiContainer-root': {
                    backgroundColor: '#fff',
                    '& a': {
                        display: 'flex',
                        position: 'relative',
                        alignItems: 'center',
                        '& .hx': {
                            padding: '0 10px',
                            display: 'flex',
                            flexDirection: 'column'
                        },
                        '& .arrow-left': {
                            fontSize: 20,
                            position: 'absolute',
                            right: 0,
                            top: '50%',
                            transform: 'translateY(-50%)'
                        },
                        '& .icon': {
                            fontSize: 20
                        }
                    }
                }

            }
        }
    }
});

const Settings = () => {
    const classes = useStyles();
    //recoil
    const [toast, setToastState] = useRecoilState(toastState);
    const userInfo = userSrv.getInfo();
    console.log(userInfo);
    return (
        <div id='page-settings' className={`page ${classes.settingsPage}`}>
            <ul>
                <li className='profile'>
                    <Container>
                        <Link to='/bl/profile'>
                            {userInfo.avatar ? <Avatar src={userInfo.avatar} /> : null}
                            <h5 className='hx'>
                                {userInfo.firstName ? <span>{userInfo.firstName} {userInfo.lastName}</span> : null}
                                <span>{userInfo.mobileNumber}</span>
                            </h5>

                            <FiChevronLeft className='arrow-left' />

                        </Link>
                    </Container>

                </li>
                <li>
                    <Container>
                        <Link to='/bl/profile'>
                            <BsFileEarmarkRuled className='icon' />
                            <h5 className='hx'>
                                قوانین
                            </h5>

                            <FiChevronLeft className='arrow-left' />

                        </Link>

                    </Container>

                </li>
            </ul>
        </div>
    );
};

export default Settings;