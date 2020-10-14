import { atom } from 'recoil';

const nLState = atom({
    key: 'nLState',
    default: {
        activeBotton: 2,
        newNotificationsCount: 0,
        newTicketCount: 0
    }
});
export default nLState;