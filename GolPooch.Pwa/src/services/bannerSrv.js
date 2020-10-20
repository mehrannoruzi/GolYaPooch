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
        let clickedBanners = this.getClickedBanners();
        let info = [];
        if (data && !ignoreStorage) info = JSON.parse(data);
        else{
            let call = await http.get(addr.getBanners);
            if (!call.isSuccessful) return call;
            info = call.result;
            localStorage.setItem(config.keys.banners, JSON.stringify(call.result));
        }
        return {
            isSuccessful: true,
            result: info.filter(x => x.page.name === pageName
                && x.locationType === bannerLocation[location]
                && (x.displayType === 1 ? clickedBanners.indexOf(x.bannerId) === -1 : true))
        };
    }
    
    static getClickedBanners() {
        let storedBanners = localStorage.getItem(config.keys.clickedBanners);
        return storedBanners ? JSON.parse(storedBanners) : [];
    }
    static handleClick(item) {
        if (item.displayType === 1) {
            let bannerIds = this.getClickedBanners();
            bannerIds.push(item.bannerId);
            localStorage.setItem(config.keys.clickedBanners, JSON.stringify(bannerIds));
        }
    }

}