import http from '../core/network';
import addr from '../core/network/addr';
import config from '../config';
import { messaging } from '../firebase';

export default class notificationSrv {
    static async requestPermission() {
        if (!messaging) return;
        let token = localStorage.getItem(config.keys.fcmToken);
        if (token) return { isSuccessful: true, result: token };
        messaging.requestPermission()
            .then(async function () {
                token = await messaging.getToken();
                localStorage.setItem(config.keys.fcmToken, token);
                console.log(token);

                let call = await http.post(addr.registerForNotification, { EndpointPushKey: token });
                return { isSuccessful: true, result: token };;
            })
            .catch(function (err) {
                console.log("Unable to get permission to notify.", err);
            });

    }
}