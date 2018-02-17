let config = {

    appShortName: "AYMK",
    appFullName: "AYMK Coin Order Management System",
    envEnums: {
        DEV: { value: 0, name: "Development", code: "D", apiURL: "http://localhost:63025/api/" },
        PROD: { value: 1, name: "Production", code: "P", apiURL: "https://aymkapi.herokuapp.com/" }
    },

    errorCaption: "Error",
    successCaption: "Succesfully",
    apiMap:
    {
        
        global: {
            comment: "Global",
            register: constants.services.Global + constants.methodTypes.Register,
            login: "login",
            forgotpassword: constants.services.Global + constants.methodTypes.Forgotpassword,
            catalogs: constants.services.Global + constants.methodTypes.Catalogs,
        },
        account:
        {
            comment: "Account Service",
            getById: constants.services.Account + constants.methodTypes.GetById,
            update: constants.services.Account + constants.methodTypes.Update,
        },
        alert:
        {
            comment: "Alert Service",

            add: constants.services.Alert + constants.methodTypes.Add,
            update: constants.services.Alert + constants.methodTypes.Update,
            delete: constants.services.Alert + constants.methodTypes.Delete,
            getById: constants.services.Alert + constants.methodTypes.GetById,
            getList: constants.services.Alert + constants.methodTypes.Getlist,
            changeStatus: constants.services.Alert + constants.methodTypes.ChangeStatus
        },
        exchange:
        {
            comment: "Exchanges of Account",
            add: "api/account/addExchange/",
            update: "api/account/updateExchange/",
            delete: "api/account/deleteExchange/",
            getById: "api/account/getExchangeById/",
            changeStatus: "api/account/changeStatusExchange/"
        }


    },

    //redirectLoginPage
    redirectLogin: (app, where) => {
        app.routeTo = where;
        app.dialog.create({
            title: 'Sign In',
            text: 'You have to log for this feature.',
            buttons: [
                {
                    text: 'Cancel',

                },
                {
                    text: 'Sign In',
                    onClick: function (dialog, e) {

                        app.loginScreen.open('#my-login-screen');
                    }
                },
            ],
            verticalButtons: false,
        }).open();
    },

    toast: (msg, position = "top") => {
        var toastTop = app.toast.create({
            text: msg,
            position: "center",
            closeTimeout: 1000,
            cssClass: "text-align-left",
            icon: '<i class="fa fa-info-circle"></i>'
        }).open();
    },

    setRoute: (routeTo, routeFrom, resolve, reject, page) => {
        if (app.data.user !== null) {

            resolve(
                {
                    componentUrl: page,
                })

        } else {
            config.redirectLogin(app, routeTo.path);
        }
    }
};
