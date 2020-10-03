import http from '../core/network';
import addr from '../core/network/addr';
import config from '../config';
import strings from '../core/strings';

export default class productSrv {
    static async get(ignoreStorage) {

        let data = localStorage.getItem(config.keys.products);
        if (data && !ignoreStorage) {
            let info = JSON.parse(data);
            return info;
        }
        let call = await http.get(addr.getProducts);
        if (!call.isSuccessful) return call;

        localStorage.setItem(config.keys.products, JSON.stringify(call));
        return call;
    }
    static async getSingle(id, ignoreStorage) {
        function find(data) {
            let result = data.result.find(x => x.productOfferId == id);
            return {
                result: result,
                isSuccessful: result ? true : false,
                message: result ? null : strings.recordNotFound
            };
        }
        let data = localStorage.getItem(config.keys.products);
        if (data && !ignoreStorage) return find(JSON.parse(data));

        let call = await http.get(addr.getProducts);
        if (!call.isSuccessful) return call;

        localStorage.setItem(config.keys.products, JSON.stringify(call));
        return find(call);
    }

    static async purchase(info){
        let call = await http.post(addr.purchaseProduct, info);
        if (!call.isSuccessful) return call;
        return call;
    }

}