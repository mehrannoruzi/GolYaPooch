import React from 'react';
import { makeStyles, Container, Avatar } from '@material-ui/core';
import userSrv from '../../services/userSrv';
import { Link } from 'react-router-dom';
import { FiChevronLeft } from 'react-icons/fi';
import { BsFileEarmarkRuled, BsInfoCircle } from 'react-icons/bs';
import { BiSupport } from 'react-icons/bi';
import { AiOutlineAppstore } from 'react-icons/ai';
import { BiUser } from 'react-icons/bi';
import strings from '../../core/strings';
import NavItem from './comps/navItem';

const useStyles = makeStyles({
    settingsPage: {
        backgroundColor: '#f8faf9',
        '& ul': {
            listStyle: 'none',
            '& li': {
                '&.profile': {
                    marginBottom: 30,
                    '& .MuiAvatar-root': {
                        width: '48px',
                        height: '48px',
                        '& img': {
                            width: '48px!important',
                            height: '48px!important',
                            borderRadius: '50%'
                        }
                    }
                },
                borderBottom: 'solid 1px #eee',
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

            },
            '& .MuiContainer-root': {
                backgroundColor: '#fff'
            }
        }
    }
});

const Settings = () => {
    const classes = useStyles();
    //recoil
    const userInfo = userSrv.getInfo();
    console.log(userInfo);
    return (
        <div id='page-settings' className={`${classes.settingsPage}`}>
            <ul>
                <li className='profile'>
                    <Container>
                        <Link to='/bl/profile'>
                            {userInfo.avatar ? <Avatar src={userInfo.avatar} /> : <BiUser className='icon' />}
                            <h5 className='hx'>
                                {userInfo.firstName ? <span>{userInfo.firstName} {userInfo.lastName}</span> : null}
                                <span>{userInfo.mobileNumber}</span>
                            </h5>
                            <FiChevronLeft className='arrow-left' />
                        </Link>
                    </Container>
                </li>
                <NavItem href='/bl/supportcenter' icon={BiSupport} title={strings.supportCenter} />
                <NavItem href='/bl/aboutus' icon={BsInfoCircle} title={strings.aboutUs} />
                <NavItem href='/bl/rules' icon={BsFileEarmarkRuled} title={strings.rules} />
                <NavItem href='/bl/appversion' icon={AiOutlineAppstore} title={strings.appVersion} />
            </ul>
        </div>
    );
};

export default Settings;