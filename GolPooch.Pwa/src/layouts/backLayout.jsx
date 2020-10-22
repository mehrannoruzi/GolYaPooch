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
import TicketList from '../pages/ticket/ticketList';
import Ticket from '../pages/ticket';
import Transactions from '../pages/transactions';
 
const useStyles = makeStyles({
    layoutBL: {
        minHeight: '100vh',
        boxSizing: 'border-box',
        '& header': {
            backgroundColor: '#172434',
            color: '#FFF',
            height: 50,
            justifyContent: 'center',
            '& .title': {
                fontSize: '12px',
                fontWeight: "normal",
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
                <PrivateRoute exact path={`${path}/addTicket`} component={Ticket} />
                <PrivateRoute exact path={`${path}/ticketList`} component={TicketList} />
                <PrivateRoute exact path={`${path}/transactions`} component={Transactions} />
            </Switch>
        </div >

    );
}
export default BackLayout;