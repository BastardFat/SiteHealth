import { createStore } from 'redux';
import { ActionHandlers } from './Actions';

const initialState = {

}

function reducer(state = initialState, action) {
    console.log('action ', action);
    let handler = ActionHandlers[action.type];
    if (!handler) {
        return state;
    }
    return handler(state, action.payload);
}

const Store = createStore(reducer, window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__());

export { Store };