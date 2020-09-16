import http from '../core/network';
import addr from '../core/network/addr';
import config from '../config';

export default class bannerSrv {
    static async get(key, ignoreStorage) {
        let data = localStorage.getItem(config.keys.banners);
        let now = new Date();
        if (data && ignoreStorage) {
            let info = JSON.parse(data);
            if (now.getTime() < info.exp) {
                return {
                    isSuccessful: true,
                    result: info.result.filter(x => x.page.name == key)
                };
            }
        }
        let call = await http.get(addr.getBanners);
        if (!call.isSuccessful) return call;
        now.setDate(now.getDate() + 3);
        localStorage.setItem(config.keys.banners, JSON.stringify({
            exp: now.getTime(),
            result: call.result
        }))
        return {
            isSuccessful: true,
            result: call.result.filter(x => x.page.name == key)
        };;
    }
}