// Dom7
var $$ = Dom7;

// Framework7 App main instance
var app = new Framework7({
    root: '#app', // App root element
    id: 'com.aymk.coinordermanagement', // App bundle ID
    name: config.appShortName,
    theme: 'auto', // Automatic theme detection
    precompileTemplates: true,
    template7Pages: true,
    // App root data
    data: function () {
        return {
            environment: config.envEnums.DEV,
            x_access_token: "",
            user: null,
            routeTo: null,
            catalogues: {
                exchanges: [],
                markets: []
            },
        };
    },
    // App root methods
    methods: {


        getMarketById: (id) => {

            let selectedItem;

            for (let i in app.data.catalogues.markets) {
                if (app.data.catalogues.markets[i]._id === id) {
                    selectedItem = app.data.catalogues.markets[i];
                    break;
                }
            }

            return selectedItem;
        },

        getExchangeById: (id) => {

            let selectedItem;

            for (let i in app.data.catalogues.exchanges) {
                if (app.data.catalogues.exchanges[i]._id === id) {
                    selectedItem = app.data.catalogues.exchanges[i];
                    break;
                }
            }

            return selectedItem;
        },

        joinAlerts: (alertList) => {
            for (let i = 0; i < alertList.length; i++)
                alertList[i].market = app.methods.getMarketById(alertList[i].marketId);
            app.data.user.alerts = alertList;
        },

        joinExchanges: (exchangeList) => {
            for (let i = 0; i < exchangeList.length; i++)
                exchangeList[i].exchange = app.methods.getExchangeById(exchangeList[i].exchangeId);
            app.data.user.exchanges = exchangeList;
        },

        aymkReq: (serviceUrl, requestData, method, preloaderMessage) => {

            return new Promise((resolve, reject) => {

                if (method === null)
                    method = "POST";

                app.dialog.progress(preloaderMessage);

                app.request({
                    url: app.data.environment.apiURL + serviceUrl,
                    method: method,
                    dataType: 'json',
                    crossDomain: false,
                    data: requestData,
                    async: true,
                    beforeSend: function (xhr) {
                        if (app.data.user !== null)
                            xhr.setRequestHeader("x-access-token", app.data.x_access_token);
                    },                   
                    success: function (data, textStatus) {

                        app.dialog.close();
                        // console.log(serviceUrl + " result is: ", data);
                        resolve(data);

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        app.dialog.close();
                        reject(xhr, textStatus, errorThrown);
                    }

                });

            });
        },

        loginCheck: () => {

            if (localStorage.getItem("account")) {

                let user = JSON.parse(localStorage.getItem("account"));

                const promise = app.methods.aymkReq(config.apiMap.global.tokenCheck + "?token=" + user.password, null, "POST", "Signing...");

                promise.then((response) => {

                    if (response.isSuccess) {

                        app.data.x_access_token = user.password;
                        app.methods.getUser(user._id);

                    }
                    else {
                        app.data.user = null;
                    }

                }).catch((xhr, textStatus, errorThrown) => {

                    app.data.user = null;
                    app.dialog.alert("There is unknown error in " + config.apiMap.global.comment, config.errorCaption);

                });
            }
            else {
                app.data.user = null;
            }
        },

        getUser: (userId, callback) => {

            const promise = app.methods.aymkReq(config.apiMap.user.getById + userId, null, "GET", "Getting your alert list...");
            promise.then((response) => {

                if (response.isSuccess) {

                    app.data.user = response.data;
                    app.methods.joinAlerts(app.data.user.alerts);
                    app.methods.joinExchanges(app.data.user.exchanges);

                    app.methods.setBadgeCounts(true);
                    app.methods.setMenu(true);
                }
                if (callback !== null)
                    callback(true);

            }).catch((xhr, textStatus, errorThrown) => {
                config.toast("There is unknown error");
                app.dialog.close();
                if (callback !== null) callback(false);
            });
        },

        getCatalogs: () => {

            const promise = app.methods.aymkReq(config.apiMap.global.catalogs, null, null, "Getting catalogs...");
            promise.then((response) => {

                if (response.isSuccess) {

                    app.data.catalogues.exchanges = response.data.catalogExchanges;
                    app.data.catalogues.markets = response.data.catalogMarkets; 
                    app.methods.loginCheck();
                }

            }).catch((xhr, textStatus, errorThrown) => {
                config.toast("There is unknown error in catalogs");
            });
        },

        setBadgeCounts: (setBadge) => {


            //badgeDefault values
            $$('#menuBadgeAlertHasItem').hide();

            if (setBadge) {
                if (app.data.user.alerts.length > 0) {
                    $$('#menuBadgeAlertCount').html(app.data.user.alerts.length);
                    $$('#menuBadgeAlertHasItem').show();
                }
            }


        },

        setMenu :(isSignIn)=> {
            if(isSignIn===true)
            {
                $$("#menu_signin").hide();
                $$("#menu_signout").show();
                $$("#menu_createAccount").hide();
                $$("#menu_settings").show();
            }
            else{
                $$("#menu_signin").show();
                $$("#menu_signout").hide();
                $$("#menu_createAccount").show();
                $$("#menu_settings").hide();
            }
        },

        logout : ()=>{

            app.data.x_access_token= "";
            app.data.user= null;
            app.data.routeTo= null;
            app.data.catalogues=
                {
                    exchanges: [],
                    markets: []
                };
            localStorage.removeItem("account");

            app.methods.setMenu(false);
            app.router.navigate("/", null);
        }
    },
    on: {
        //pageInit: function (page) {
        //    console.log("pageInit -> name: " + page.name + " from: " + page.from + " - to: " + page.to);
        //},
        //pageMounted: function (page) {
        //    console.log("pageMounted -> name: " + page.name + " from: " + page.from + " - to: " + page.to);
        //},
        pageBeforeIn: function (page) {

            if (page.name === "index" && page.pageFrom.name === "settings") {
                console.log("pageBeforeIn -> name: " + page.name + " from: " + page.pageFrom.name + " - to: " + page.to);

                $$("#accountSummary").html("Name:" + app.data.user.name);
            }
        }
    },
    
    // App routes
    routes: routes,

});

let homeView = app.views.create('#view-home', {
    url: '/'
});

$$('#my-login-screen .login-button').on('click', function () {

    let requestData =
        {
            grant_type: "password",
            Username: $$('#my-login-screen [name="username"]').val(),
            Password: $$('#my-login-screen [name="password"]').val()
        };

    const promise = app.methods.aymkReq(config.apiMap.global.login, requestData, null, "Signing...");

    promise.then((response) => {

        if (response.isSuccess) {
            //app.dialog.alert("Login is succesfully.", config.successCaption);
            app.loginScreen.close('#my-login-screen');

            localStorage.setItem("account", JSON.stringify(response.data));
            app.methods.getUser(response.data._id, (result) => {
                if (app.routeTo)
                    app.router.navigate(app.routeTo);
            });

        }
        else {
            app.dialog.alert(response.Message + " " + response.Detail, config.errorCaption);
        }

    }).catch((xhr, textStatus, errorThrown) => {

        app.dialog.alert("There is unknown error in " + config.apiMap.global.comment, config.errorCaption);
    });

});

$$('#my-register-screen .register-button').on('click', function () {

    let requestData =
        {
            username: $$('#my-register-screen [name="username"]').val(),
            password: $$('#my-register-screen [name="password"]').val(),
            email: $$('#my-register-screen [name="email"]').val()
        };


    const promise = app.methods.aymkReq(config.apiMap.global.register, requestData, "POST", "Getting data...");

    promise.then((response) => {

        if (response.isSuccess) {
            app.dialog.alert("Registration is succesfully.", config.successCaption);
            app.loginScreen.close('#my-register-screen');

            localStorage.setItem("account", JSON.stringify(response.data));
            app.methods.getUser(response.data._id, (result) => {
                if (app.routeTo)
                    app.router.navigate(app.routeTo);
            });

        }
        else {
            app.dialog.alert(response.Message + " " + response.Detail, config.errorCaption);
        }

    }).catch((xhr, textStatus, errorThrown) => {

        app.dialog.alert("There is unknown error in " + config.apiMap.global.comment, config.errorCaption);


    });

});

$$("#menu_signout").on('click', function () {
    app.methods.logout();
});


app.methods.setMenu(false);
app.methods.setBadgeCounts(false);
app.methods.getCatalogs();


