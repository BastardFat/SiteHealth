import SitesReducers from './Reducers/Sites';

function makeActionCreator(type) {
    return (payload) => ({ type, payload });
}

const ActionTypes = {
    Sites: {
        RefreshSites: { key: 'REFRESH_SITES', create: makeActionCreator('REFRESH_SITES') }
    }
};

const ActionHandlers = {};

ActionHandlers[ActionTypes.Sites.RefreshSites.key] = SitesReducers.RefreshSites;

export { ActionTypes, ActionHandlers };