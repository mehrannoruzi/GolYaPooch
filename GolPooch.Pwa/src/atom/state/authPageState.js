import { atom } from 'recoil';

const authPageState = atom({
    key: 'authPageState',
    default: { activePanel: 'login' }
});
export default authPageState;