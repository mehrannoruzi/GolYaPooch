import React from 'react';
import { AppBar, Container, Grid, makeStyles, IconButton } from '@material-ui/core';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import ArrowBackIosIcon from '@material-ui/icons/ArrowBackIos';
import { useRecoilValue } from 'recoil';
import bLState from '../atom/state/bLState';
import Profile from '../pages/profile';
import ProductPage from '../pages/product';
import { useHistory } from "react-router-dom";
import strings from '../core/strings';
import PrivateRoute from '../atom/comps/PrivateRoute';

const useStyles = makeStyles({
    layoutBL: {
        minHeight: '100vh',
        boxSizing: 'border-box',
        '& header': {
            backgroundColor: '#fff',
            color: '#666',
            '& .title': {
                fontSize: '13px',
                padding: '12px 0'
            },
            '& .l-col': {
                display: 'flex',
                justifyContent: 'flex-end',
                '& .MuiButtonBase-root': {
                    padding: '12px 0'
                }
            }
        }

    }
});


const BackLayout = () => {
    let { path, url } = useRouteMatch();
    let history = useHistory();
    const classes = useStyles();
    const blState = useRecoilValue(bLState);
    return (
        <div id='layout-bl' className={classes.layoutBL}>
            <AppBar position="static">
                <Container>
                    <Grid container className={classes.root} spacing={0}>
                        <Grid item xs={6} className='r-col'>
                            <h1 className='title'>{blState.title}</h1>
                        </Grid>
                        <Grid item xs={6} className='l-col'>
                            <IconButton edge="start" color="inherit" onClick={() => history.goBack()}>
                                <small>{strings.return}</small>
                                <ArrowBackIosIcon />
                            </IconButton>
                        </Grid>
                    </Grid>
                </Container>

            </AppBar>
            <Switch>
                <PrivateRoute exact path={`${path}/profile`} component={Profile} />
                <PrivateRoute exact path={`${path}/product/:id`} component={ProductPage} />
            </Switch>
        </div >

    );
}
export default BackLayout;