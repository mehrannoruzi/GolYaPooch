import { selector } from 'recoil';
import { decrypt } from './../../core/utils';
import config from './../../config';

const userInfo = selector({
    key: 'userInfo',
    get: ({ get }) => {
        let userStr = localStorage.getItem(config.keys.user);
        if(userStr) return decrypt(userStr);
        return null;
    }
  });