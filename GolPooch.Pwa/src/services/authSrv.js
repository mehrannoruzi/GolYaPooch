import http from '../core/network';
import addr from '../core/network/addr';

export default class authSrv {
    static async login(mobNum) {
        let getCode = await http.post(addr.auth_getVerificationCode, {
            mobileNumber: parseInt(mobNum)
        });
        // let call = await fetch(addr.auth_getVerificationCode, {
        //     'method': 'POST',
        //     'mode': 'cors',
        //     'headers': {
        //         'Content-Type': 'application/json; charset=utf-8;',
        //         'apiKey': "5E5AE551-D93E-4801-BC02-22A5CEB71F08"
        //     },
        //     'body': JSON.stringify({ mobileNumber: parseInt(mobNum) })
        // });
        //let getCode = call.json();
        console.log(getCode);
        if (!getCode.IsSuccessful) return { success: false, message: getCode.Message };
        return {
            success: true,
            result: getCode.Result
        }
    }
}