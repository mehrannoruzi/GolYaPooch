const dev = {
    apiGateway: {
        API_KEY: "5E5AE551-D93E-4801-BC02-22A5CEB71F08",
        URL: "http://localhost:5032/"
    },
    salt: 'shahrooz_bazrafshan',
    keys: {
        user: 'user_info'
    }
};

const prod = {
    apiGateway: {
        API_KEY: "5E5AE551-D93E-4801-BC02-22A5CEB71F08",
        URL: "http://golpoosh.avanod.com/"
    },
    salt: 'shahrooz_bazrafshan',
    keys: {
        user: 'user_info'
    }
};

const config = process.env.REACT_APP_STAGE === 'production'
    ? prod
    : dev;

export default {
    LOGIN_PAGE: "/login",
    ...config
};