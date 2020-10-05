import { atom } from 'recoil';

const nLState = atom({
    key: 'nLState',
    default: {
        activeBotton: 0,
        newNotificationsCount: 0
    }
});
export default nLState;