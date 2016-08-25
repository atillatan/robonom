//#region APPLICATION
var App = angular.module("App", ['ui.router', 'ui', 'ui.grid', 'ui.directives', 'ui.bootstrap', 'oc.lazyLoad', 'ngSanitize', 'toastr']);
//#endregion

//#region INTERCEPTER
App.factory('usisIntercepter', ['$log', 'toastr', usisIntercepterInstance]);

function usisIntercepterInstance($log, toastr) {
    var usisIntercepter = {
        request: function (config) {
            config.requestTimestamp = new Date().getTime();
            return config;
        },
        response: function (response) {
            response.config.responseTimestamp = new Date().getTime();
            var elapsedms = response.config.responseTimestamp - response.config.requestTimestamp
            $log.debug(response.config.url + ' --> ' + elapsedms + 'ms');

            if (response.status != 200) {
                toastr.error(response.statusText, 'Hata', { TimeOut: 10000 });
                $log.error(response.statusText);
            }
            if (response.data != null && response.data.IsSuccess != undefined && response.data.IsSuccess != true) {
                toastr.error(response.data.Message, 'Hata', { timeOut: 10000000, positionClass: "toast-top-full-width" });
                $log.error(response.data.Message);
            }
            return response;
        }
    };
    return usisIntercepter;
}

//#endregion 

//#region DIRECTIVES

//#region LOADING (used on page or content load)
App.directive('ngSpinnerBar', ['$rootScope',
    function ($rootScope) {
        return {
            link: function (scope, element, attrs) {
                // by defult hide the spinner bar
                element.addClass('hide'); // hide spinner bar by default

                // display the spinner bar whenever the route changes(the content part started loading)
                $rootScope.$on('$stateChangeStart', function () {
                    element.removeClass('hide'); // show spinner bar
                });

                // hide the spinner bar on rounte change success(after the content loaded)
                $rootScope.$on('$stateChangeSuccess', function () {
                    element.addClass('hide'); // hide spinner bar
                    $('body').removeClass('page-on-load'); // remove page loading indicator
                    var menu = $('.page-sidebar-menu');
                    if (menu.length!=0) {
                        Layout.setSidebarMenuActiveLink('match'); // activate selected link in the sidebar menu
                    }
                  

                    // auto scorll to page top
                    setTimeout(function () {
                        UsisUtils.scrollTop();
                    }, $rootScope.settings.layout.pageAutoScrollOnLoad);
                });

                // handle errors
                $rootScope.$on('$stateNotFound', function () {
                    element.addClass('hide'); // hide spinner bar
                });

                // handle errors
                $rootScope.$on('$stateChangeError', function () {
                    element.addClass('hide'); // hide spinner bar
                });
            }
        };
    }
])
//#endregion

// Handle global LINK click
App.directive('a', function () {
    return {
        restrict: 'E',
        link: function (scope, elem, attrs) {
            if (attrs.ngClick || attrs.href === '' || attrs.href === '#') {
                elem.on('click', function (e) {
                    e.preventDefault(); // prevent link click for above criteria
                });
            }
        }
    };
});

// Handle Dropdown Hover Plugin Integration
App.directive('dropdownMenuHover', function () {
    return {
        link: function (scope, elem) {
            elem.dropdownHover();
        }
    };
});
//#endregion

//#region SETTINGS
App.factory('settings', ['$rootScope', function ($rootScope) {

    $rootScope.settings = settings;

    return settings;
}]);
//#endregion

//#region CONFIG
App.config(['$httpProvider', '$ocLazyLoadProvider', '$httpProvider', 'toastrConfig', function ($httpProvider, $ocLazyLoadProvider, $httpProvider, toastrConfig) {

    console.log('App.config() calisti...');

    //INTERCEPTER
    $httpProvider.interceptors.push('usisIntercepter');

    //RequestHeader config
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';

    //LAZY_LOADER (refer: https://github.com/ocombe/ocLazyLoad) 
    $ocLazyLoadProvider.config({
        debug: true,
        event: true,
        modules: [
                {
                    name: 'angular-animate',
                    files: [
                        '/lib/angular-animate/angular-animate.min.js'
                    ]
                }
        ],
    });

    angular.extend(toastrConfig, {
        autoDismiss: false,
        containerId: 'toast-container',
        maxOpened: 0,
        newestOnTop: true,
        positionClass: 'toast-top-center',
        preventDuplicates: false,
        preventOpenDuplicates: false,
        allowHtml: true,
        closeButton: true,
        closeHtml: '<button>&times;</button>',
        extendedTimeOut: 1000,
        target: 'body'
    });

}]);
//#endregion

//#region RUN
App.run(["$rootScope", "$state", "settings", "toastr",  "$log", "$location",   function ($rootScope, $state, settings, toastr,  $log, $location) {
    console.log('App.run() calisti...');
    $rootScope.State = $state;
    $rootScope.Settings = settings;
    $rootScope.Message = toastr;
   // $rootScope.Modal = $uibModal;
    $rootScope.Log = $log;
    $rootScope.Utils = RoboUtils;
    $rootScope.Location = $location;
    $rootScope.go = function go(url) { $location.href(url) }; 
  

     
 
}]);

//#endregion 

