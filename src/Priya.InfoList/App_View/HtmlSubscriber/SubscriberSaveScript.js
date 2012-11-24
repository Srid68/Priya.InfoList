window.requestId = 0;

function refreshSubscriberForm(pageNo, itemsPerPage, dataIndex, templateSuffix) {

    //if (typeof (getSubscriberSaveView) == 'function') {
    //    getSubscriberSaveView(0, pageNo, itemsPerPage, dataIndex, templateSuffix, false);
    //}

    if (typeof (refreshSubscriberList) == 'function') {
        refreshSubscriberList(pageNo, itemsPerPage, dataIndex, templateSuffix);
    }

    $('#subscriberDetails').hide('slow');
    $('#subscriberListSection').trigger('expand');
}

function getSubscriberSaveView(subscriberId, pageNo, itemsPerPage, dataIndex, templateSuffix, expand) {

    var paramValues = '{"subscriberId" : ' + subscriberId;
    paramValues += ', "pageNo" : ' + pageNo;
    paramValues += ', "itemsPerPage" : ' + itemsPerPage; 
    paramValues += ', "dataIndex" : ' + dataIndex;
    paramValues += ', "templateSuffix" : "' + templateSuffix + '"';
    paramValues += '}';

    var postMethod = "GetSubscriberSaveView";
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
                    $('#subscriberSaveView').html(retData.html);
                    $('#subscriberSaveView').trigger('create');
                    if (expand == true) {
                        $('#subscriberSaveSection').trigger('expand');
                    }
                }
            } else {
                showError(returnObj.error.name + ":" + returnObj.error.message);
            }
        }
    });

    return false;
}

function saveSubscriber(subscriberId, pageNo, itemsPerPage, dataIndex, templateSuffix) {

    var valid = true;

    var subscriberEmail = $('#subscriberEmail').val();
    if (subscriberEmail.length == 0) {
        showError("Please enter the Subscriber Email");
        valid = false;
    }

    var subscriberMessage = $('#subscriberMessage').val();
    if (subscriberMessage.length == 0) {
        showError("Please enter the Subscriber Message");
        valid = false;
    }

    var subscriberIsDeleted = false;
    if ($('#subscriberIsDeleted').is(':checked')) subscriberIsDeleted = true;

    if (valid == true) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"subscriberEmail" : "' + encodeURIComponent(subscriberEmail) + '"';
        paramValues += ', "subscriberMessage" : "' + encodeURIComponent(subscriberMessage) + '"';
        paramValues += ', "subscriberIsDeleted" : ' + subscriberIsDeleted;
        paramValues += ', "subscriberId" : ' + subscriberId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "SaveSubscriber";
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
                        refreshSubscriberForm(pageNo, itemsPerPage, dataIndex, templateSuffix);
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

function deleteSubscriber(subscriberId, pageNo, itemsPerPage, dataIndex, templateSuffix) {
    //Json Post to Json Handler
    if (subscriberId > 0) {

        if (typeof (showProgress) == 'function') showProgress();

        var paramValues = '{"subscriberId" : ' + subscriberId;
        paramValues += '}';

        var requestId = window.requestId++;
        var postMethod = "DeleteSubscriber";
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
                        refreshSubscriberForm(pageNo, itemsPerPage, dataIndex, templateSuffix);
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
