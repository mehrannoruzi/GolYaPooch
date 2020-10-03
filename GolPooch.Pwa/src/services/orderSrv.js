import http from '../core/network';
import addr from '../core/network/addr';
import { encrypt, decrypt } from '../core/utils';
import config from '../config';

export default class orderSrv {
    static async getChances() {
        return {
            isSuccessful: true,
            result: {
                beforeChance: 12,
                addedChance: 3,
                chance: 15
            }
        };
    }

    static async getProductInfo(){
        return {
            isSuccessful: true,
            result: {
                imageUrl: 'http://cdn.golpooch.com/Assets/mdpi/Winner.png',
                name: 'اکانت 6 ماهه فیلیمو',
                price:550000,
                chance: 15
            }
        };
    }
}