import http from '../core/network';
import addr from '../core/network/addr';
import { encrypt } from '../core/utils';
import config from '../config';

export default class authSrv {
    static async login(mobNum) {
        let getCode = await http.post(addr.auth_getVerificationCode, {
            mobileNumber: parseInt(mobNum)
        });
        console.log(getCode);
        if (!getCode.IsSuccessful) return { isSuccessful: false, message: getCode.Message };
        return {
            isSuccessful: true,
            result: getCode.Result
        }
    }


    static async verify(transactionId, pinCode) {
        let verifyCode = await http.post(addr.auth_verifyCode, {
            transactionId: transactionId,
            pinCode: parseInt(pinCode)
        });
        console.log(verifyCode);
        if (!verifyCode.IsSuccessful) return { isSuccessful: false, message: verifyCode.Message };
        let token = encrypt(verifyCode.result);
        localStorage.setItem(config.keys.token, token);
        return {
            isSuccessful: true,
            result: verifyCode.Result
        }
    }
}