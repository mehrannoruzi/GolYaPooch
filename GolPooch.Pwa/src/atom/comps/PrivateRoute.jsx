
import React from 'react';
import { Route, Redirect, useHistory } from 'react-router-dom';
import userSrv from '../../services/userSrv';

const PrivateRoute = ({ Component: Component, ...rest }) => {
    let history = useHistory();
    if (!userSrv.isAuthenticated()) history.push("/el/auth");
    return (<Route {...rest} render={({ props }) => <Component {...props} />} />);
}
export default PrivateRoute;