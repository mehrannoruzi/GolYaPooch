import React from 'react';
import { Route, Switch, useRouteMatch } from 'react-router-dom';
import Authorization from '../pages/auth';
import AfterGatewayPage from '../pages/afterGateway';
import PrivateRoute from '../atom/comps/PrivateRoute';

const EmptyLayout = () => {
    let { path, url } = useRouteMatch();
    return (
        <div id='el' className='page'>
            <Switch>
                <Route exact path={`${path}/auth`} component={Authorization} />
                <PrivateRoute exact path={`${path}/aftergateway`} component={AfterGatewayPage} />
            </Switch>
        </div>

    );
}
export default EmptyLayout;