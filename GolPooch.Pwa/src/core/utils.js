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
    return JSON.parse(bytes.toString(CryptoJS.enc.Utf8));
}

export const encrypt = (infoObj) => CryptoJS.AES.encrypt(JSON.stringify(infoObj), config.salt).toString();