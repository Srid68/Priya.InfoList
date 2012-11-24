window.requestId = 0;

function refreshInfoPageCategoryOptionList(infoPageId, templateSuffix) {

    var paramValues = '{"infoPageId" : ' + infoPageId ;
    paramValues += ', "templateSuffix":"' + templateSuffix + '"';
    paramValues += '}';

    var postMethod = "GetInfoPageCategoryOptionList";
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
                    $('#infoPageCategorySelect')[0].options.length = 0;
                    $('#infoPageCategorySelect').html(retData.html);
                    $('#infoPageCategorySelect').selectmenu('refresh');
                    $('#infoPageCategorySelect').trigger('create');
                }
                if (retData.hasOwnProperty('warn')) {
                    showWarning(retData.warn);
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });
}

function refreshInfoPageForm(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading) {

    //if (typeof (getInfoPageSaveView) == 'function') {
    //    getInfoPageSaveView(0, pageNo, itemsPerPage, dataIndex, templateSuffix, false);
    //}

    if (typeof (refreshInfoPageList) == 'function') {
        refreshInfoPageList(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading);
    }

    $('#infoPageSaveView' + dataIndex).hide('slow');
    $('#infoPageListSection' + dataIndex).trigger('expand');
}

function getInfoPageSaveView(infoPageId, pageNo, itemsPerPage, dataIndex, templateSuffix, expand) {

    var paramValues = '{"infoPageId" : ' + infoPageId;
    paramValues += ', "pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + itemsPerPage;
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += '}';

    var postMethod = "GetInfoPageSaveView";
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
                    $('#infoPageSaveView' + dataIndex).html(retData.html);
                    $('#infoPageSaveView' + dataIndex).trigger('create');
                    if (expand == true) {
                        $('#infoPageSaveView' + dataIndex).toggle('slow');
                        $('#infoPageSaveSection' + dataIndex).trigger('expand');
                    }
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });


    return false;
}

function saveInfoPage(infoPageId, pageNo, itemsPerPage, dataIndex, templateSuffix) {

    var valid = true;

    var infoPageName = $('#infoPageName').val();
    if (infoPageName.length == 0) {
        showError("Please enter the Info Page Name");
        valid = false;
    }

    var infoPageDescription = $('#infoPageDescription').val();
    if (infoPageDescription.length == 0) {
        showError("Please enter the Info Page Description");
        valid = false;
    }

    var infoPageCategoryId = $('#infoPageCategorySelect option:selected').val();
    if (infoPageCategoryId == 0) {
        showError("Please Select a Info Category");
        valid = false;
    }

    var accessGroupId = 0;
    if ($('#accessGroupSelect').length > 0) {
        accessGroupId = $('#accessGroupSelect option:selected').val();
    }

    var infoPageAsyncLoading = false;
    if ($('#infoPageAsyncLoading').is(':checked')) infoPageAsyncLoading = true;

    var infoPageIsActive = false;
    if ($('#infoPageIsActive').is(':checked')) infoPageIsActive = true;

    var infoPageExpiryDate = "";
    if ($('#infoPageExpiryDate').length > 0) {
        infoPageExpiryDate = $('#infoPageExpiryDate').val();
        if (valid == true) {
            if (infoPageExpiryDate.length == 0) {
                showError("Please enter the Page Expiry Date");
                valid = false;
            }
        }
    }

    var infoPageCommentable = false;
    if ($('#infoPageCommentable').is(':checked')) infoPageCommentable = true;

    var commentorRoleList = "";
    if ($('#commentorRoleSelect').length > 0) {
        var commentorRoleArray = $('#commentorRoleSelect').val();
        if (Object.prototype.toString.call(commentorRoleArray) === '[object Array]') {
            commentorRoleList = commentorRoleArray.join();
        }
        else {
            commentorRoleList = commentorRoleArray;
        }
    }

    var infoPageIsPublic = false;
    if ($('#infoPageIsPublic').is(':checked')) infoPageIsPublic = true;

    var infoPageIsCommon = false;
    if ($('#infoPageIsCommon').is(':checked')) infoPageIsCommon = true;

    var infoPageIsDeleted = false;
    if ($('#infoPageIsDeleted').is(':checked')) infoPageIsDeleted = true;

    var infoPageSequence = 0;
    if ($('#infoPageSequence').length > 0) {
        infoPageSequence = $('#infoPageSequence').val();
    }

    if (valid == true) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"infoPageName" : "' + encodeURIComponent(infoPageName) + '"';
        paramValues += ', "infoPageDescription" : "' + encodeURIComponent(infoPageDescription) + '"';
        paramValues += ', "infoPageCategoryId" : ' + infoPageCategoryId;
        paramValues += ', "accessGroupId" : ' + accessGroupId;
        paramValues += ', "asyncLoading" : ' + infoPageAsyncLoading;
        paramValues += ', "isActive" : ' + infoPageIsActive;
        paramValues += ', "expiryDate" : "' + infoPageExpiryDate + '"';
        paramValues += ', "commentable" : ' + infoPageCommentable;
        paramValues += ', "commentorRoleList" : "' + encodeURIComponent(commentorRoleList) + '"';
        paramValues += ', "isPublic" : ' + infoPageIsPublic;
        paramValues += ', "isCommon" : ' + infoPageIsCommon;
        paramValues += ', "isDeleted" : ' + infoPageIsDeleted;
        paramValues += ', "sequence" : ' + infoPageSequence;
        paramValues += ', "infoPageId" : ' + infoPageId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "SaveInfoPage";
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
                        refreshInfoPageForm(pageNo, itemsPerPage, dataIndex, templateSuffix, infoPageAsyncLoading);
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

function deleteInfoPage(infoPageId, pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading) {
    //Json Post to Json Handler
    if (infoPageId > 0) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"infoPageId" : ' + infoPageId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "DeleteInfoPage";
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
                        refreshInfoPageForm(pageNo, itemsPerPage, dataIndex, templateSuffix, asyncLoading);
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
