import React from 'react';
import { Redirect } from 'react-router-dom';
import { makeStyles, Button } from '@material-ui/core';
import logoImage from '../../assets/images/logo.jpeg';
import config from './../../config';
import strings from '../../core/strings';
import { useHistory } from "react-router-dom";
import { Share, TouchApp, AddBox } from '@material-ui/icons';
import userSrv from '../../services/userSrv';

const useStyles = makeStyles({
    productPage: [

    ]
});

const Product = () => {
    const classes = useStyles();
    let history = useHistory();

    return (
        <div id='page-product' className={classes.productPage}>

        </div>
    );
};

export default Product;