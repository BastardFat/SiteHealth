import { cloneDeep } from 'lodash';

const SitesReducers = {
    RefreshSites(state, payload) {
        let newstate = cloneDeep(state);
        newstate.Sites = payload;
        return newstate;
    }
}

export default SitesReducers;