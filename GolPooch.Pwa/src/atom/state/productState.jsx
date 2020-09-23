import { atom } from 'recoil';

const productState = atom({
    key: 'productState',
    default: {
        gatewatId: ''
    }
});
export default productState;