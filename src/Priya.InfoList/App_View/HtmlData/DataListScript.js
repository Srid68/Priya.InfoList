window.requestId = 0;

function filterDataList(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken) {

    var selectedItemsPerPage = itemsPerPage;
    var itemsPerPageEleId = "itemsPerPage" + dataIndex;
    if ($('#' + itemsPerPageEleId).length > 0) {
        selectedItemsPerPage = $('#' + itemsPerPageEleId + ' option:selected').val();
    }

    var dataRefTypeId = $('#filterDataRefTypeSelect option:selected').val();
    var dataTypeId = $('#filterDataTypeSelect option:selected').val();

    var paramValues = '{"pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + selectedItemsPerPage;
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += ', "configToken" : "' + configToken + '"';
    paramValues += ', "dataRefTypeId" : ' + dataRefTypeId;
    paramValues += ', "dataTypeId" : ' + dataTypeId;
    paramValues += '}';

    var postMethod = "GetDataListFilterView";
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
                    $('#dataList' + dataIndex).html(retData.html);
                    $('#dataList' + dataIndex).listview('refresh');
                    $('#dataList' + dataIndex).trigger('create');
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}

function refreshDataList(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken) {

    var selectedItemsPerPage = itemsPerPage;
    var itemsPerPageEleId = "itemsPerPage" + dataIndex;
    if ($('#' + itemsPerPageEleId).length > 0) {
        selectedItemsPerPage = $('#' + itemsPerPageEleId + ' option:selected').val();
    }

    var paramValues = '{"pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + selectedItemsPerPage;
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += ', "configToken" : "' + configToken + '"';
    paramValues += '}';

    var postMethod = "GetDataListView";
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
                    $('#dataList' + dataIndex).html(retData.html);
                    $('#dataList' + dataIndex).listview('refresh');
                    $('#dataList' + dataIndex).trigger('create');
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}
