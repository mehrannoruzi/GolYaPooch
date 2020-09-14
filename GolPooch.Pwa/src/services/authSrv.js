import http from '../core/network';
import addr from '../core/network/addr';

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
}