import http from '../core/network';
import addr from '../core/network/addr';
import { encrypt, decrypt } from '../core/utils';
import config from '../config';

export default class userSrv {
    static async login(mobNum) {
        let getCode = await http.post(addr.auth_getCode, {
            mobileNumber: parseInt(mobNum)
        });
        if (!getCode.isSuccessful) return getCode;
        return getCode;
    }


    static async verify(transactionId, pinCode) {
        let verifyCode = await http.post(addr.auth_verifyCode, {
            transactionId: transactionId,
            pinCode: parseInt(pinCode)
        });
        console.log(verifyCode);
        if (!verifyCode.isSuccessful) return verifyCode;
        let token = encrypt(verifyCode.result.token);
        localStorage.setItem(config.keys.token, token);
        return verifyCode;
    }

    static isAuthenticated() {
        let tokenStr = localStorage.getItem(config.keys.token);
        if (tokenStr) return decrypt(tokenStr);
        return null;
    }

    static saveInfo(user) {
        let infoStr = localStorage.getItem(config.keys.userInfo);
        let info = {};
        if (infoStr) info = JSON.parse(infoStr);
        localStorage.setItem(config.keys.userInfo, JSON.stringify({ ...info, ...user }));
    }

    static getInfo() {
        let infoStr = localStorage.getItem(config.keys.userInfo);
        if (infoStr) return JSON.parse(infoStr);
        else return {};
    }

    static async updateAvatar(avatar) {
        var formData = new FormData();
        formData.append("avatar", avatar);
        let upload = await http.post(addr.uploadAvatar, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
        if (!upload.isSuccessful) return upload;
        return upload;

    }

    static async updateProfile(user) {
        console.log(user);
        let update = await http.post(addr.updateProfile, user, { errorHandle: false });
        if (!update.isSuccessful) return update;
        return update;

    }
}