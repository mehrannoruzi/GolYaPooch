import React, { useEffect } from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import { RecoilRoot } from 'recoil';
import { MuiThemeProvider, createMuiTheme, StylesProvider, jssPreset } from '@material-ui/core/styles';
import './assets/styles/index.css';
import './assets/styles/material-design-iconic-font.min.css';
import './assets/styles/animate.css';
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import { create } from 'jss';
import rtl from 'jss-rtl';

const theme = createMuiTheme({
  direction: 'rtl',
});

// Configure JSS
const jss = create({ plugins: [...jssPreset().plugins, rtl()] });

function RTL(props) {
  useEffect(() => {
    if (navigator && navigator.serviceWorker)
      navigator.serviceWorker.addEventListener("message", (payload) => {
        console.log(payload);
        let notif = payload.data['firebase-messaging-msg-data'].notification;
        navigator.serviceWorker.ready.then(registration => {
          registration.showNotification(notif.title, payload.data['firebase-messaging-msg-data'].notification);
        });
      });

  },[]);
  return (
    <StylesProvider jss={jss}>
      {props.children}
    </StylesProvider>
  );
}

//========================================================
//==== register service worker
//========================================================

// if ('serviceWorker' in navigator) {
//   window.addEventListener('load', function () {
//     //var currentUrl = window.location;
//     navigator.serviceWorker.register(process.env.PUBLIC_URL + '/sw.js', { scope: '/' }).then(function (registration) {
//       console.log('Worker registration successful', registration.scope);
//     }, function (err) {
//       console.log('Worker registration failed', err);
//     }).catch(function (err) {
//       console.log(err);
//     });
//   });
// } else {
//   console.log('Service Worker is not supported by browser.');
// }


ReactDOM.render(
  <RecoilRoot>
    <RTL>
      <MuiThemeProvider theme={theme}>
        <App />
      </MuiThemeProvider>
    </RTL>
  </RecoilRoot>,
  document.getElementById('root'));

//registerServiceWorker();