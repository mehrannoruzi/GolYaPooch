const dev = {
    apiGateway: {
        API_KEY: "F7192259-AC89-4FBB-8033-ACE703B659FB",
        //URL: "https://localhost:44367/",
        //  URL: "https://localhost:44300/",
        URL: "https://api.golpooch.com/"
    },
    salt: 'shahrooz_bazrafshan',
    keys: {
        token: 'token',
        userInfo: 'user_info',
        visitedStartPage: 'visited_start_page',
        banners: 'banners',
        products: 'products',
        chests: 'chests',
        fcmToken: 'fcmToken',
        chances: 'chances',
        clickedBanners: 'clicked_banners'
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
        userInfo: 'user_info',
        visitedStartPage: 'visited_start_page',
        banners: 'banners',
        products: 'products',
        chests: 'chests',
        fcmToken: 'fcmToken',
        chances: 'chances',
        clickedBanners: 'clicked_banners'

    }
};

const config = process.env.REACT_APP_STAGE === 'production'
    ? prod
    : dev;

if (process.env.REACT_APP_STAGE === 'production') {
    console.log = function () { };
}
export default {
    LOGIN_PAGE: "/el/auth",
    ...config,
    confettiConfig: {
        angle: "360",
        spread: "360",
        startVelocity: "29",
        elementCount: "102",
        dragFriction: "0.07",
        duration: "5100",
        stagger: "6",
        width: "10px",
        height: "10px",
        perspective: "739px",
        colors: ["#a864fd", "#29cdff", "#78ff44", "#ff718d", "#fdff6a"]
    };
};