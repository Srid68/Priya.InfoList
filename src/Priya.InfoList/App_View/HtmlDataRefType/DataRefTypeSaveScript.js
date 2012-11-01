window.requestId = 0;

function refreshDataRefTypeForm(pageNo, itemsPerPage, dataIndex, templateSuffix) {

    //if (typeof (getDataRefTypeSaveView) == 'function') {
    //    getDataRefTypeSaveView(0, pageNo, itemsPerPage, dataIndex, templateSuffix, false);
    //}

    if (typeof (refreshDataRefTypeList) == 'function') {
        refreshDataRefTypeList(pageNo, itemsPerPage, dataIndex, templateSuffix);
    }

    $('#dataRefTypeSaveView').html('');
    $('#dataRefTypeListSection').trigger('expand');
}

function getDataRefTypeSaveView(dataRefTypeId, pageNo, itemsPerPage, dataIndex, templateSuffix, expand) {

    var paramValues = '{"dataRefTypeId" : ' + dataRefTypeId;
    paramValues += ', "pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + itemsPerPage; 
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix":"' + templateSuffix + '"'
    paramValues += '}';

    var postMethod = "GetDataRefTypeSaveView";
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
                    $('#dataRefTypeSaveView').html(retData.html);
                    $('#dataRefTypeSaveView').trigger('create');
                    $('#dataRefTypeSaveView').show('slow');
                    if (expand == true) {
                        $('#dataRefTypeSaveSection').trigger('expand');
                    }
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}

function saveDataRefType(dataRefTypeId, pageNo, itemsPerPage, dataIndex, templateSuffix) {

    var valid = true;

    var dataRefType = $('#dataRefType').val();
    if (dataRefType.length == 0) {
        showError("Please enter the DataRefType");
        valid = false;
    }

    var dataRefTypeIsDefault = false;
    if ($('#dataRefTypeIsDefault').is(':checked')) dataRefTypeIsDefault = true;

    var dataRefTypeIsActive = false;
    if ($('#dataRefTypeIsActive').is(':checked')) dataRefTypeIsActive = true;

    if (valid == true) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"dataRefType" : "' + encodeURIComponent(dataRefType) + '"';
        paramValues += ', "dataRefTypeIsDefault" : ' + dataRefTypeIsDefault;
        paramValues += ', "dataRefTypeIsActive" : ' + dataRefTypeIsActive;
        paramValues += ', "dataRefTypeId" : ' + dataRefTypeId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "SaveDataRefType";
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
                        refreshDataRefTypeForm(pageNo, itemsPerPage, dataIndex, templateSuffix);
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

function deleteDataRefType(dataRefTypeId, pageNo, itemsPerPage, dataIndex, templateSuffix) {
    //Json Post to Json Handler
    if (dataRefTypeId > 0) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"dataRefTypeId" : ' + dataRefTypeId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "DeleteDataRefType";
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
                        refreshDataRefTypeForm(pageNo, itemsPerPage, dataIndex, templateSuffix);
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
