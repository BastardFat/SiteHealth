
import Home from '../Pages/Home.jsx';
import EditSite from '../Pages/EditSite.jsx';

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
    }
];

const MappedRoutes = Routes.reduce((obj, route) => { obj[route.name] = route; return obj; }, {});

export { Routes, MappedRoutes }