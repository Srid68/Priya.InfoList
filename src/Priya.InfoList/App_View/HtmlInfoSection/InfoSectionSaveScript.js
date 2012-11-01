window.requestId = 0;

function refreshInfoSectionForm(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, infoPageId) {

    //if (typeof (getInfoSectionSaveView) == 'function') {
    //    getInfoSectionSaveView(0, pageNo, itemsPerPage, dataIndex, templateSuffix, false, infoPageId);
    //}

    if (typeof (refreshInfoSectionList) == 'function') {
        refreshInfoSectionList(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, infoPageId);
    }

    $('#infoSectionSaveView' + infoPageId).hide('slow');
    $('#infoSectionListSection' + infoPageId).trigger('expand');
}

function getInfoSectionSaveView(infoSectionId, pageNo, itemsPerPage, dataIndex, templateSuffix, expand, infoPageId) {

    var paramValues = '{"infoSectionId" : ' + infoSectionId;
    paramValues += ', "pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + itemsPerPage; 
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += ', "infoPageId" : ' + infoPageId;
    paramValues += '}';

    var postMethod = "GetInfoSectionSaveView";
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
                    $('#infoSectionSaveView' + infoPageId).html(retData.html);
                    $('#infoSectionSaveView' + infoPageId).trigger('create');
                    if (expand == true) {
                        $('#infoSectionSaveView' + infoPageId).toggle('slow');
                        $('#infoSectionSaveSection' + infoPageId).trigger('expand');
                    }
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}

function saveInfoSection(infoSectionId, pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, infoPageId) {

    var valid = true;

    var infoSectionName = $('#infoSectionName').val();
    if (infoSectionName.length == 0) {
        showError("Please enter the Info Section Name");
        valid = false;
    }

    var infoSectionDescription = $('#infoSectionDescription').val();
    if (infoSectionDescription.length == 0) {
        showError("Please enter the Info Section Description");
        valid = false;
    }

    var infoSectionIsActive = false;
    if ($('#infoSectionIsActive').is(':checked')) infoSectionIsActive = true;

    var infoSectionIsDeleted = false;
    if ($('#infoSectionIsDeleted').is(':checked')) infoSectionIsDeleted = true;

    var infoSectionSequence = 0;
    if ($('#infoSectionSequence').length > 0) {
        infoSectionSequence = $('#infoSectionSequence').val();
    }

    if (valid == true) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"infoPageId" : ' + infoPageId;
        paramValues += ', "infoSectionName" : "' + encodeURIComponent(infoSectionName) + '"';
        paramValues += ', "infoSectionDescription" : "' + encodeURIComponent(infoSectionDescription) + '"';
        paramValues += ', "isActive" : ' + infoSectionIsActive;
        paramValues += ', "isDeleted" : ' + infoSectionIsDeleted;
        paramValues += ', "sequence" : ' + infoSectionSequence;
        paramValues += ', "infoSectionId" : ' + infoSectionId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "SaveInfoSection";
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
                        refreshInfoSectionForm(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, infoPageId);
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

function deleteInfoSection(infoSectionId, pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, infoPageId) {
    //Json Post to Json Handler
    if (infoSectionId > 0) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"infoSectionId" : ' + infoSectionId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "DeleteInfoSection";
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
                        refreshInfoSectionForm(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading, infoPageId);
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
