angular.module('App').controller('moduleController', ['$rootScope', '$scope',  moduleControllerInstance]);

function moduleControllerInstance($rootScope, $scope) {
    'use strict';

    //#region Public Properties  

 
    var _r = $rootScope;
    $scope.init = init;
    $scope.load = $scope.$on('$viewContentLoaded', load);
    //#endregion

    //#region Private Functions 
    function init() {        
        _r.Log.debug('Module controller yuklendi..');        
    }

    function load() {
        _r.Log.debug('Module View yuklendi..');
    }

    //#endregion
}














