import { selector } from 'recoil';
import { decrypt } from '../../core/utils';
import config from '../../config';

export default selector({
    key: 'token',
    get: ({ get }) => {
        let tokenStr = localStorage.getItem(config.keys.token);
        if(tokenStr) return decrypt(tokenStr);
        return null;
    }
  });