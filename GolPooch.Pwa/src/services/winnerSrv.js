import http from '../core/network';
import addr from '../core/network/addr';

export default class winnerSrv {
    
    static async getLatest(pageSize, pageNumber) {
        return await http.get(addr.getLatestWinners(pageSize, pageNumber));
    }

    static async getMost(pageSize, pageNumber) {
        return await http.get(addr.getMostWinners(pageSize, pageNumber));
    }
}