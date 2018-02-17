routes = [
    {
        path: '/',
        url: './index.html',
    },
    {
        path: '/exchanges/',
        async(routeTo, routeFrom, resolve, reject) {
            config.setRoute(routeTo, routeFrom, resolve, reject, "./pages/exchanges.html");
        },
    },
    {
        path: '/exchangesForm/',
        async(routeTo, routeFrom, resolve, reject) {
            config.setRoute(routeTo, routeFrom, resolve, reject, "./pages/exchangesForm.html");
        },
    },
    {
        path: '/alerts/',
        async(routeTo, routeFrom, resolve, reject) {
            config.setRoute(routeTo, routeFrom, resolve, reject, "./pages/alerts.html");
        },
    },
    {
        path: '/alertsForm/',
        async(routeTo, routeFrom, resolve, reject) {
            config.setRoute(routeTo, routeFrom, resolve, reject, "./pages/alertsForm.html");
        },
    },
    {
        path: '/settings/',
        async(routeTo, routeFrom, resolve, reject) {
            config.setRoute(routeTo, routeFrom, resolve, reject, "./pages/settings.html");
        },
    },

    // Default route (404 page). MUST BE THE LAST
    {
        path: '(.*)',
        url: './pages/404.html',
    },
];
