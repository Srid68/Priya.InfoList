window.requestId = 0;

function inputFilterInfoPageList(e, pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading) {
    var evt = e ? e : window.event;
    if (evt.keyCode === 13) {
        return filterInfoPageList(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading);
    }
    return true;
}

function filterInfoPageList(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading) {

    var selectedItemsPerPage = itemsPerPage;
    var itemsPerPageEleId = "itemsPerPage" + dataIndex;
    if ($('#' + itemsPerPageEleId).length > 0) {
        selectedItemsPerPage = $('#' + itemsPerPageEleId + ' option:selected').val();
    }

    var filterInfoCategoryId = $('#filterCategorySelect option:selected').val();
    var filterInfoPage = $('#infoPageFilter').val();
    var filterInfoPagePublic = false;
    if ($('#filterInfoPagePublic').is(':checked')) filterInfoPagePublic = true;

    var paramValues = '{"pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + selectedItemsPerPage;
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += ', "asyncLoading" : ' + asyncLoading;
    paramValues += ', "filterInfoCategoryId" : ' + filterInfoCategoryId;
    paramValues += ', "filterInfoPage" : "' + filterInfoPage + '"';
    paramValues += ', "filterInfoPagePublic" : ' + filterInfoPagePublic;
    paramValues += '}';

    var postMethod = "GetInfoPageFilterListView";
    var requestId = window.requestId++;
    var parameters = '{"jsonrpc": "2.0",\"method\":\"' + postMethod + '\",\"params\":' + paramValues + ',\"id\":' + requestId + '}';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/*!@ServiceUrl@*/',
        data: parameters,
        dataType: "json",
        success: function (returnObj) {
            var retData = returnObj;
            if (returnObj.hasOwnProperty('error') == false) {
                if (returnObj.hasOwnProperty('d')) {
                    retData = returnObj.d;
                }
                else if (returnObj.hasOwnProperty('result')) {
                    retData = returnObj.result;
                }
                if (retData.hasOwnProperty('html')) {
                    $('#infoPageList' + dataIndex).html(retData.html);
                    $('#infoPageList' + dataIndex).listview('refresh');
                    $('#infoPageList' + dataIndex).trigger('create');
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;

}

function refreshInfoPageList(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading) {

    var selectedItemsPerPage = itemsPerPage;
    var itemsPerPageEleId = "itemsPerPage" + dataIndex;
    if ($('#' + itemsPerPageEleId).length > 0) {
        selectedItemsPerPage = $('#' + itemsPerPageEleId + ' option:selected').val();
    }

    var paramValues = '{"pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + selectedItemsPerPage;
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += ', "asyncLoading" : ' + asyncLoading;
    paramValues += '}';

    var postMethod = "GetInfoPageListView";
    var requestId = window.requestId++;
    var parameters = '{"jsonrpc": "2.0",\"method\":\"' + postMethod + '\",\"params\":' + paramValues + ',\"id\":' + requestId + '}';

    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: '/*!@ServiceUrl@*/',
        data: parameters,
        dataType: "json",
        success: function (returnObj) {
            var retData = returnObj;
            if (returnObj.hasOwnProperty('error') == false) {
                if (returnObj.hasOwnProperty('d')) {
                    retData = returnObj.d;
                }
                else if (returnObj.hasOwnProperty('result')) {
                    retData = returnObj.result;
                }
                if (retData.hasOwnProperty('html')) {
                    $('#infoPageList' + dataIndex).html(retData.html);
                    $('#infoPageList' + dataIndex).listview('refresh');
                    $('#infoPageList' + dataIndex).trigger('create');
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}
