window.requestId = 0;

function retrieveInfoSectionList(source, pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, infoPageId) {

    $(source).button('disable');
    return refreshInfoSectionList(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, infoPageId);
}

function refreshInfoSectionList(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, infoPageId) {

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
    paramValues += ', "infoPageId" : ' + infoPageId;
    paramValues += '}';

    var postMethod = "GetInfoSectionListView";
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
                    $('#infoSectionList' + infoPageId).html(retData.html);
                    $('#infoSectionList' + infoPageId).listview('refresh');
                    $('#infoSectionList' + infoPageId).trigger('create');
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}
