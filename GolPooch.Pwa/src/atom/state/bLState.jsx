import { atom } from 'recoil';

const bLState = atom({
    key: 'bLState',
    default: {
        title: ''
    }
});
export default bLState;