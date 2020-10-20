import http from '../core/network';
import addr from '../core/network/addr';
import config from '../config';
import strings from '../core/strings';

export default class productSrv {
    static async get() {
        let call = await http.get(addr.getProducts);
        if (!call.isSuccessful) return call;
        return call;
    }
    static async getSingle(id) {
        function find(data) {
            let result = data.result.find(x => x.productOfferId == id);
            return {
                result: result,
                isSuccessful: result ? true : false,
                message: result ? null : strings.recordNotFound
            };
        }
        let call = await http.get(addr.getProducts);
        if (!call.isSuccessful) return call;
        return find(call);
    }

    static async purchase(info){
        let call = await http.post(addr.purchaseProduct, info);
        if (!call.isSuccessful) return call;
        return call;
    }

}