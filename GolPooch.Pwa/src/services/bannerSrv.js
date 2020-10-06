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
            return {
                isSuccessful: true,
                result: info.filter(x => x.page.name === pageName
                    && x.locationType === bannerLocation[location]
                    && (x.displayType === 1 ? !x.opened : true))
                };
            }
        let call = await http.get(addr.getBanners);
        if (!call.isSuccessful) return call;
        call.result = call.result.map(x => ({ ...x, opened: false }));
        localStorage.setItem(config.keys.banners, JSON.stringify(call.result));
        return {
            isSuccessful: true,
            result: call.result.filter(x => x.page.name === pageName && x.locationType === bannerLocation[location])
        };;
    }

    static handleClick(item) {
        if (item.displayType === 1) {
            let bannersResult = JSON.parse(localStorage.getItem(config.keys.banners));
            bannersResult.find(x => x.bannerId === item.bannerId).opened = true;
            console.log(bannersResult);
            localStorage.setItem(config.keys.banners,JSON.stringify(bannersResult));
        }
    }

}