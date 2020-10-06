import { atom } from 'recoil';

const chestState = atom({
    key: 'chestState',
    default: {
        purchaseId: '',
        withoutChance: null
    }
});
export default chestState;