App.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    console.debug('App.config() calisti...');
    $urlRouterProvider.otherwise("");

    $stateProvider
 

  

    // Modules
    .state("modules", {
        url: "/modules",
        templateUrl: "/apps/modules/index.html",
        data: { pageTitle: 'Ornek Moduller' },
        controller: "moduleController",
        resolve: {
            deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                return $ocLazyLoad.load({
                    name: 'App',
                    insertBefore: '#ng_load_plugins_before',
                    files: [
                          '/apps/modules/moduleController.js',
                    ]
                });
            }]
        }
    })

     // Prototip
    .state("prototip", {
        url: "/prototip",
        templateUrl: "/prototip/index",
        data: { pageTitle: 'Prototip' },
        controller: "prototipController",
        resolve: {
            deps: ['$ocLazyLoad', function ($ocLazyLoad) {
                return $ocLazyLoad.load({
                    name: 'App',
                    insertBefore: '#ng_load_plugins_before',
                    files: [
                          '/apps/prototip/prototipController.js',
                    ]
                });
            }]
        }
    })

}]);