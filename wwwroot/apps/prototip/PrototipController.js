angular.module('App').controller('prototipController', ['$rootScope', '$scope', function ($rootScope, $scope) {

    //#region Public Properties 
    $scope.Status = null;
    var _r = $rootScope;
    $scope.init = init;
    $scope.load = $scope.$on('$viewContentLoaded', load);
    //#endregion


    //#region Private Functions 
    function init() {
        _r.Log.debug('Prototip controller yuklendi..');
    }

    function load() {
        _r.Log.debug('Prototip View yuklendi..');
    }

    //#endregion

}]);



 