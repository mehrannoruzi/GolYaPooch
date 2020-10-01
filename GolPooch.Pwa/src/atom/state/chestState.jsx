import { atom } from 'recoil';

const chestState = atom({
    key: 'chestState',
    default: {
        purchaseId: ''
    }
});
export default chestState;