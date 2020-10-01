import http from '../core/network';
import addr from '../core/network/addr';

export default class chanceSrv {
    static async get(pageSize, pageNumber) {
        let call = await http.get(addr.getChances(pageSize, pageNumber));
        if (!call.isSuccessful) return call;
        return call;
    }
}