import './/../node_modules/toastr/build/toastr.css';
import 'semantic-ui-css/semantic.min.css';


import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import * as Toastr from 'toastr';
import { HashRouter } from 'react-router-dom';


import App from '../Spa/Pages/App.jsx';
import { Store } from '../Spa/State/Store'

Toastr.options.timeOut = 2000;

window.NOT_LOADED = function () {
    this.started = new Date();
}
window.isNotLoaded = function (object) {
    return object instanceof window.NOT_LOADED;
}

window.ERRORED = function (error) {
    this.error = error;
}
window.isErrored = function (object) {
    return object instanceof window.ERRORED;
}

ReactDOM.render(
    <HashRouter>
        <Provider store={Store}>
            <App />
        </Provider>
    </HashRouter>
    , document.getElementById('app')
);