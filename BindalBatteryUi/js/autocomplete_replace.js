$(document).ready(function () { 

aData = {}
$("#txtBrandName").autocomplete({
	
        source: function (request, response) {
				console.log('test');
            let tt = request.term;
            if (tt.length >= 2) {			
                $.ajax({
                    url: 'http://satvikaitsolutions.com/bindalapi/api/Product/GetAllProductName?brandname=' + request.term,
                    type: 'Get',
                    dataType: 'json',
                    success: function (data) {
                        //console.log(data);
                        aData = $.map(data, function (value, key) {
                            return {
                                id: value.brandName,
                                label: value.brandName
                            };
                        });                  
                        var results = $.ui.autocomplete.filter(aData, request.term);
                        response(results);
                    }
                });
            }
        },
        select: function (event, ui) {
            console.log(ui.item.id);
            sessionStorage.setItem("brandName", ui.item.id)  
           
        }
    });
   
aData1 = {}
$("#txtBatteryType").autocomplete({
    source: function (request, response) {
        let tt = request.term;
        if (tt.length >= 2) {
            $.ajax({
                url: 'http://satvikaitsolutions.com/bindalapi/api/Product/GetProductTypeAutocomplete?brandname='+ sessionStorage.getItem("brandName") +'&batteryType=' + request.term,
                type: 'Get',
                dataType: 'json',
                success: function (data) {
                    console.log(data);
                    aData1 = $.map(data, function (value, key) {
                        return {
                            id: value.productId,
                            label: value.batteryType
                        };
                    });                  
                    var results1 = $.ui.autocomplete.filter(aData1, request.term);
                    response(results1);
                }
            });
        }
    },
    select: function (event, ui) {
        console.log(ui.item.id);
        sessionStorage.setItem("productid", ui.item.id)
    }
}); 

aData1 = {}
$("#txtOldBatterySerno").autocomplete({
    source: function (request, response) {
        let tt = request.term;
        if (tt.length >= 2) {
            $.ajax({
                url: 'http://satvikaitsolutions.com/bindalapi/api/Replace/GetBatterySerNo?productid='+sessionStorage.getItem("productid")+'&batterySerno='+ request.term,
                type: 'Get',
                dataType: 'json',
                success: function (data) {
                    console.log(data);
                    aData1 = $.map(data, function (value, key) {
                        return {
                            id: value.id +',' + value.saleDate,
                            label: value.batterSrNo
                        };
                    });                  
                    var results1 = $.ui.autocomplete.filter(aData1, request.term);
                    response(results1);
                }
            });
        }
    },
    select: function (event, ui) {
		var myArray = ui.item.id.split(",");
		console.log(myArray[1]);
        $('#txtSaleDate').attr('value',myArray[1]); 
		console.log(myArray[0]);
		
        sessionStorage.setItem("saleid", myArray[0])
    }
}); 

aData = {}
$("#txtPartyName").autocomplete({
        source: function (request, response) {
            let tt = request.term;
            if (tt.length >= 2) {
                $.ajax({
                    url: 'http://satvikaitsolutions.com/bindalapi/api/Party/AutoCompleteParty?vwparty=' + request.term,
                    type: 'Post',
                    dataType: 'json',
                    success: function (data) {
                        //console.log(data);
                        aData = $.map(data, function (value, key) {
                            return {
                                id: value.partyMasterId,
                                label: value.partyname
                            };
                        });                  
                        var results = $.ui.autocomplete.filter(aData, request.term);
                        response(results);
                    }
                });
            }
        },
        select: function (event, ui) {
            console.log(ui.item.id);
            sessionStorage.setItem("partyid", ui.item.id) 
           
        }
    });
});