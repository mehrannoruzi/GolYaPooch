const dev = {
    apiGateway: {
        API_KEY: "F7192259-AC89-4FBB-8033-ACE703B659FB",
        URL: "https://localhost:44367/"
    },
    salt: 'shahrooz_bazrafshan',
    keys: {
        token: 'token',
        visitedStartPage:'visited_start_page'
    }
};

const prod = {
    apiGateway: {
        API_KEY: "F7192259-AC89-4FBB-8033-ACE703B659FB",
        URL: "https://api.golpooch.com/"
    },
    salt: 'shahrooz_bazrafshan',
    keys: {
        token: 'token',
        visitedStartPage:'visited_start_page'
    }
};

const config = process.env.REACT_APP_STAGE === 'production'
    ? prod
    : dev;

export default {
    LOGIN_PAGE: "/auth",
    ...config
};