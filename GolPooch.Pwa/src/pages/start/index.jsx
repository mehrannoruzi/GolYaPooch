import React from 'react';
import { Redirect } from 'react-router-dom';
import { makeStyles, Button } from '@material-ui/core';
import logoImage from '../../assets/images/logo.jpeg';
import config from './../../config';
import strings from '../../core/strings';
import { useHistory } from "react-router-dom";

const useStyles = makeStyles({
    startPage: {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignItems: 'center',
        padding: 15
    },
    logo: {
        maxWidth: '100%'
    },
    steps: {
        listStyle: 'none',
        marginBottom: 15,
        '& li': {
            marginBottom: 10,
            '& i': {
                fontSize: 30,
                verticalAlign: 'middle',
                marginRight: 10
            }
        }

    }
});

const Start = () => {
    const classes = useStyles();
    let history = useHistory();
    let visited = localStorage.getItem(config.keys.visitedStartPage);

    const handleclick = () => {
        localStorage.setItem(config.keys.visitedStartPage, 'true');
        history.push('/auth')
    }
    if (visited) return <Redirect to='/auth' />
    return (
        <div id='page-start' className={classes.startPage}>
            <img className={classes.logo} src={logoImage} />
            <h1 className='mb-15'>وب اپلیکیشن <strong>گل یا پوچ</strong> را به موبایل خود اضافه کنید</h1>
            <ul className={classes.steps}>
                <li><i className='zmdi zmdi-share'></i>1- در نوار بالا روی دکمه Share تپ کنید</li>
                <li><i className='zmdi zmdi-plus-square'></i>2- در منوی باز شده روی گزینه Add to Home Screen تپ کنید</li>
                <li><i className='zmdi zmdi-plus-square'></i>3- در مرجله بعد در قسمت بالا روی  Add تپ کنید</li>
            </ul>
            <Button onClick={handleclick} className='w-100' variant="contained" color="primary">
                {strings.underestood}
            </Button>
        </div>
    );
};

export default Start;