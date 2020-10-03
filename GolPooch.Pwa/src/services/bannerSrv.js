import http from '../core/network';
import addr from '../core/network/addr';
import config from '../config';
const bannerLocation = {
    top: 1,
    bottom: 3
}
export default class bannerSrv {
    static async get(pageName, location, ignoreStorage) {
        let data = localStorage.getItem(config.keys.banners);
        if (data && !ignoreStorage) {
            let info = JSON.parse(data);
            console.log(info);
            return {
                isSuccessful: true,
                result: info.result.filter(x => x.page.name === pageName && x.locationType === bannerLocation[location])
            };
        }
        let call = await http.get(addr.getBanners);
        if (!call.isSuccessful) return call;
        localStorage.setItem(config.keys.banners, JSON.stringify(call));
        return {
            isSuccessful: true,
            result: call.result.filter(x => x.page.name === pageName && x.locationType === bannerLocation[location])
        };;
    }
}