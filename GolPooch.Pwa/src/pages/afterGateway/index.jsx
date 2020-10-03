import React, { useState, useEffect } from 'react';
import { makeStyles, Container, AppBar, IconButton } from '@material-ui/core';
import Success from './comps/success';
import Failed from './comps/failed';
import { useRecoilState } from 'recoil';
import queryString from 'query-string';
import { useLocation } from 'react-router-dom';
import ArrowBackIosIcon from '@material-ui/icons/ArrowBackIos';
import { useHistory } from "react-router-dom";

const useStyles = makeStyles({
    afterGatewayPage: {
        '& header': {
            backgroundColor: 'transparent',
            boxShadow: 'none',
            '& .MuiContainer-root': {
                display: 'flex',
                justifyContent: 'flex-end',
                '& button': {
                    display: 'inline-block',
                    '& svg': {
                        fontSize: 20
                    }
                }
            }

        },
        '&.success': {
            backgroundColor: '#41ce66',
            color: '#fff'
        },
        '&.failed': {
            backgroundColor: 'red',
            color: '#fff'
        }
    }
});

const AfterGateway = (props) => {
    const classes = useStyles();
    const location = useLocation();
    let history = useHistory();
    const values = queryString.parse(location);
    return (
        <div id='page-after-gateway' className={`page ${classes.afterGatewayPage} ${(values.isSuccessful ? 'success' : 'failed')}`}>
            <AppBar position="static">
                <Container>
                    <IconButton edge="start" color="inherit" onClick={() => history.push('/nl/store')}>
                        <ArrowBackIosIcon />
                    </IconButton>
                </Container>
            </AppBar>
            <Container className={classes.products}>

                {values.isSuccessful ? <Success traceId='123456' insertDateSh='1399/07/10' time='20:15' /> : <Failed traceId='123456'/>}
            </Container>

        </div>
    );
};

export default AfterGateway;