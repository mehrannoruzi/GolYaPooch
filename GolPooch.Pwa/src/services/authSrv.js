import http from '../core/network';
import addr from '../core/network/addr';
import { encrypt, decrypt } from '../core/utils';
import config from '../config';

export default class authSrv {
    static async login(mobNum) {
        let getCode = await http.post(addr.auth_getCode, {
            mobileNumber: parseInt(mobNum)
        });
        console.log(getCode)
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
}