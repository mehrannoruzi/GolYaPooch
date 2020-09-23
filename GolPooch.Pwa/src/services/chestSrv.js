import http from '../core/network';
import addr from '../core/network/addr';
import config from '../config';
import strings from '../core/strings';

export default class chestSrv {
    static async get(ignoreStorage) {

        let data = localStorage.getItem(config.keys.chests);
        if (data && !ignoreStorage) {
            let info = JSON.parse(data);
            return info;
        }
        let call = await http.get(addr.getChests);
        if (!call.isSuccessful) return call;

        localStorage.setItem(config.keys.chests, JSON.stringify(call));
        return call;
    }

    static async getSingle(id, ignoreStorage) {
        function find(data) {
            let result = data.result.find(x => x.productOfferId == id);
            console.log(result);
            return {
                result: result,
                isSuccessful: result ? true : false,
                message: result ? null : strings.recordNotFound
            };
        }
        let data = localStorage.getItem(config.keys.chests);
        if (data && !ignoreStorage) return find(JSON.parse(data));

        let call = await http.get(addr.chests);
        if (!call.isSuccessful) return call;

        localStorage.setItem(config.keys.products, JSON.stringify(call));
        return find(call);
    }

}