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
import ticketSrv from './services/ticketSrv';
import { useRecoilState } from 'recoil';
import nLAtom from './atom/state/nLState';

export default function () {
    const [nLState, setNLState] = useRecoilState(nLAtom);
    const getInitInfo = async () => {
        if (localStorage.getItem(config.keys.token) == null)
            return;

        let getNewNotifCount = await notificationSrv.getNotReadCount();
        console.log(getNewNotifCount);
        if (getNewNotifCount.isSuccessful)
            setNLState({ ...nLState, newNotificationsCount: getNewNotifCount.result });
        let getTicketCount = await ticketSrv.getNotReadCount();
        if (getTicketCount.isSuccessful)
            setNLState({ ...nLState, newTicketCount: getTicketCount.result });
    };

    useEffect(() => {
        if (navigator && navigator.serviceWorker)
            navigator.serviceWorker.addEventListener("message", (payload) => {
                console.log(payload);
                let notif = payload.data['firebase-messaging-msg-data'].notification;
                navigator.serviceWorker.ready.then(registration => {
                    registration.showNotification(notif.title, payload.data['firebase-messaging-msg-data'].notification);
                });
            });
        localStorage.removeItem(config.keys.banners);
        localStorage.removeItem(config.keys.chests);
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
