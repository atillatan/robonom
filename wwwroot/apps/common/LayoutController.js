angular.module('App').controller('LayoutController', ['$scope', '$rootScope', LayoutControllerInstance]);

function LayoutControllerInstance($scope, $rootScope, $location) {
    'use strict';
    //#region Public Properties   
      

 
    //standart   
    var _r = $rootScope;
    $scope.init = init;
    $scope.load = $scope.$on('$viewContentLoaded', load);
    //#endregion

    //#region Private Functions

    function init() {
        _r.Log.debug('LayoutController yuklendi..');
    }

    function load() {
        _r.Log.debug('LayoutController View yuklendi..');
    }

    function go(url) {
        _r.go(url);
    }

    //*********main container da post, put, delete, get methodlari hazir da olabilir. hersey otomatiklesebilir.

    //#endregion
}