window.requestId = 0;

function refreshInfoCategoryList(pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay) {

    var selectedItemsPerPage = 0;
    var itemsPerPageEleId = "itemsPerPage" + dataIndex;
    if ($('#' + itemsPerPageEleId).length > 0) {
        selectedItemsPerPage = $('#' + itemsPerPageEleId + ' option:selected').val();
    }

    var paramValues = '{"pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + selectedItemsPerPage;
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix":"' + templateSuffix + '"'
    paramValues += ', "hideDisplay":' + hideDisplay;
    paramValues += '}';

    var postMethod = "GetInfoCategoryListView";
    var requestId = window.requestId++;
    var parameters = '{\"id\":' + requestId + ',\"method\":\"' + postMethod + '\",\"params\":' + paramValues + '}';

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
                    $('#infoCategoryList' + dataIndex).html(retData.html);
                    $('#infoCategoryList' + dataIndex).listview('refresh');
                    $('#infoCategoryList' + dataIndex).trigger('create');
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}
