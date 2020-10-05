import http from '../core/network';
import addr from '../core/network/addr';

export default class notificationSrv {
    
    static async get(pageSize, pageNumber) {
        let call = await http.get(addr.getTickets(pageSize, pageNumber));
        return call;
    }

    static async read(id) {
        let call = await http.post(addr.readTicket(id));
        return call;
    }

    static async getNotReadCount() {
        let call = await http.get(addr.getUnReadTicketCount);
        return call;
    }
}