import { cloneDeep } from 'lodash';

const AdminReducers = {
    SetToken(state, payload) {
        let newstate = cloneDeep(state);
        newstate.Token = payload;
        newstate.IsAdmin = true;
        return newstate;
    }
};

export default AdminReducers;