import Axios from 'axios';
import config from '../../config';
import strings from './../strings';
import userSrv from '../../services/userSrv';

function parseBody(response) {
    if (response.status === 200)
        return response.data;
    else
        return Promise.reject(response);
}
let instance = Axios.create({
    timeout: 60000,
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Access-Control-Allow-Credentials': '*',
    }
});
instance.interceptors.request.use((conf) => {
    conf.headers.Token = config.apiGateway.API_KEY;
    let jwt = userSrv.isAuthenticated();
    if (jwt) conf.headers.Authorization = `Bearer ${jwt}`;
    return conf;
}, error => Promise.reject(error));

instance.interceptors.response.use((response) => {
    return parseBody(response)
}, error => {
    console.log(error);
    if (error.config && error.config.hasOwnProperty('errorHandle') && error.config.errorHandle === false)
        return Promise.reject(error);
    if (error.response && error.response.status == 401){
        localStorage.removeItem(config.keys.token);
        window.location.href = config.LOGIN_PAGE;
    }

    return { IsSuccessful: false, Message: strings.unknownError };
    //return Promise.reject(error);
});

export default instance