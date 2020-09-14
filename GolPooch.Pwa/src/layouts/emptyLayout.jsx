import React from 'react';
import { Route, Switch } from 'react-router-dom';
import Authorization from '../pages/auth';

const EmptyLayout = () => {
    return (
        <div className="container">
            <Switch>
                <Route path="/" component={Authorization} />
            </Switch>
        </div>
    );
}
export default EmptyLayout;