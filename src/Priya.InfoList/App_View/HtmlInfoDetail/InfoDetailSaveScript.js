window.requestId = 0;

function refreshInfoDetailForm(pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId) {

    //if (typeof (getInfoDetailSaveView) == 'function') {
    //    getInfoDetailSaveView(0, pageNo, itemsPerPage, dataIndex, templateSuffix, false, infoSectionId);
    //}

    if (typeof (refreshInfoDetailList) == 'function') {
        refreshInfoDetailList(pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId);
    }

    $('#infoDetailSaveView' + infoSectionId).hide('slow');
    $('#infoDetailListSection' + infoSectionId).trigger('expand');
}

function getInfoDetailSaveView(infoDetailId, pageNo, itemsPerPage, dataIndex, templateSuffix, expand, infoSectionId) {

    var paramValues = '{"infoDetailId" : ' + infoDetailId;
    paramValues += ', "pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + itemsPerPage; 
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += ', "infoSectionId" : ' + infoSectionId;
    paramValues += '}';

    var postMethod = "GetInfoDetailSaveView";
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
                    $('#infoDetailSaveView' + infoSectionId).html(retData.html);
                    $('#infoDetailSaveView' + infoSectionId).trigger('create');
                    if (expand == true) {
                        $('#infoDetailSaveView' + infoSectionId).toggle('slow');
                        $('#infoDetailSaveSection' + infoSectionId).trigger('expand');
                    }
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}

function saveInfoDetail(infoDetailId, pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId) {

    var valid = true;

    var infoDetailName = $('#infoDetailName').val();
    if (infoDetailName.length == 0) {
        showError("Please enter the Info Detail Name");
        valid = false;
    }

    var infoDetailDescription = $('#infoDetailDescription').val();
    if (infoDetailDescription.length == 0) {
        showError("Please enter the Info Detail Description");
        valid = false;
    }

    var infoDetailIsActive = false;
    if ($('#infoDetailIsActive').is(':checked')) infoDetailIsActive = true;

    var infoDetailIsDeleted = false;
    if ($('#infoDetailIsDeleted').is(':checked')) infoDetailIsDeleted = true;

    var infoDetailSequence = 0;
    if ($('#infoDetailSequence').length > 0) {
        infoDetailSequence = $('#infoDetailSequence').val();
    }
    if (valid == true) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"infoSectionId" : ' + infoSectionId;
        paramValues += ', "infoDetailName" : "' + encodeURIComponent(infoDetailName) + '"';
        paramValues += ', "infoDetailDescription" : "' + encodeURIComponent(infoDetailDescription) + '"';
        paramValues += ', "isActive" : ' + infoDetailIsActive;
        paramValues += ', "isDeleted" : ' + infoDetailIsDeleted;
        paramValues += ', "sequence" : ' + infoDetailSequence;
        paramValues += ', "infoDetailId" : ' + infoDetailId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "SaveInfoDetail";
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
                        refreshInfoDetailForm(pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId);
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

function deleteInfoDetail(infoDetailId, pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId) {
    //Json Post to Json Handler
    if (infoDetailId > 0) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"infoDetailId" : ' + infoDetailId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "DeleteInfoDetail";
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
                        refreshInfoDetailForm(pageNo, itemsPerPage, dataIndex, templateSuffix, infoSectionId);
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
