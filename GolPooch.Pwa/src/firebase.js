import * as firebase from "firebase/app";
import "firebase/messaging";
const config =  {
    apiKey: "AIzaSyDAx5O8Te76lUHFEEfTx7URneZBEu-Stuc",
    authDomain: "golyapooch-70712.firebaseapp.com",
    databaseURL: "https://golyapooch-70712.firebaseio.com",
    projectId: "golyapooch-70712",
    storageBucket: "golyapooch-70712.appspot.com",
    messagingSenderId: "873210377614",
    appId: "1:873210377614:web:284744c8f3e250cf80e0df",
    measurementId: "G-27TEL4NS4E"
  };
// const config = {
//     apiKey: "AIzaSyBn60BEx3DUs4zFRMeYslZs-6PSu1q-9k0",
//     authDomain: "golpooch2.firebaseapp.com",
//     databaseURL: "https://golpooch2.firebaseio.com",
//     projectId: "golpooch2",
//     storageBucket: "golpooch2.appspot.com",
//     messagingSenderId: "79314841229",
//     appId: "1:79314841229:web:658578adcbf41e5250fcc4"
// };


const initializedFirebaseApp = firebase.initializeApp(config);

let messaging;

// we need to check if messaging is supported by the browser
if (('serviceWorker' in navigator) && ('PushManager' in window) && firebase.messaging.isSupported()) {
    messaging = firebase.messaging();
    let registration = null;
    window.addEventListener('load', async () => {
        try {
            registration = await navigator.serviceWorker.register(process.env.PUBLIC_URL + '/sw.js', {
                updateViaCache: 'none'
            });
            messaging.useServiceWorker(registration);
            console.log('firebase');
            messaging.onMessage((payload) => {
                console.log(payload);
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
                if (registration)
                  registration.showNotification(title, options);
              });
        }
        catch (err) {
            console.log('Worker registration failed: ', err);
        }

    });
   

}


export {
    messaging
};