import React from 'react';
import { Redirect } from 'react-router-dom';
import { makeStyles, Button } from '@material-ui/core';
import logoImage from '../../assets/images/logo.jpeg';
import config from './../../config';
import strings from '../../core/strings';
import { useHistory } from "react-router-dom";
import userSrv from '../../services/userSrv';
import { TouchApp, AddBox, ShareOutlined } from '@material-ui/icons';
import { FiShare, FiPlusSquare } from 'react-icons/fi';
import { blue } from '@material-ui/core/colors';

const useStyles = makeStyles({
    app: {
        borderBottom: "1px solid #eee",
        paddingBottom: "25px"
    },
    startPage: {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        padding: 15,
        backgroundColor: '#fff'
    },
    logo: {
        maxWidth: '100%'
    },
    steps: {
        listStyle: 'none',
        marginBottom: 15,
        '& li': {
            padding: "15px 10px",
            '& svg': {
                fontSize: 24,
                verticalAlign: 'middle',
                marginRight: 5,
            },
            '& span': {
                fontSize: 15,
                verticalAlign: 'middle',
                marginRight: 5,
            }
        }
    },
    blue: {
        color: "#2196F3"
    },
    gray: {
        color: "#6d6d6d"
    },
    addHomeScreenButton: {
        height: 48,
        fontSize: 13,
        color: "#fff",
        backgroundColor: "#03A9F4",
        width: "100%"
    }
});

const Start = () => {
    const classes = useStyles();
    let jwt = userSrv.isAuthenticated();
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
            <h3 className={classes.app}>{strings.webApp} <strong>{strings.appName}</strong> {strings.addHomeScreen}</h3>
            <ul className={classes.steps}>
                <li>
                    <FiShare className={classes.blue} />
                    {strings.addHomeScreenStep1}</li>
                <li>
                    <FiPlusSquare className={classes.gray} />{strings.addHomeScreenStep2}
                </li>
                <li>
                    <span className={classes.blue}>Add</span>
                    {strings.addHomeScreenStep3}
                </li>
            </ul>

            <footer className="addHomeScreenFooter">
                <Button onClick={handleclick} className={classes.addHomeScreenButton} variant="contained" color="primary">
                    {strings.underestood}
                </Button>
            </footer>
        </div>
    );
};

export default Start;