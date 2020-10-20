﻿import React, { useEffect } from 'react';
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
  //Redirect http to https
  //if (window.location.origin.startsWith("http://")) window.location.replace(window.location.href.replace("http://", "https://"));
  return (
    <StylesProvider jss={jss}>
      {props.children}
    </StylesProvider>
  );
}


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