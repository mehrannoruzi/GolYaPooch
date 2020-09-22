importScripts("https://www.gstatic.com/firebasejs/7.21.0/firebase-app.js");
importScripts("https://www.gstatic.com/firebasejs/7.21.0/firebase-messaging.js");
// importScripts('https://www.gstatic.com/firebasejs/5.7.2/firebase-app.js');
// importScripts('https://www.gstatic.com/firebasejs/5.7.2/firebase-messaging.js');

var CACHE_NAME = 'golpooch-catch-1';
var urlsToCache = [
  '/',
  './favicon.ico',
];
// const baseUrl = 'https://pharma.hillavas.com';
// const apiUrl = 'https://pharma.hillavas.com/api/';

const baseUrl = 'localhost:3000';
const apiUrl = 'https://localhost:44367/';

const firebaseConfig = {
  apiKey: "AIzaSyAKNbXvUP69rqFGFNMtBKb2CNq5SLjBw_k",
  authDomain: "golpooch-814c3.firebaseapp.com",
  databaseURL: "https://golpooch-814c3.firebaseio.com",
  projectId: "golpooch-814c3",
  storageBucket: "golpooch-814c3.appspot.com",
  messagingSenderId: "225229029648",
  appId: "1:225229029648:web:3178f60ed29fd37a21a595",
  measurementId: "G-52X9ZF9Y3X"
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