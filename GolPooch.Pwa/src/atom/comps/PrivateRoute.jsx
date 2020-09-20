
import React from 'react';
import { Route, Redirect, useHistory } from 'react-router-dom';
import userSrv from '../../services/userSrv';

const PrivateRoute = (info) => {
    let history = useHistory();
    if (!userSrv.isAuthenticated()) {
        history.push("/el/auth");
    }
    const Comp = info.component;
    let otherProps = {};
    for (let k in info)
        if (k !== 'component')
            otherProps[k] = info[k];
    return (<Route {...otherProps} render={({ props }) => <Comp {...props} />} />);
}
export default PrivateRoute;