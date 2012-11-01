window.requestId = 0;

function refreshDataTypeForm(pageNo, itemsPerPage, dataIndex, templateSuffix) {

    //if (typeof (getDataTypeSaveView) == 'function') {
    //    getDataTypeSaveView(0, pageNo, itemsPerPage, dataIndex, templateSuffix, false);
    //}

    if (typeof (refreshDataTypeList) == 'function') {
        refreshDataTypeList(pageNo, itemsPerPage, dataIndex, templateSuffix);
    }

    $('#dataTypeSaveView').html('');
    $('#dataTypeListSection').trigger('expand');
}

function getDataTypeSaveView(dataTypeId, pageNo, itemsPerPage, dataIndex, templateSuffix, expand) {

    var paramValues = '{"dataTypeId" : ' + dataTypeId;
    paramValues += ', "pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + itemsPerPage; 
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix":"' + templateSuffix + '"'
    paramValues += '}';

    var postMethod = "GetDataTypeSaveView";
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
                    $('#dataTypeSaveView').html(retData.html);
                    $('#dataTypeSaveView').trigger('create');
                    $('#dataTypeSaveView').show('slow');
                    if (expand == true) {
                        $('#dataTypeSaveSection').trigger('expand');
                    }
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}

function saveDataType(dataTypeId, pageNo, itemsPerPage, dataIndex, templateSuffix) {

    var valid = true;

    var dataTypeName = $('#dataTypeName').val();
    if (dataTypeName.length == 0) {
        showError("Please enter the DataType");
        valid = false;
    }

    var dataTypeIsDefault = false;
    if ($('#dataTypeIsDefault').is(':checked')) dataTypeIsDefault = true;

    var dataTypeIsActive = false;
    if ($('#dataTypeIsActive').is(':checked')) dataTypeIsActive = true;

    if (valid == true) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"dataTypeName" : "' + encodeURIComponent(dataTypeName) + '"';
        paramValues += ', "dataTypeIsDefault" : ' + dataTypeIsDefault;
        paramValues += ', "dataTypeIsActive" : ' + dataTypeIsActive;
        paramValues += ', "dataTypeId" : ' + dataTypeId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "SaveDataType";
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
                    if (retData.hasOwnProperty('message')) {
                        showSuccess(retData.message);
                        refreshDataTypeForm(pageNo, itemsPerPage, dataIndex, templateSuffix);
                    } else if (retData.hasOwnProperty('error')) {
                        showError(retData.error);
                    }
                } else {
                    showError(returnObj.error.name + ":" + returnObj.error.message);
                }
                if (typeof (hideProgress) == 'function') hideProgress();
            }
        });
    }

    return false;
}

function deleteDataType(dataTypeId, pageNo, itemsPerPage, dataIndex, templateSuffix) {
    //Json Post to Json Handler
    if (dataTypeId > 0) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"dataTypeId" : ' + dataTypeId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "DeleteDataType";
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
                    if (retData.hasOwnProperty('message')) {
                        showSuccess(retData.message);
                        refreshDataTypeForm(pageNo, itemsPerPage, dataIndex, templateSuffix);
                    } else if (retData.hasOwnProperty('error')) {
                        showError(retData.error);
                    }
                } else {
                    showError(returnObj.error.name + ":" + returnObj.error.message);
                }
                if (typeof (hideProgress) == 'function') hideProgress();
            }
        });
    }

    return false;
}
