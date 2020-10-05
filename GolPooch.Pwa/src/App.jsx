import React, { useEffect } from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import EmptyLayout from './layouts/emptyLayout';
import NavigationLayout from './layouts/navigationLayout';
import BackLayout from './layouts/backLayout';
import Toast from './atom/comps/Toast';
import BottomUpModal from './atom/comps/BottomUpModal';
import Start from './pages/start';
import config from './config';
import notificationSrv from './services/notificationSrv';
import { useRecoilState } from 'recoil';
import nLAtom from './atom/state/nLState';

export default function () {
    const [nLState, setNLState] = useRecoilState(nLAtom);
    localStorage.removeItem(config.keys.banners);
    const getInitInfo = async () => {
        console.log(localStorage.getItem(config.keys.token));
        if (localStorage.getItem(config.keys.token) == null)
            return;

        let getNewNotifCount = await notificationSrv.getNotReadCount();
        console.log(getNewNotifCount);
        if (getNewNotifCount.isSuccessful)
            setNLState({ ...nLState, newNotificationsCount: getNewNotifCount.result })
    };

    useEffect(() => {
        getInitInfo();
    }, []);

    return (
        <Router className="layout">
            <Toast />
            <Switch>
                <Route exact path="/" component={Start} />
                <Route path="/el" component={EmptyLayout} />
                <Route path="/nl" component={NavigationLayout} />
                <Route path="/bl" component={BackLayout} />
            </Switch>

            <BottomUpModal />
        </Router>
    );
}
