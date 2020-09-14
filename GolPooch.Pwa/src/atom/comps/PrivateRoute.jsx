
import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import { useRecoilValue } from 'recoil';
import token from '../selectors/token';

export default  (Component, rest) => {
    const tokenValue = useRecoilValue(token);
    console.log('fired');
    console.log(tokenValue);
    return (<Route
        {...rest}
        render={({ props }) =>
        tokenValue ? (
                <Component {...props} />
            ) : (
                <Component {...props} />
                    // <Redirect
                    //     to={{
                    //         pathname: "/",
                    //         state: { from: props.location }
                    //     }}
                    // />
                )
        }
    />
    );
}