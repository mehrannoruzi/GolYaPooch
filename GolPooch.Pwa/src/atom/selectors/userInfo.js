import { selector } from 'recoil';
import config from '../../config';

export default selector({
    key: 'userInfo',
    get: ({ get }) => {
        let userInfoStr = localStorage.getItem(config.keys.userInfo);
        if(userInfoStr) return JSON.parse(userInfoStr);
        return null;
    }
  });