import config from './../config';
import CryptoJS from 'crypto-js';

export const validate = {
    mobileNumber: function (mobNumber) {
        if (isNaN(mobNumber)) return false;
        else if (!new RegExp(/^9\d{9}$/g).test(mobNumber)) return false;
        else return true;
    },
    email: function (email) {
        const reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return reg.test(String(email).toLowerCase());
    }
};

export const decrypt = (txt) => {
    var bytes = CryptoJS.AES.decrypt(txt, config.salt);
    return bytes.toString(CryptoJS.enc.Utf8);
}

export const encrypt = (infoStr) => CryptoJS.AES.encrypt(infoStr, config.salt).toString();

export function commaThousondSeperator(input) {
    let str = isNaN(input) ? input : input.toString();
    return str.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
};

function gregorian_to_jalali(gy, gm, gd) {
    let g_d_m = [0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334];
    let jy;
    if (gy > 1600) {
        jy = 979;
        gy -= 1600;
    } else {
        jy = 0;
        gy -= 621;
    }
    let gy2 = (gm > 2) ? (gy + 1) : gy;
    let days = (365 * gy) + (parseInt((gy2 + 3) / 4)) - (parseInt((gy2 + 99) / 100)) + (parseInt((gy2 + 399) / 400)) - 80 + gd + g_d_m[gm - 1];
    jy += 33 * (parseInt(days / 12053));
    days %= 12053;
    jy += 4 * (parseInt(days / 1461));
    days %= 1461;
    if (days > 365) {
        jy += parseInt((days - 1) / 365);
        days = (days - 1) % 365;
    }
    let jm = (days < 186) ? 1 + parseInt(days / 31) : 7 + parseInt((days - 186) / 30);
    let jd = 1 + ((days < 186) ? (days % 31) : ((days - 186) % 30));
    return [jy, jm, jd];
}

const jalali_to_gregorian = function (jy, jm, jd) {
    let gy;
    if (jy > 979) {
        gy = 1600;
        jy -= 979;
    } else {
        gy = 621;
    }
    let days = (365 * jy) + ((parseInt(jy / 33)) * 8) + (parseInt(((jy % 33) + 3) / 4)) + 78 + jd + ((jm < 7) ? (jm - 1) * 31 : ((jm - 7) * 30) + 186);
    gy += 400 * (parseInt(days / 146097));
    days %= 146097;
    if (days > 36524) {
        gy += 100 * (parseInt(--days / 36524));
        days %= 36524;
        if (days >= 365) days++;
    }
    gy += 4 * (parseInt(days / 1461));
    days %= 1461;
    if (days > 365) {
        gy += parseInt((days - 1) / 365);
        days = (days - 1) % 365;
    }
    let gd = days + 1;
    let sal_a = [0, 31, ((gy % 4 == 0 && gy % 100 != 0) || (gy % 400 == 0)) ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    let v, gm;
    for (gm = 0; gm < 13; gm++) {
        v = sal_a[gm];
        if (gd <= v) break;
        gd -= v;
    }
    return [gy, gm, gd];
}

export const toPersianDate = function (dt) {
    var resArr = gregorian_to_jalali(dt.getFullYear(), dt.getMonth() + 1, dt.getDate());
    var res = resArr[0] + "/";
    if (resArr[1] < 10)
        res += "0" + resArr[1] + "/";
    else res += resArr[1] + "/";
    if (resArr[2] < 10)
        res += "0" + resArr[2];
    else res += resArr[2];
    return res;
};

export const toGregorianDate = function(dt){
    let arr = dt.split('/');
    return 
}