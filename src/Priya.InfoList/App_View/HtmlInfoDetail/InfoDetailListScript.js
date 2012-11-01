window.requestId = 0;

function retrieveInfoDetailList(source, pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId) {

    $(source).button('disable');
    return refreshInfoDetailList(pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId);
}

function refreshInfoDetailList(pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId) {

    var selectedItemsPerPage = itemsPerPage;
    var itemsPerPageEleId = "itemsPerPage" + dataIndex;
    if ($('#' + itemsPerPageEleId).length > 0) {
        selectedItemsPerPage = $('#' + itemsPerPageEleId + ' option:selected').val();
    }

    var paramValues = '{"pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + selectedItemsPerPage;
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += ', "infoSectionId" : ' + infoSectionId;
    paramValues += '}';

    var postMethod = "GetInfoDetailListView";
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
                    $('#infoDetailList' + infoSectionId).html(retData.html);
                    $('#infoDetailList' + infoSectionId).listview('refresh');
                    $('#infoDetailList' + infoSectionId).trigger('create');
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}
