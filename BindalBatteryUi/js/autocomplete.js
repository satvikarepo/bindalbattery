$(document).ready(function () { 

    aData = {}
    $("#Party").autocomplete({
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
   

aData1 = {}
$("#Product").autocomplete({
    source: function (request, response) {
        let tt = request.term;
        if (tt.length >= 2) {
            $.ajax({
                url: 'http://satvikaitsolutions.com/bindalapi/api/Product/GetProductAutocomplete?subproduct=' + request.term,
                type: 'Post',
                dataType: 'json',
                success: function (data) {
                    console.log(data);
                    aData1 = $.map(data, function (value, key) {
                        return {
                            id: value.productId,
                            label: value.brandName
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
   





});