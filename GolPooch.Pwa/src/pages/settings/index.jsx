import React, { useState } from 'react';
import { makeStyles, Container, Avatar } from '@material-ui/core';
import { useHistory } from 'react-router-dom';
import userSrv from '../../services/userSrv';
import { Link } from 'react-router-dom';
import { FiChevronLeft } from 'react-icons/fi';
import { BsFileEarmarkRuled, BsInfoCircle, BsPower, BsFileText } from 'react-icons/bs';
import { BiSupport, BiMessageSquareAdd } from 'react-icons/bi';
import { AiOutlineAppstore } from 'react-icons/ai';
import { BiUser } from 'react-icons/bi';
import { GiTicket } from 'react-icons/gi';
import strings from '../../core/strings';
import NavItem from './comps/navItem';
import config from '../../config';
import packageJson from '../../../package.json';
import Confetti from 'react-dom-confetti';
import { useEffect } from 'react';


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
                '& .item': {
                    display: 'flex',
                    position: 'relative',
                    alignItems: 'center',
                    padding: 0,
                    width: '100%',
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
            },
            '& .version': {
                textAlign: 'center',
                padding: '20px 0',
                fontSize: '0.8rem',
                backgroundColor: '#fff',
                borderBottom: 'none'
            }
        }
    }
});

const Settings = () => {
    //Hooks
    const classes = useStyles();
    const history = useHistory();
    const userInfo = userSrv.getInfo();
    const [clicks, setClick] = useState(0);
    const [confettiIsActive, setConfettiIsActive] = useState(false);

    const _handleLogOut = () => {
        localStorage.removeItem(config.keys.token);
        history.push('/el/auth');
    }


    return (
        <div id='page-settings' className={`${classes.settingsPage}`}>
          
            <ul>
                <li className='profile'>
                    <Container>
                        <Link to='/bl/profile' className='item'>
                            {userInfo.avatar ? <Avatar src={userInfo.avatar} /> : <BiUser className='icon' />}
                            <h5 className='hx settingMenu'>
                                {userInfo.firstName ? <span>{userInfo.firstName} {userInfo.lastName}</span> : null}
                                <span>{userInfo.mobileNumber}</span>
                            </h5>
                            <FiChevronLeft className='arrow-left' />
                        </Link>
                    </Container>
                </li>
                <NavItem href='/bl/supportcenter' icon={BiSupport} title={strings.supportCenter} />
                <Confetti active={clicks == 5} config={config.confettiConfig} />
                <NavItem href='/aboutus.html' redirect={true} icon={BsInfoCircle} title={strings.aboutUs} />
                <NavItem href='/rules.html' redirect={true} icon={BsFileText} title={strings.rules} />
                <NavItem href='/bl/ticketList' icon={BiMessageSquareAdd} title={strings.sendTicket} />
                <NavItem onClick={() => _handleLogOut()} icon={BsPower} title={strings.logOut} />
                <li className='version'>
                    <span onClick={() => { setClick( clicks + 1);}}>{strings.appVersion} {packageJson.version}</span>

                </li>
            </ul>
        </div>
    );
};

export default Settings;