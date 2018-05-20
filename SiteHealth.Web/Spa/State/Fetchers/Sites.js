import Api from '../../Utils/Api';
import { ActionTypes } from '../Actions'
const SitesFetchers = {
    RefreshSites(dispatch) {
        return (page, search) => {
            dispatch(ActionTypes.Sites.RefreshSites.create(new window.NOT_LOADED()));
            Api.Data.GetSites(page, search)
                .then(data => dispatch(ActionTypes.Sites.RefreshSites.create(data)))
                .catch(error => dispatch(ActionTypes.Sites.RefreshSites.create(new window.ERRORED(error))));
        }
    }
}

export default SitesFetchers;