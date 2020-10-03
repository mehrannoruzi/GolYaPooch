import { atom } from 'recoil';

const buttomUpModalState = atom({
    key: 'fullButtomUpModal',
    default: {
        open: false,
        title: '',
        children: null,
        props: {}
    }
});
export default buttomUpModalState;