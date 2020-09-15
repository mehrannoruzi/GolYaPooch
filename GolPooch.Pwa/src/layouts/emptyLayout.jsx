import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import Authorization from '../pages/auth';
import Start from '../pages/start';

const EmptyLayout = () => {
    let { path, url } = useRouteMatch();
    console.log('EmptyLayout');
    return (
        <Switch>
            <Route exact path={`${path}/auth`} component={Authorization} />
        </Switch>
    );
}
export default EmptyLayout;