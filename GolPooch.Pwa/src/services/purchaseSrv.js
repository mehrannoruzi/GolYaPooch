import http from '../core/network';
import addr from '../core/network/addr';

export default class purchaseSrv {
    static async getActive(pageSize, pageNumber) {
        let call = await http.get(addr.getActivePurchase(pageSize, pageNumber));
        if (!call.isSuccessful) return call;
        return call;
    }
    static async getAll(pageSize, pageNumber) {
        let call = await http.get(addr.getAllPurchase(pageSize, pageNumber));
        if (!call.isSuccessful) return call;
        return call;
    }
}