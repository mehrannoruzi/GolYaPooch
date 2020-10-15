import http from '../core/network';
import addr from '../core/network/addr';
import config from '../config';
import strings from '../core/strings';

export default class chestSrv {
    static async get() {
        let call = await http.get(addr.getChests);
        return call;
    }

    static async getSingle(id, ignoreStorage) {
        function find(data) {
            let result = data.result.find(x => x.chestId === id);
            return {
                result: result,
                isSuccessful: result ? true : false,
                message: result ? null : strings.recordNotFound
            };
        }
        let call = await http.get(addr.getChests);
        if (!call.isSuccessful) return call;
        return find(call);
    }
    static async getChance(chestId) {
        let call = await http.get(addr.getMyChanceCount(chestId));
        return call;
    }
    static async spendChance(info) {
        let call = await http.post(addr.spendChance, info);
        return call;

    }
}