import { atom } from 'recoil';

const toastState = atom({
    key: 'toast',
    default: {
        open: false,
        severity: 'success',
        message: '',
        autoHideDuration:3000,
        callback: null
    }
});
export default toastState;