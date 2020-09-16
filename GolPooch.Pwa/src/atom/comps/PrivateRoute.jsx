
import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import authSrv from '../../services/authSrv';

export default ({ Component: Component, ...rest }) => {
    return (<Route
        {...rest}
        render={({ props }) =>
            authSrv.isAuthenticated() ? (
                <Component {...props} />
            ) : (
                    <Redirect
                        to={{
                            pathname: "/",
                            state: { from: props.location }
                        }}
                    />
                )
        }
    />
    );
}