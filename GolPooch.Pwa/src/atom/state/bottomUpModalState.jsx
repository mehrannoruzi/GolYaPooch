import { atom } from 'recoil';

const buttomUpModalState = atom({
    key: 'buttomUpModal',
    default: {
        open: false,
        title: '',
        children: null,
        showHeader: true,
        props: {}
    }
});
export default buttomUpModalState;