import { atom } from 'recoil';

const authPageState = atom({
    key: 'authPageState',
    default: {
        activePanel: 'login',
        mobileNumber: '',
        transactionId: null,
        inputFocused: false
    }
});
export default authPageState;