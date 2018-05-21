
import Home from '../Pages/Home.jsx';
import EditSite from '../Pages/EditSite.jsx';
import ViewSite from '../Pages/ViewSite.jsx';
import Admin from '../Pages/Admin.jsx';

const Routes = [
    {
        name: 'Home',
        url: '/',
        exact: true,
        component: Home
    },
    {
        name: 'AddSite',
        url: '/site/add',
        exact: true,
        component: EditSite
    },
    {
        name: 'EditSite',
        url: '/site/edit/:siteid',
        exact: true,
        component: EditSite
    },
    {
        name: 'ViewSite',
        url: '/site/view/:siteid',
        exact: true,
        component: ViewSite
    },
    {
        name: 'Admin',
        url: '/admin',
        exact: true,
        component: Admin
    }
];

const MappedRoutes = Routes.reduce((obj, route) => { obj[route.name] = route; return obj; }, {});

export { Routes, MappedRoutes };