window.requestId = 0;

function refreshInfoCategoryForm(pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay) {

    //if (typeof (getInfoCategorySaveView) == 'function') {
    //    getInfoCategorySaveView(0, pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay, false);
    //}

    if (typeof (refreshInfoCategoryList) == 'function') {
        refreshInfoCategoryList(pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay);
    }

    /*!@RefreshCallback@*/

    $('#infoCategorySaveView' + dataIndex).html('');
    $('#infoCategoryListSection' + dataIndex).trigger('expand');
}

function getInfoCategorySaveView(infoCategoryId, pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay, expand) {

    var paramValues = '{"infoCategoryId" : ' + infoCategoryId;
    paramValues += ', "pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage":' + itemsPerPage;
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix":"' + templateSuffix + '"'
    paramValues += ', "hideDisplay":"' + hideDisplay + '"';
    paramValues += '}';

    var postMethod = "GetInfoCategorySaveView";
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
                    $('#infoCategorySaveView' + dataIndex).html(retData.html);
                    $('#infoCategorySaveView' + dataIndex).trigger('create');
                    $('#infoCategorySaveView' + dataIndex).show('slow');
                    if (expand == true) {
                        $('#infoCategorySaveSection' + dataIndex).trigger('expand');
                    }
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}

function saveInfoCategory(infoCategoryId, pageNo, itemsPerPage, dataIndex, templateSuffix, hideDiplay) {

    var valid = true;

    var infoCategoryName = $('#infoCategoryName' + dataIndex).val();
    if (infoCategoryName.length == 0) {
        showError("Please enter the Category");
        valid = false;
    }

    var infoCategoryIsDefault = false;
    if ($('#infoCategoryIsDefault' + dataIndex).is(':checked')) infoCategoryIsDefault = true;

    var infoCategoryIsActive = false;
    if ($('#infoCategoryIsActive' + dataIndex).is(':checked')) infoCategoryIsActive = true;

    if (valid == true) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"infoCategoryName" : "' + infoCategoryName + '"';
        paramValues += ', "isDefault" : ' + infoCategoryIsDefault;
        paramValues += ', "isActive" : ' + infoCategoryIsActive;
        paramValues += ', "infoCategoryId" : ' + infoCategoryId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "SaveInfoCategory";
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
                    if (retData.hasOwnProperty('message')) {
                        showSuccess(retData.message);
                        refreshInfoCategoryForm(pageNo, itemsPerPage, dataIndex, templateSuffix, hideDiplay);
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

function deleteInfoCategory(infoCategoryId, pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay) {
    //Json Post to Json Handler
    if (infoCategoryId > 0) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"infoCategoryId":' + infoCategoryId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "DeleteInfoCategory";
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
                    if (retData.hasOwnProperty('message')) {
                        showSuccess(retData.message);
                        refreshInfoCategoryForm(pageNo, itemsPerPage, dataIndex, templateSuffix, hideDisplay);
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
