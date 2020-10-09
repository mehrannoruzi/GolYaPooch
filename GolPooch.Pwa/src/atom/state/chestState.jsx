import { atom } from 'recoil';

const chestState = atom({
    key: 'chestState',
    default: {
        purchase: null,
        count: 1,
        withoutChance: null,
        disabled: false
    }
});
export default chestState;