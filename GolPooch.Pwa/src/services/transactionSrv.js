import http from '../core/network';
import addr from '../core/network/addr';

export default class notificationSrv {

    static async get(pageSize, pageNumber) {
        return await http.get(addr.getTransactions(pageSize, pageNumber));
    }
}