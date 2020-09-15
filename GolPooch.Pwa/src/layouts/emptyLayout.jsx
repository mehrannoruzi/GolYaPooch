import React from 'react';
import { Route, Switch } from 'react-router-dom';
import Authorization from '../pages/auth';
import Start from '../pages/start';

const EmptyLayout = () => {
    return (
        <Switch>
            <Route exact path='/' component={Start} />
            <Route exact path="/auth" component={Authorization} />
        </Switch>
    );
}
export default EmptyLayout;