import http from '../core/network';
import addr from '../core/network/addr';

export default class gatewaySrv {
    static async get() {
        let call = await http.get(addr.getGateways);
        console.log(call);
        if (!call.isSuccessful) return call;
        return call;
    }
}