import { atom } from 'recoil';

const buttomUpModalState = atom({
    key: 'buttomUpModal',
    default: {
        open: false,
        title:'',
        children: null
    }
});
export default buttomUpModalState;