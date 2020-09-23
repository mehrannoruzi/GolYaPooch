import * as firebase from "firebase/app";
import "firebase/messaging";
const config = process.env.NODE_ENV === 'production' ? {
    apiKey: "AIzaSyA495LicYX9kfjOB68whYoq7VoJf7AKkCA",
    authDomain: "test1-a6acf.firebaseapp.com",
    databaseURL: "https://test1-a6acf.firebaseio.com",
    projectId: "test1-a6acf",
    storageBucket: "test1-a6acf.appspot.com",
    messagingSenderId: "156105196573",
    appId: "1:156105196573:web:49848ff8f12ef9760d72f4"
} : {
        apiKey: "AIzaSyDAx5O8Te76lUHFEEfTx7URneZBEu-Stuc",
        authDomain: "golyapooch-70712.firebaseapp.com",
        databaseURL: "https://golyapooch-70712.firebaseio.com",
        projectId: "golyapooch-70712",
        storageBucket: "golyapooch-70712.appspot.com",
        messagingSenderId: "873210377614",
        appId: "1:873210377614:web:284744c8f3e250cf80e0df",
        measurementId: "G-27TEL4NS4E"
    };
const initializedFirebaseApp = firebase.initializeApp(config);


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
        catch (err) {
            console.log('Worker registration failed: ', err);
        }

    });
}

export {
    messaging
};