import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import Profile from '../pages/profile';

const BackLayout = () => {
    let { path, url } = useRouteMatch();
    return (
        <Switch>
            <Route exact path={`${path}/profile`} component={Profile} />
        </Switch>
    );
}
export default BackLayout;