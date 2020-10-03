import http from '../core/network';
import addr from '../core/network/addr';

export default class chanceSrv {
    static async getPurchase(pageSize, pageNumber) {
        let call = await http.get(addr.getPurchase(pageSize, pageNumber));
        if (!call.isSuccessful) return call;
        return call;
    }
}