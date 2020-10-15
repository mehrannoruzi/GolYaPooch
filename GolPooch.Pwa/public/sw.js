importScripts("https://www.gstatic.com/firebasejs/6.6.2/firebase-app.js");
importScripts("https://www.gstatic.com/firebasejs/6.6.2/firebase-messaging.js");
// importScripts('https://www.gstatic.com/firebasejs/5.7.2/firebase-app.js');
// importScripts('https://www.gstatic.com/firebasejs/5.7.2/firebase-messaging.js');

var CACHE_NAME = 'golpooch-catch-0.1.2';
var urlsToCache = [
  '/',
  './favicon.ico',
];

//--development
// const baseUrl = 'http://localhost:3000/';
 //const apiUrl = 'https://localhost:44367/';
// const firebaseConfig = {
//   apiKey: "AIzaSyBn60BEx3DUs4zFRMeYslZs-6PSu1q-9k0",
//   authDomain: "golpooch2.firebaseapp.com",
//   databaseURL: "https://golpooch2.firebaseio.com",
//   projectId: "golpooch2",
//   storageBucket: "golpooch2.appspot.com",
//   messagingSenderId: "79314841229",
//   appId: "1:79314841229:web:658578adcbf41e5250fcc4"
// };

//--production
const baseUrl = 'https://pwa.golpooch.com/';
const apiUrl = 'https://api.golpooch.com/';

const firebaseConfig = {
  apiKey: "AIzaSyDAx5O8Te76lUHFEEfTx7URneZBEu-Stuc",
  authDomain: "golyapooch-70712.firebaseapp.com",
  databaseURL: "https://golyapooch-70712.firebaseio.com",
  projectId: "golyapooch-70712",
  storageBucket: "golyapooch-70712.appspot.com",
  messagingSenderId: "873210377614",
  appId: "1:873210377614:web:284744c8f3e250cf80e0df",
  measurementId: "G-27TEL4NS4E"
};
firebase.initializeApp(firebaseConfig);
const messaging = firebase.messaging();
messaging.setBackgroundMessageHandler(function (payload) {
  const promiseChain = clients
    .matchAll({
      type: "window",
      includeUncontrolled: true,
    })
    .then((windowClients) => {
      for (let i = 0; i < windowClients.length; i++) {
        const windowClient = windowClients[i];
        windowClient.postMessage(payload);
      }
    })
    .then(() => {
      return self.registration.showNotification("my notification title");
    });
  return promiseChain;
});

self.addEventListener('notificationclick', (payload) => {
  console.log(payload);
  let data = payload.notification.data;
  if (data.ActionUrl) {
    clients.openWindow(data.ActionUrl);
  }
  if (data.NotificationId) {
    fetch(`${apiUrl}notification/AddClick?notificationId=${data.NotificationId}`, {
      method: 'POST',
      mode: 'cors'
    })
      .then(function (response) { return response.json(); })
      .then(function (data) {
        console.log(data);
        return data;
      });
  }

  payload.notification.close();
});

// Install a service worker
self.addEventListener('install', e => {
  // Perform install steps
  e.waitUntil(
    caches.open(CACHE_NAME)
      .then(function (cache) {
        console.log('Opened cache');
        return cache.addAll(urlsToCache);
      })
  );
});

self.addEventListener('fetch', e => {
  if (e.request.url.indexOf(baseUrl) > -1 || urlsToCache.indexOf(e.request.url) >= 0) {
    //for shell
    e.respondWith(caches.match(e.request).then(function (cRep) {
      var fRep = null;
      try {
        fRep = fetch(e.request)
          .then(function (response) {
            return caches.open(CACHE_NAME).then(function (cache) {
              cache.put(e.request, response.clone());
              return response;
            });
          });
      }
      catch (e) { }

      return cRep || fRep;
    })
    );
  }
});

// Update a service worker
self.addEventListener('activate', event => {
  var cacheWhitelist = [CACHE_NAME];
  event.waitUntil(
    caches.keys().then(cacheNames => {
      return Promise.all(
        cacheNames.map(cacheName => {
          if (cacheWhitelist.indexOf(cacheName) === -1) {
            return caches.delete(cacheName);
          }
        })
      );
    })
  );
});
