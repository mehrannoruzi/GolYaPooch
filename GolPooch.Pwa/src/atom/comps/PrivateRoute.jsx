
import React from 'react';
import { Route, Redirect, useHistory } from 'react-router-dom';
import authSrv from '../../services/authSrv';

const PrivateRoute = ({ Component: Component, ...rest }) => {
    let history = useHistory();
    if (!authSrv.isAuthenticated()) history.push("/el/auth");
    return (<Route {...rest} render={({ props }) => <Component {...props} />} />
    );
}
export default PrivateRoute;