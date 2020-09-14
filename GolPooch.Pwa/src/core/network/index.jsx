import Axios from 'axios';
import config from '../../config';
import strings from './../strings';
function parseBody(response) {
    if (response.status === 200)
        return response;
    else
        return Promise.reject(response);
}
let instance = Axios.create({
    withCredentials: true,
    timeout: 60000,
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Access-Control-Allow-Credentials': 'true',
    }
});
instance.interceptors.request.use((conf) => {
    conf.headers.apiKey = config.apiGateway.API_KEY;
    return conf;
}, error => Promise.reject(error));

instance.interceptors.response.use((response) => {
    return parseBody(response)
}, error => {
    if (error.config.hasOwnProperty('errorHandle') && error.config.errorHandle === false)
        return Promise.reject(error);
    if (error.response == undefined)
        return Promise.reject(error);

    if (error.response.status == 401)
        window.location.href = config.LOGIN_PAGE;
    if (error.response.status == 500)
        return { IsSuccessful: false, Message: strings.unknownError };
    return Promise.reject(error);
});

export default instance