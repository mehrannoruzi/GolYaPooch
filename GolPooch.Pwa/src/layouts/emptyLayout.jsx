﻿import React from 'react';
import { Route, Switch } from 'react-router-dom';
import Authorization from '../pages/auth';

const EmptyLayout = () => {
    return (
            <Switch>
                <Route exact path="/" component={Authorization} />
            </Switch>
    );
}
export default EmptyLayout;