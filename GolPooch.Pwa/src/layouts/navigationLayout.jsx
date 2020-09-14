import React from 'react';
import { Route, Switch } from 'react-router-dom';
import Store from '../pages/store';

const NavigationLayout = () => {
    return (
        <>
            <Switch>
                <Route path="/store" component={Store} />
            </Switch>
            <div>
                bottom navigation
            </div>
        </>
    );
}
export default NavigationLayout;