import React from 'react';
import { Redirect } from 'react-router-dom';
import { makeStyles, Button } from '@material-ui/core';
import logoImage from '../../assets/images/logo.jpeg';
import config from './../../config';
import strings from '../../core/strings';
import { useHistory } from "react-router-dom";
import { Share, TouchApp, AddBox } from '@material-ui/icons';
import authSrv from '../../services/authSrv';

import token from './../../atom/selectors/token';
import { useRecoilValue } from 'recoil';

const useStyles = makeStyles({
    startPage: {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        padding: 15,
        backgroundColor:'#fff'
    },
    logo: {
        maxWidth: '100%'
    },
    steps: {
        listStyle: 'none',
        marginBottom: 15,
        '& li': {
            marginBottom: 10,
            '& svg': {
                fontSize: 30,
                verticalAlign: 'middle',
                marginRight: 5
            }
        }

    }
});

const Start = () => {
    const classes = useStyles();
    let jwt = authSrv.isAuthenticated();
    let history = useHistory();
    let page = '';
    let visited = localStorage.getItem(config.keys.visitedStartPage);
    if (jwt)
        page = '/nl/store';
    else if (visited) page = '/el/auth';
    const handleclick = () => {
        localStorage.setItem(config.keys.visitedStartPage, 'true');
        history.push('/el/auth')
    }
    if (page) return <Redirect to={page} />
    return (
        <div id='page-start' className={classes.startPage}>
            <img className={classes.logo} src={logoImage} />
            <h3 className='mb-15'>وب اپلیکیشن <strong>گل یا پوچ</strong> را به موبایل خود اضافه کنید</h3>
            <ul className={classes.steps}>
                <li><Share />1- در نوار بالا روی دکمه Share تپ کنید</li>
                <li><TouchApp />2- در منوی باز شده روی گزینه Add to Home Screen تپ کنید</li>
                <li><AddBox />3- در مرجله بعد در قسمت بالا روی  Add تپ کنید</li>
            </ul>
            <Button onClick={handleclick} className='w-100' variant="contained" color="primary">
                {strings.underestood}
            </Button>
        </div>
    );
};

export default Start;