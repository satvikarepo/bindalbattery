$(document).ready(function () {
    var warrantyMasterUrl = "http://satvikaitsolutions.com/bindalapi/api/warranty/GetAllWarranty";
    var tableId = "#example";
    var formid = "#warrantyMaster";
    var cols = [{
        'data': 'warrantyId'
    }, {
        'data': 'partyName'
    }, {
        'data': 'productType'
    }, {
        'data': 'gracePeriod'
    }
    ];
    _loadData({ _apiUrl: warrantyMasterUrl, tableId, colDef: cols });
    
    $.validator.addMethod("productSelect", function( value, element ) {
        return this.optional( element ) || sessionStorage.getItem("productid");
    }, "Please select a product type" );

    $.validator.addMethod( "partySelect", function( value, element ) {
        return this.optional( element ) || sessionStorage.getItem("partyid");
    }, "Please select a party" );
    
    $(formid).validate({
        rules: {
            Product: {
                required: true,
                productSelect: true
            },
            Party: {
                required: true,
                partySelect: true
            },
            gracePeriod: {
                required: true,
                number: true,
            }
        },
        messages: {
            Product: {
                required: "Please select a product",
                productSelect: "Please select a product."
            },
            Party: {
                required: "Please select a party",
                partySelect: "Please select a party"
            },
            gracePeriod: {
                required: "Please enter grace period",
                number: "Only numbers allowed",
            }
        },
        submitHandler: function (form) {
            //form.submit();
        }
    });
    
    init();

    $('#btnyes').click(function () {
       
        var data = table.api().row('.selected').data();
        var dataToPost = JSON.stringify({warrantyId:data.warrantyId});
        $.ajax({
            type: "POST",
            url: "http://satvikaitsolutions.com/bindalapi/api/warranty/DeleteWarranty",
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
            success: function (response) {
                console.log('deleted')
                _loadData({ _apiUrl: warrantyMasterUrl, tableId, colDef: cols });
                Alert.success("Record deleted");;
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
        var partyid = sessionStorage.getItem("partyid")
        var productid = sessionStorage.getItem("productid")
        var gracePeriod = document.getElementById("gracePeriod").value;

        var Contact = {
            "productId": productid,
            "partyMatserId": partyid,
            "gracePeriod": gracePeriod
        };
        var dataToPost = JSON.stringify(Contact);

        $.ajax({
            type: "POST",
            url: "http://satvikaitsolutions.com/bindalapi/api/warranty/InsertWarranty",
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
            success: function (response) {
                _loadData({ _apiUrl: warrantyMasterUrl, tableId, colDef: cols });
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
    $('#example tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
}