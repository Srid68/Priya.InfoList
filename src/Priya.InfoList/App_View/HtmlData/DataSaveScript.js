window.requestId = 0;

function refreshDataForm(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken) {

    if (typeof (getDataSaveView) == 'function') {
        getDataSaveView(0, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken, false);
    }

    if (typeof (refreshDataList) == 'function') {
        refreshDataList(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);
    }

    $('#dataListSection').trigger('expand');
}

function getDataSaveView(dataId, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken, expand) {

    var paramValues = '{"dataId" : ' + dataId;
    paramValues += ', "pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + itemsPerPage;
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += ', "configToken" : "' + configToken + '"';
    paramValues += '}';

    var postMethod = "GetDataSaveView";
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
                    $('#dataSaveView').html(retData.html);
                    if (typeof (bindAddressMap) == 'function') {
                        bindAddressMap();
                    }
                    $('#dataSaveView').trigger('create');
                    if (expand == true) {
                        $('#dataSaveView').show('slow');
                        $('#dataSaveSection').trigger('expand');
                    } else {
                        $('#dataSaveView').hide('slow');
                    }
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}

function getDataAppendView(dataId, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken, expand) {

    var paramValues = '{"dataId" : ' + dataId;
    paramValues += ', "pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + itemsPerPage;
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += ', "configToken" : "' + configToken + '"';
    paramValues += '}';

    var postMethod = "GetDataAppendView";
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
                    $('#dataSaveView').html(retData.html);
                    if (typeof (bindAddressMap) == 'function') {
                        bindAddressMap();
                    }
                    $('#dataSaveView').trigger('create');
                    if (expand == true) {
                        $('#dataSaveView').show('slow');
                        $('#dataSaveSection').trigger('expand');
                    }
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}

function saveData(dataId, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken) {

    var valid = true;

    var parentDataId = $('#parentDataId').val();
    if (parentDataId.length == 0) {
        parentDataId = 0;
    }

    var dataRefTypeId = $('#dataRefTypeSelect option:selected').val();
    var dataRefId = $('#dataRefId').val();
    if (dataRefId.length == 0) {
        dataRefId = 0;
    }
    var dataTypeId = $('#dataTypeSelect option:selected').val();

    var dataValue = $('#dataValue').val();
    if (dataValue.length == 0) {
        showError("Please enter the Data Value");
        valid = false;
    }

    var dataIsActive = false;
    if ($('#dataIsActive').is(':checked')) dataIsActive = true;

    if (valid == true) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"parentDataId" : ' + parentDataId;
        paramValues += ', "dataRefId" : ' + dataRefId;
        paramValues += ', "dataRefTypeId" : ' + dataRefTypeId;
        paramValues += ', "dataTypeId" : ' + dataTypeId;
        paramValues += ', "dataValue" : "' + encodeURIComponent(dataValue) + '"';
        paramValues += ', "dataIsActive" : ' + dataIsActive;
        paramValues += ', "configToken" : "' + configToken + '"';
        paramValues += ', "dataId" : ' + dataId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "SaveData";
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
                        refreshDataForm(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);
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

function deleteData(dataId, pageNo, itemsPerPage, dataIndex, templateSuffix, configToken) {
    //Json Post to Json Handler
    if (dataId > 0) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"dataId" : ' + dataId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "DeleteData";
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
                        refreshDataForm(pageNo, itemsPerPage, dataIndex, templateSuffix, configToken);
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
