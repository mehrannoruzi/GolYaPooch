importScripts("https://www.gstatic.com/firebasejs/6.6.2/firebase-app.js");
importScripts("https://www.gstatic.com/firebasejs/6.6.2/firebase-messaging.js");
// importScripts('https://www.gstatic.com/firebasejs/5.7.2/firebase-app.js');
// importScripts('https://www.gstatic.com/firebasejs/5.7.2/firebase-messaging.js');

var CACHE_NAME = 'golpooch-catch-1';
var urlsToCache = [
  '/',
  './favicon.ico',
];

//--development
const baseUrl = 'http://localhost:3000/';
const firebaseConfig =  {
    apiKey: "AIzaSyDAx5O8Te76lUHFEEfTx7URneZBEu-Stuc",
    authDomain: "golyapooch-70712.firebaseapp.com",
    databaseURL: "https://golyapooch-70712.firebaseio.com",
    projectId: "golyapooch-70712",
    storageBucket: "golyapooch-70712.appspot.com",
    messagingSenderId: "873210377614",
    appId: "1:873210377614:web:284744c8f3e250cf80e0df",
    measurementId: "G-27TEL4NS4E"
  };
//--production
// const baseUrl = 'https://golpooch.com';
// const firebaseConfig = {
//   apiKey: "AIzaSyDAx5O8Te76lUHFEEfTx7URneZBEu-Stuc",
//   authDomain: "golyapooch-70712.firebaseapp.com",
//   databaseURL: "https://golyapooch-70712.firebaseio.com",
//   projectId: "golyapooch-70712",
//   storageBucket: "golyapooch-70712.appspot.com",
//   messagingSenderId: "873210377614",
//   appId: "1:873210377614:web:284744c8f3e250cf80e0df",
//   measurementId: "G-27TEL4NS4E"
// };

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
      return registration.showNotification("my notification title");
    });
  return promiseChain;
});

self.addEventListener("notificationclick", function (event) {
  console.log(event);
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