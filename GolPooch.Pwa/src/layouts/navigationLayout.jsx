import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import Store from '../pages/store';
import PrivateRoute from '../atom/comps/PrivateRoute';

const NavigationLayout = () => {
    let { path, url } = useRouteMatch();
    return (
        <>
            <Switch>
                <PrivateRoute exact path={`${path}/store`} component={Store} />
            </Switch>
            {/* <div>
                <a href='/pages/store'>go</a>
                bottom navigation
            </div> */}
        </>
    );
}
export default NavigationLayout;