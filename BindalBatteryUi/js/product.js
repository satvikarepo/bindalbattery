$(document).ready(function () {
    var selected = [];
    var productMasterUrl = "http://satvikaitsolutions.com/bindalapi/api/Product/GetAllProduct";
    var tableId = "#example";
    var formid="#productMaster";
    var cols = [{
        'data': 'productId'
    }, {
        'data': 'brandName'
    }, {
        'data': 'batteryType'
    }, {
        'data': 'gauranteePeriod'
    }
    ];

    _loadData({ _apiUrl: productMasterUrl, tableId, colDef: cols });

    $(formid).validate({
        rules: {
            brandName: {
                required: true,
                alphanumeric: true
            },
            batteryType: {
                required: true,
                alphanumeric: true
            },
            gauranteePeriod: {
                required: true,
                number: true
            }
        },
        messages: {
            brandName: {
                required: "Enter brand name",
                alphanumeric: "Only letters, numbers, and underscores allowed"
            },
            batteryType: {
                required: "Enter brand name",
                alphanumeric: "Only letters, numbers, and underscores allowed"
            },
            gauranteePeriod: {
                required: "Enter place",
                number: "Only numbers allowed"
            }
        },
        submitHandler: function (form) {
            //form.submit();
        }
    });



    $('#example tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    $('#deleterecord').click(function () {
        const data = table.api().row('.selected').data();
        const productid=data.productId;
        const dataToPost = JSON.stringify({productid});
        $.ajax({
            type: "POST",
            url: "http://satvikaitsolutions.com/bindalapi/api/product/DeleteProduct",
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
            success: function (resonse) {
                _loadData({ _apiUrl: productMasterUrl, tableId, colDef: cols });
                console.log(resonse);
                Alert.success("Record deleted");
            },
            error: function (err) {
                console.log(err);
                Alert.error();
            }
        });
    });



    $('#addrecord').click(function () {
        if (!$(formid).valid()) {
            return false;
        }
        const _data = serilizeData(formid);
        console.log('start add');

        var dataToPost = JSON.stringify(_data);

        $.ajax({
            type: "POST",
            url: "http://satvikaitsolutions.com/bindalapi/api/product/InsertProduct",
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
            success: function () {
                _loadData({ _apiUrl: productMasterUrl, tableId, colDef: cols });
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