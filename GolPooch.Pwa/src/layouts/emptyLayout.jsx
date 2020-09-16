import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import Authorization from '../pages/auth';

const EmptyLayout = () => {
    let { path, url } = useRouteMatch();
    return (
        <Switch>
            <Route exact path={`${path}/auth`} component={Authorization} />
        </Switch>
    );
}
export default EmptyLayout;