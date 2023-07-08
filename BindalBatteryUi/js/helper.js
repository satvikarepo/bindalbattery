var apiBaseUrl = "http://satvikaitsolutions.com/bindalapi/api/";
// var apiBaseUrl="https://localhost:7122/api/";
var table;
var loaderId = "#cover-spin";
$(document).ready(function () {
    // loader start *********
    const loaderDiv = document.createElement('div');
    loaderDiv.id = loaderId.replace('#', '');
    loaderDiv.innerHTML = "<span class='spinner-txt'>Please wait...</span>"
    document.body.appendChild(loaderDiv);
    $(document).ajaxStart(function () {
        if (!document.activeElement.className.includes("ui-autocomplete-input")) {
            $(loaderId).show(0);
        }
    });
    $(document).ajaxComplete(function () {
        $(loaderId).hide(0);
    });

    $(document).ajaxError(function () {
        $(loaderId).hide(0);
    });

    // loader end *********
    const regex = /^[a-z\d\-_\s]+$/i;
    $.validator.addMethod("alphanumeric", function (value, element) {
        return this.optional(element) || regex.test(value) ///^\w+$/i.test( value );
    }, "Letters, numbers, and underscores only please.");
});


function registerCommonValidators() {
    $.validator.addMethod("mobile", function (value, element) {
        var pattern = /^\d{10}$/;
        return (pattern.test(value) && value.length === 10);
    }, "Invalid mobile number.");
}

function resetForm(formid) {
    $(':input', formid)
        .not(':button, :submit, :reset, :hidden')
        .val('')
        .prop('checked', false)
        .prop('selected', false);
}
function serilizeData(formid) {
    let _data = $(formid).serialize();
    _data = decodeURIComponent(_data.replace(/%2F/g, " "))
    let arr = _data.split("&");
    let ob = {};
    arr.map(x => {
        let vals = x.split('=');
        ob[vals[0]] = vals[1];
        return x;
    });
    return ob;
}

function showMsgErr(msg = "Somthing went wrong. Please try after sometime.", timeOut = 5000) {
    let msgBox = document.getElementById("errMsg");
    msgBox.innerHTML = msg;
    setTimeout(() => { msgBox.innerHTML = "" }, timeOut);
}

function _loadData({ _apiUrl, tableId, colDef = [] }) {

    //Col def example
    // colDef =[{
    //     'data': 'partyMasterId'
    // }, {
    //     'data': 'partyname'
    // }, {
    //     'data': 'mobile'
    // }, {
    //     'data': 'place'
    // }
    // ]
    let _url = _apiUrl.includes('http') ? _apiUrl : `${apiBaseUrl}${_apiUrl}`;
    setTimeout(() => {
        $.ajax({
            url: _url,
            method: 'Get',
            dataType: 'json',
            success: function (data) {
                console.log(data);
                if ($.fn.dataTable.isDataTable(tableId)) {
                    table = $(tableId).DataTable();
                    table.destroy();
                }
                table = $(tableId).dataTable({
                    data: data,
                    columns: colDef
                });
            }
        });
    }, 100);

}

function clearStorage(el) {
    sessionStorage.removeItem(el);
}

var Alert = {
    error: function (msg = 'Something went wrong. Please try again.', title = "Error") {
        $.alert({ title: title, content: msg });
    },
    success: function (msg = 'Operation completed', title = "Success") {
        $.alert({ title: title, content: msg });
    },
    warning: function (msg = 'This is warning.', title = "Warning") {
        $.alert({ title: title, content: msg });
    },
    info: function (msg = 'This is info.', title = "Info") {
        $.alert({ title: title, content: msg });
    }
}


function ExportJsonToCsv(JSONData, fileName = "", isShowHeader = true) {
    var arrJsonData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
    var CSV = '';
    // CSV += ReportName ? ReportName + '\r\n\n' :'';
    if (isShowHeader) {
        var row = "";
        for (var index in arrJsonData[0]) {
            row += index + ',';
        }
        row = row.slice(0, -1);
        CSV += row + '\r\n';
    }
    for (var i = 0; i < arrJsonData.length; i++) {
        var row = "";
        for (var index in arrJsonData[i]) {
            row += '"' + arrJsonData[i][index] + '",';
        }
        row.slice(0, row.length - 1);
        CSV += row + '\r\n';
    }
    if (CSV == '') {
        alert("Invalid JsonData");
        return;
    }
    let _fileName = fileName ? `${fileName.replace(/ /g, "_")}_${formatDate().res}` : "";
    let uri = 'Data:text/csv;charset=utf-8,' + escape(CSV);
    let link = document.createElement("a");
    link.href = uri;
    link.style = "visibility:hidden";
    link.download = _fileName + ".csv";
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function formatDate(format = "dd-mm-yyyy-hh-min-ss", date = new Date()) {
    const mm = date.getMonth() + 1;
    const dd = date.getDate();
    const yy = date.getFullYear();
    const hh = date.getHours();
    const min = date.getMinutes();
    const ss = date.getSeconds();
    const ms = date.getMilliseconds();
    let res = format.toLowerCase();
    res = res.replace("dd", dd);
    res = res.replace("mm", mm);
    res = res.replace("yyyy", yy);
    res = res.replace("hh", hh);
    res = res.replace("min", min);
    res = res.replace("ss", ss);
    res = res.replace("ms", ms);
    return { res, mm, dd, yy, hh, min, ss, ms }
}

function postData(path, dataToPost, options) {
    $.ajax({
        type: "POST",
        url: `${apiBaseUrl}${path}`,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataToPost),
        success: function (response) {
            if (options) {
                resetForm(options.formid);
                Alert.success(options['200']);
            } else {
                Alert.success();
            }
        },
        error: function (err) {
            console.log(err);
            if (options) {
                Alert.error(options[err.status] || "Something went wrong. Please try again.");
            } else {
                Alert.error("Something went wrong. Please try again.");
            }
        }
    });

}


function checkRole(roles = ['Dealer', 'Distributor']) {
    const userRole = sessionStorage.getItem('role');
    if(userRole.toLocaleLowerCase() !=='admin'){
        const filtered = roles.filter(x => x.toLocaleLowerCase() === userRole.toLocaleLowerCase());
        if (filtered.length === 0) {
            sessionStorage.clear();
            location.replace("login.html")
        }
    }
}