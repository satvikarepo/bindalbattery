$(document).ready(function () {
    var ReplaceMasterUrl = "http://satvikaitsolutions.com/bindalapi/api/Replace/GetAllReplace";
    var tableId = "#replacegrid";
    var formid = "#replacementMaster";
    var cols = [{
        'data': 'replaceId'
    }, {
        'data': 'batterType'
    }, {
        'data': 'okdSrNo'
    }, {
        'data': 'newSrNo'
        }, {
        'data': 'saleDate'
    }
    , {
            'data': 'replacementDate'
        }, {
            'data': 'partyName'
        }
    ];

    _loadData({ _apiUrl: ReplaceMasterUrl, tableId, colDef: cols });

    init();


    $.validator.addMethod("brandSelect", function( value, element ) {
        return this.optional( element ) || sessionStorage.getItem("brandName");
    }, "Please select a brand" );
    $.validator.addMethod("batteryTypeSelect", function( value, element ) {
        return this.optional( element ) || sessionStorage.getItem("productid");
    }, "Please select a battery type" );
    $.validator.addMethod("SrNoSelect", function( value, element ) {
        return this.optional( element ) || sessionStorage.getItem("saleid");
    }, "Please select a Sr. No." );
    $.validator.addMethod( "partySelect", function( value, element ) {
        return this.optional( element ) || sessionStorage.getItem("partyid");
    }, "Please select a party" );

    $(formid).validate({
        rules: {
            txtBrandName: {
                required: true,
                brandSelect: true
            },
            txtBatteryType: {
                required: true,
                batteryTypeSelect: true
            },
            txtOldBatterySerno: {
                required: true,
                SrNoSelect: true,
            },
            txtNewBatterySerno: {
                required: true,
                alphanumeric: true,
            },
            txtSaleDate: {
                required: true
            },
            txtReplacementDate: {
                required: true
            },
            txtPartyName: {
                required: true,
                partySelect: true,
            }
        },
        messages: {
            txtBrandName: {
                required: "Please select a brand",
                brandSelect: "Please select a brand"
            },
            txtBatteryType: {
                required: "Please select a battery type",
                batteryTypeSelect: "Please select a battery type"
            },
            txtOldBatterySerno: {
                required: "Please select a battery sr. no.",
                SrNoSelect: "Please select a battery sr. no.",
            },
            txtNewBatterySerno: {
                required: "Please enter battery sr. no.",
                alphanumeric: "Only Letters, numbers, and underscores allowed",
            },
            txtSaleDate: {
                required: "Please select date of sale"
            },
            txtReplacementDate: {
                required: "Please select date of replacement"
            },
            txtPartyName: {
                required:  "Please select a party name",
                partySelect: "Please select a party name",
            }
        },
        submitHandler: function (form) {
            //form.submit();
        }
    });


    $('#deleteReplace').click(function () {	
        const data = table.api().row('.selected').data();
        const dataToPost = JSON.stringify({replaceId:data.replaceId});
        $.ajax({
            type: "POST",
            url: "http://satvikaitsolutions.com/bindalapi/api/Sales/DeleteSale",
            contentType: "application/json; charset=utf-8",
            data: dataToPost,           
            success: function (response) {
              _loadData({ _apiUrl: ReplaceMasterUrl, tableId, colDef: cols });
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

		console.log('start');
        var productid = sessionStorage.getItem("productid")
		console.log(productid);
        var saleId = sessionStorage.getItem("saleid")
		console.log(saleId);
        var newSrNo = document.getElementById("txtNewBatterySerno").value;
		console.log(newSrNo);
		var replacementDate = document.getElementById("txtReplacementDate").value;
		console.log(replacementDate);
		var partyId = sessionStorage.getItem("partyid");
		console.log(partyId);

        var replace = {
            "productId": productid,
            "saleid": saleId,
            "newSrNo": newSrNo,
            "replacementDate": replacementDate,
			"partyId": partyId
        };
        var dataToPost = JSON.stringify(replace);

        $.ajax({
            type: "POST",
            url: "http://satvikaitsolutions.com/bindalapi/api/Replace/InsertReplaceItem",
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
			success: function(response){
                _loadData({ _apiUrl: ReplaceMasterUrl, tableId, colDef: cols });
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

    $('#replacegrid tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });
	
	$("#btnUpload").on('click',function(){
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
				error: function() {
					alert("Error occurs");
				}
			});
	});
}