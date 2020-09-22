import * as firebase from "firebase/app";
import "firebase/messaging";
const initializedFirebaseApp = firebase.initializeApp({
    apiKey: "AIzaSyAKNbXvUP69rqFGFNMtBKb2CNq5SLjBw_k",
    authDomain: "golpooch-814c3.firebaseapp.com",
    databaseURL: "https://golpooch-814c3.firebaseio.com",
    projectId: "golpooch-814c3",
    storageBucket: "golpooch-814c3.appspot.com",
    messagingSenderId: "225229029648",
    appId: "1:225229029648:web:3178f60ed29fd37a21a595",
    measurementId: "G-52X9ZF9Y3X"
});
//const messaging = initializedFirebaseApp.messaging();


let messaging;

// we need to check if messaging is supported by the browser
if (('serviceWorker' in navigator) && ('PushManager' in window) && firebase.messaging.isSupported()) {
    console.log('messaging');
    messaging = firebase.messaging();

    window.addEventListener('load', async () => {
        try {
            const registration = await navigator.serviceWorker.register(process.env.PUBLIC_URL + '/sw.js', {
                updateViaCache: 'none'
            });
            console.log(registration);
            messaging.useServiceWorker(registration);
            messaging.onMessage((payload) => {
                const title = payload.notification.title;
                const options = {
                    body: payload.notification.body,
                    icon: payload.notification.icon,
                    actions: [
                        {
                            action: payload.fcmOptions.link,
                            title: 'Book Appointment'
                        }
                    ]
                };
                registration.showNotification(title, options);           
            });


        }
        catch (err){
            console.log('Worker registration failed: ', err);
        }
       
    });
}

export {
    messaging
};