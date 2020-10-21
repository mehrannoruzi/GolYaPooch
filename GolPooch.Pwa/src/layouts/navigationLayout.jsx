import React, { useEffect } from 'react';
import { Switch, useRouteMatch } from 'react-router-dom';
import StorePage from '../pages/store';
import ActivitiesPage from '../pages/activities';
import ChestsPage from '../pages/chests';
import SettingsPage from '../pages/settings';
import Winners from '../pages/winners';
import PrivateRoute from '../atom/comps/PrivateRoute';
import { Grid, AppBar, makeStyles, BottomNavigation, BottomNavigationAction, IconButton, Badge, Container } from '@material-ui/core';
import nLState from '../atom/state/nLState';
import { useRecoilState } from 'recoil';
import { useHistory } from "react-router-dom";
import { FiUser, FiMessageSquare, FiPlusSquare } from 'react-icons/fi';
import { GoListOrdered } from 'react-icons/go';
import { RiNotification3Line, RiDashboardFill, RiBarChart2Line, RiMedalLine, RiShoppingBasket2Line } from 'react-icons/ri';
import { AiOutlineSetting } from 'react-icons/ai';
import strings from '../core/strings';
import FullBottomUpModal from '../atom/comps/FullBottomUpModal';
import fullBottomUpModalState from '../atom/state/fullBottomUpModalState';
import NotificationsPage from '../pages/notificationsModal';
import TicketListModal from '../pages/ticket/ticketListModal';
import { BiPurchaseTagAlt } from 'react-icons/bi';
 

const navs = [
    {
        label: strings.pageName_store,
        icon: RiShoppingBasket2Line,
        path: 'store',
        comp: StorePage
    },
    {
        label: strings.pageName_activities,
        icon: BiPurchaseTagAlt,
        path: 'mypackages',
        comp: ActivitiesPage
    },
    {
        label: strings.pageName_chest,
        icon: FiPlusSquare,
        path: 'chests',
        comp: ChestsPage
    },
    {
        label: strings.pageName_leaderboard,
        icon: GoListOrdered,
        path: 'winners',
        comp: Winners
    },
    {
        label: strings.pageName_setting,
        icon: AiOutlineSetting,
        path: 'settings',
        comp: SettingsPage
    }
];

const useStyles = makeStyles({
    root: {
        height: 50,
    },
    layoutNL: {
        minHeight: '100vh',
        paddingBottom: '60px',
        boxSizing: 'border-box',
        paddingTop: 50,
        '& header': {
            position: 'fixed',
            top: 0,
            left: 0,
            right: 0,
            zIndex: 998,
            '& .c-col': {
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',

                '& .hx': {
                    fontSize: '13px',
                    fontWeight: 'normal'
                },
            },
            '& .l-col': {
                display: 'flex',
                justifyContent: 'flex-end'
            },
            '& .btn-profile': {
                paddingLeft: 0
            },
            '& .btn-notifs': {
                paddingRight: 0
            },

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
    //Recoil
    const [rState, setNLState] = useRecoilState(nLState);
    const [modal, setModalState] = useRecoilState(fullBottomUpModalState);

    useEffect(() => {
        let currentPath = window.location.pathname;
        let currentNavIndex = navs.findIndex(x => '/nl/' + x.path == currentPath);
        if (currentNavIndex > -1 && '/nl/' + navs[rState.activeBotton].path !== currentPath) {
            setNLState({ ...rState, activeBotton: currentNavIndex })
        }
    }, []);
    return (
        <div id='layout-nl' className={classes.layoutNL}>
            {/* ---------------
            --HEADER
            ---------------*/}
            <AppBar position="static" className="appbar">
                <Container>
                    <Grid container className={classes.root} spacing={0}>
                        <Grid item xs={4}>
                            <IconButton
                                className='color-white btn-profile'
                                aria-label="account of current user"
                                aria-haspopup="true"
                                onClick={() => history.push('/bl/profile')}>
                                <FiUser />
                            </IconButton>
                        </Grid>
                        <Grid item xs={4} className='c-col'>
                            <h1 className='hx color-white'> {navs[rState.activeBotton].label}</h1>
                        </Grid>
                        <Grid item xs={4} className='l-col'>
                            <Badge color="secondary" className={rState.newTicketCount === 0 ? "emptyBadge" : "successBadge"} variant="dot">
                                <IconButton className='color-white btn-ticket' aria-label="show 4 new mails" onClick={() => setModalState({
                                    ...modal,
                                    open: true,
                                    title: 'تیکت ها',
                                    children: TicketListModal
                                })}>
                                    <FiMessageSquare />
                                </IconButton>
                            </Badge>

                            <Badge color="secondary" className={rState.newNotificationsCount === 0 ? "emptyBadge" : "successBadge"} variant="dot">
                                <IconButton className='color-white btn-notifs' onClick={() => setModalState({
                                    ...modal,
                                    open: true,
                                    title: 'اعلان ها',
                                    children: NotificationsPage
                                })}>
                                    <RiNotification3Line className="hx" />
                                </IconButton>
                            </Badge>
                        </Grid>
                    </Grid>
                </Container>
            </AppBar>
            {/* ---------------
            --ROUTES
            ---------------*/}
            <Switch>
                {navs.map((n, idx) => <PrivateRoute exact path={`${path}/${n.path}`} component={n.comp} key={idx} />)}
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
                            showLabels>
                            {navs.map((n, idx) => (<BottomNavigationAction key={idx} label={n.label} icon={<n.icon fontSize="large" />} />))}
                        </BottomNavigation>
                    </Grid>
                </Grid>
            </div>
            <FullBottomUpModal />
        </div >
    );
}
export default NavigationLayout;