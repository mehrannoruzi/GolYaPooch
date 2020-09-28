import { atom } from 'recoil';

const buttomUpModalState = atom({
    key: 'fullButtomUpModal',
    default: {
        open: false,
        title:'',
        children: null
    }
});
export default buttomUpModalState;