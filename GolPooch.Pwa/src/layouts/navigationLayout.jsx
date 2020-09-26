import React from 'react';
import { Switch, useRouteMatch } from 'react-router-dom';
import StorePage from '../pages/store';
import ActivitiesPage from '../pages/activities';
import ChestPage from '../pages/chest';
import SettingsPage from '../pages/settings'
import PrivateRoute from '../atom/comps/PrivateRoute';
import { Grid, AppBar, makeStyles, BottomNavigation, BottomNavigationAction, IconButton, Typography } from '@material-ui/core';
import nLState from '../atom/state/nLState';
import { useRecoilState } from 'recoil';
import { useHistory } from "react-router-dom";
import { FiUser, FiMessageSquare, FiPlusSquare } from 'react-icons/fi';
import { RiNotification3Line, RiDashboardFill, RiBarChart2Line, RiMedalLine } from 'react-icons/ri';
import { AiOutlineSetting } from 'react-icons/ai';
import strings from '../core/strings';

const navs = [
    {
        label: strings.pageName_store,
        icon: RiDashboardFill,
        path: 'store'
    },
    {
        label: strings.pageName_activities,
        icon: RiBarChart2Line,
        path: 'activities'
    },
    {
        label: strings.pageName_chest,
        icon: FiPlusSquare,
        path: 'chest'
    },
    {
        label: strings.pageName_leaderboard,
        icon: RiMedalLine,
        path: 'winners'
    },
    {
        label: strings.pageName_setting,
        icon: AiOutlineSetting,
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
                    fontSize: '14px',
                    fontWeight: 'normal'
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
        boxShadow: '0px 1px 7px 0px rgb(167 167 167 / 50%)',
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
            <AppBar position="static" className="appbar">
                <Grid container className={classes.root} spacing={0}>
                    <Grid item xs={4}>
                        <IconButton
                            edge="end"
                            aria-label="account of current user"
                            aria-haspopup="true"
                            onClick={() => history.push('/bl/profile')}
                            color="inherit">
                            <FiUser />
                        </IconButton>
                    </Grid>
                    <Grid item xs={4} className='c-col'>
                        <h1 className='hx'> {navs[rState.activeBotton].label}</h1>
                    </Grid>
                    <Grid item xs={4} className='l-col'>
                        <IconButton edge="start" aria-label="show 4 new mails" color="inherit">
                            <FiMessageSquare />
                        </IconButton>
                        <IconButton color="inherit" >
                            <RiNotification3Line className="hx" />
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
                <PrivateRoute exact path={`${path}/chest`} component={ChestPage} />
                <PrivateRoute exact path={`${path}/settings`} component={SettingsPage} />
            
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