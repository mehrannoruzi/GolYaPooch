const dev = {
    apiGateway: {
        API_KEY: "F7192259-AC89-4FBB-8033-ACE703B659FB",
        URL: "https://localhost:44318/"
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