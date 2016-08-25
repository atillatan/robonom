'use strict';

//#region Helpers

var RoboUtils = RoboUtils || {};

RoboUtils = {
    isNumeric: isNumeric,
    ShowMessage2: ShowMessage2,
    isNotString: isNotString,
    isString: isString,
    isFunction: isFunction,
    _errorMethod: _errorMethod,
    scrollTop: scrollTop
};


function isNumeric(obj) {
    return !jQuery.isArray(obj) && (obj - parseFloat(obj) + 1) >= 0;
}

function ShowMessage2($scope, panelStyle, message) {
    $scope.Message = message;
    $("#messagePanel").attr('class', 'panel panel-' + panelStyle);
    $("#messagePanel").fadeTo(2000, 500).slideUp(500, function () {
        $("#messagePanel").alert('close');
    });
}

function isNotString(str) {
    return (typeof str !== "string");
}

function isString(str) {
    return !isNotString(str);
}

function isDate(str) {
    return str.math('/^(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2}):(\d{2})$/');
}

function isFunction(obj) {
    return jQuery.type(obj) === "function";
}

function _errorMethod(error) {
    $log.error(error);
}

function scrollTop(el, offeset) {
    var pos = (el && el.size() > 0) ? el.offset().top : 0;

    if (el) {
     
        pos = pos + (offeset ? offeset : -1 * el.height());
    }

    $('html,body').animate({
        scrollTop: pos
    }, 'slow');
}

//#endregion