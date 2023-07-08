$(document).ready(function () {
    var selected = [];
    var salesMasterUrl = "http://satvikaitsolutions.com/bindalapi/api/Sales/GetAllSales";
    var tableId = "#salegrid";
    var formid = "#salesMaster";
    var cols = [{
        'data': 'id'
    }, {
        'data': 'brandName'
    }, {
        'data': 'batteryType'
    }, {
        'data': 'batterySrNo'
    }, {
        'data': 'partyName'
    }
        , {
        'data': 'saleDate'
    }
    ];

    $.validator.addMethod("brandSelect", function (value, element) {
        return this.optional(element) || sessionStorage.getItem("brandName");
    }, "Please select a brand");

    $.validator.addMethod("batteryTypeSelect", function (value, element) {
        return this.optional(element) || sessionStorage.getItem("productid");
    }, "Please select a battery type");

    $.validator.addMethod("partyNameSelect", function (value, element) {
        return this.optional(element) || sessionStorage.getItem("partyid");
    }, "Please select a party name");

    _loadData({ _apiUrl: salesMasterUrl, tableId, colDef: cols });

    $(formid).validate({
        rules: {
            brandname: {
                required: true,
                brandSelect: true
            },
            batterytype: {
                required: true,
                batteryTypeSelect: true
            },
            serno: {
                required: true,
                alphanumeric: true
            },
            partyname: {
                required: true,
                partyNameSelect: true
            },
            saledate: {
                required: true
            }
        },
        messages: {
            brandname: {
                required: "Enter brand name",
                brandSelect: "Please select a brand"
            },
            batterytype: {
                required: "Enter battery type",
                batteryTypeSelect: "Please select a battery type"
            },
            serno: {
                required: "Enter battery serial no.",
                alphanumeric: "Only letters, numbers, and underscores allowed"
            },
            partyname: {
                required: "Enter party name",
                partyNameSelect: "Please select a party name"
            },
            saledate: {
                required: "Select date"
            }
        },
        submitHandler: function (form) {
            //form.submit();
        }
    });

    init();


    $('#btnyessale').click(function () {
        const data = table.api().row('.selected').data();
        const dataToPost = JSON.stringify({ id: data.id });
        $.ajax({
            type: "POST",
            url: "http://satvikaitsolutions.com/bindalapi/api/Sales/DeleteSale",
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
            success: function (response) {
                _loadData({ _apiUrl: salesMasterUrl, tableId, colDef: cols });
                Alert.success("Record deleted");
            },
            error: function (err) {
                console.log(err)
                Alert.error();
            }
        });

    });

    $('#addrecord').click(function () {
        if (!$(formid).valid()) {
            return false;
        }

        const dataToPost = JSON.stringify({
            "productId": sessionStorage.getItem("productid"),
            "partyId": sessionStorage.getItem("partyid"),
            "batterySrNo": document.getElementById("serno").value,
            "saleDate": document.getElementById("saledate").value
        });

        $.ajax({
            type: "POST",
            url: "http://satvikaitsolutions.com/bindalapi/api/Sales/InsertSales",
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
            success: function (response) {
                _loadData({ _apiUrl: salesMasterUrl, tableId, colDef: cols });

                resetForm(formid);
                $('#exampleModal').modal('hide');
            },
            error: function (err) {
                console.log(err);
                showMsgErr();
            }
        });
    });

});


function init() {
    $('#salegrid tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    $("#btnUpload").on('click', function () {
        var files = $('#fileUpload').prop("files");
        var url = "/Index?handler=MyUploader";
        formData = new FormData();
        formData.append("MyUploader", files[0]);

        jQuery.ajax({
            type: 'POST',
            url: url,
            data: formData,
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            success: function (repo) {
                if (repo.status == "success") {
                    alert("File : " + repo.filename + " is uploaded successfully");
                }
            },
            error: function () {
                alert("Error occurs");
            }
        });
    });
}

function exportSalesData() {
    var _data = table?.api().data().toArray();
    if (_data && _data.length) {
        const formatedData = [];
        _data.map(x => {
            formatedData.push({
                    ProductId: x.productId, PartyId: x.partyId, Brand: x.brandName,
                    "Battery Type": x.batteryType, "Battery Sr. No.": x.batterySrNo,
                    "Party Name": x.partyName, "Sale Date": x.saleDate
                })
        })
        ExportJsonToCsv(formatedData, "SalesMaster")
    } else {
        Alert.info("There is no data to export.")
    }

}

