import React from 'react';
import { Switch, useRouteMatch } from 'react-router-dom';
import StorePage from '../pages/store';
import ActivitiesPage from '../pages/activities';
import PrivateRoute from '../atom/comps/PrivateRoute';
import { Grid, AppBar, makeStyles, BottomNavigation, BottomNavigationAction, IconButton, Typography } from '@material-ui/core';
import nLState from '../atom/state/nLState';
import { useRecoilState } from 'recoil';
import { useHistory } from "react-router-dom";
import { Store, HowToReg, AddBox, CardGiftcard, Settings, Mail, Notifications, AccountCircle } from '@material-ui/icons';

const navs = [
    {
        label: 'فروشگاه',
        icon: Store,
        path: 'store'
    },
    {
        label: 'فعالیت ها',
        icon: HowToReg,
        path: 'activities'
    },
    {
        label: 'بسته ها',
        icon: AddBox,
        path: 'packages'
    },
    {
        label: 'برنده ها',
        icon: CardGiftcard,
        path: 'winners'
    },
    {
        label: 'تنظیمات',
        icon: Settings,
        path: 'settings'
    }
];

const useStyles = makeStyles({
    layoutNL: {
        minHeight: '100vh',
        paddingBottom: '60px',
        boxSizing: 'border-box',
        '& header': {
            '& .c-col': {
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',

                '& .hx': {
                    fontSize: '1.4rem'
                },
            },
            '& .l-col': {
                display: 'flex',
                justifyContent: 'flex-end'
            }

        }
    },
    btnNavs: {
        width: '100%',
        boxShadow: '0px -1px 3px 0px rgba(0,0,0,0.5)',
        position: 'fixed',
        bottom: 0,
        left: 0,
        right: 0,
        '& button': {
            minWidth: 'auto',
            maxWidth: 'auto'
        },
        '& .MuiBottomNavigationAction-label': {
            fontSize: '0.9rem',
            whiteSpace: 'nowrap',
            fontFamily: "iransans"
        }
    }
});

const NavigationLayout = () => {
    let { path, url } = useRouteMatch();
    let history = useHistory();
    const classes = useStyles();
    const [rState, setNLState] = useRecoilState(nLState);

    return (
        <div id='layout-nl' className={classes.layoutNL}>
           {/* ---------------
            --HEADER
            ---------------*/}
            <AppBar position="static">
                <Grid container className={classes.root} spacing={0}>
                    <Grid item xs={4}>
                        <IconButton
                            edge="end"
                            aria-label="account of current user"
                            aria-haspopup="true"
                            onClick={() => history.push('/bl/profile')}
                            color="inherit">
                            <AccountCircle fontSize="large" />
                        </IconButton>
                    </Grid>
                    <Grid item xs={4} className='c-col'>
                        <h1 className='hx'> {navs[rState.activeBotton].label}</h1>
                    </Grid>
                    <Grid item xs={4} className='l-col'>
                        <IconButton edge="start" aria-label="show 4 new mails" color="inherit">
                            <Mail fontSize="large" />
                        </IconButton>
                        <IconButton edge="start" color="inherit">
                            <Notifications fontSize="large" />
                        </IconButton>
                    </Grid>
                </Grid>
            </AppBar>
            {/* ---------------
            --ROUTES
            ---------------*/}
            <Switch>
                <PrivateRoute exact path={`${path}/store`} component={StorePage} />
                <PrivateRoute exact path={`${path}/activities`} component={ActivitiesPage} />
            </Switch>
            {/* ---------------
            --BUTTONS
            ---------------*/}
            <div className={classes.btnNavs}>
                <Grid container spacing={0}>
                    <Grid item xs={12}>
                        <BottomNavigation
                            value={rState.activeBotton}
                            onChange={(event, newValue) => {
                                setNLState({ ...rState, activeBotton: newValue });
                                let nav = navs[newValue];
                                history.push(`${url}/${nav.path}`);
                            }}
                            showLabels
                        >
                            {navs.map((n, idx) => (<BottomNavigationAction key={idx} label={n.label} icon={<n.icon fontSize="large" />} />))}
                        </BottomNavigation>
                    </Grid>
                </Grid>
            </div>

        </div>
    );
}
export default NavigationLayout;