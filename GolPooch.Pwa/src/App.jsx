import React, { useEffect } from 'react';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import { useSetRecoilState } from 'recoil';
import EmptyLayout from './layouts/emptyLayout';
import NavigationLayout from './layouts/navigationLayout';
import BackLayout from './layouts/backLayout';
import Toast from './atom/comps/Toast';
import BottomUpModal from './atom/comps/BottomUpModal';
import Start from './pages/start';
import config from './config';
import notificationSrv from './services/notificationSrv';
import bottomUpModalAtom from './atom/state/bottomUpModalState';
import offlineModal from './atom/comps/offlineModal';

export default function () {
    //Recoil
    const setModalState = useSetRecoilState(bottomUpModalAtom);

    const _handleConnectionChange = () => {
        if (!navigator.onLine) {
            setModalState((state) => ({ open: true, children: offlineModal }));
        }
        else  setModalState((state) => ({ open: false, children: null }));
        console.log( navigator.onLine );
    }

    useEffect(() => {
        if (navigator && navigator.serviceWorker)
            navigator.serviceWorker.addEventListener("message", (payload) => {
                let notif = payload.data['firebase-messaging-msg-data'].notification;
                let notifData = payload.data['firebase-messaging-msg-data'].data;
                navigator.serviceWorker.ready.then(async (registration) => {
                    registration.showNotification(notif.title, { ...notif, data: notifData });
                    await notificationSrv.deliver(notifData.NotificationId)
                });
            });
        localStorage.removeItem(config.keys.banners);
        localStorage.removeItem(config.keys.chests);
        //Network Listeners
        _handleConnectionChange();
        window.addEventListener('online', _handleConnectionChange);
        window.addEventListener('offline', _handleConnectionChange);
        return () => {
            window.removeEventListener('online', _handleConnectionChange);
            window.removeEventListener('offline', _handleConnectionChange);
        }
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
