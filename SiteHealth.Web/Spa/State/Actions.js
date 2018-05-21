import SitesReducers from './Reducers/Sites';
import AdminReducers from './Reducers/Admin';

function makeActionCreator(type) {
    return (payload) => ({ type, payload });
}

const ActionTypes = {
    Sites: {
        RefreshSites: { key: 'REFRESH_SITES', create: makeActionCreator('REFRESH_SITES') }
    },
    Admin: {
        SetToken: { key: 'SET_TOKEN', create: makeActionCreator('SET_TOKEN') }
    }
};

const ActionHandlers = {};

ActionHandlers[ActionTypes.Sites.RefreshSites.key] = SitesReducers.RefreshSites;
ActionHandlers[ActionTypes.Admin.SetToken.key] = AdminReducers.SetToken;

export { ActionTypes, ActionHandlers };