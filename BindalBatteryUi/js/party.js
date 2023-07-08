
$(document).ready(function () {
    var selected = [];
    var _GetAllPartyUrl="http://satvikaitsolutions.com/bindalapi/api/party/GetAllParty";
    var tableId="#example";
    var cols=[{
        'data': 'partyMasterId'
    }, {
        'data': 'partyname'
    }, {
        'data': 'mobile'
    }, {
        'data': 'place'
    }
    ];

    // // Setup form validation on the #register-form element
    $("#partform").validate({
        rules: {
            ptname: {
                required:true,
                alphanumeric:true
            }, //firstname is corresponding input name   
            mobl: {
                required: true,
                number: true,
                minlength: 10,
                maxlength: 10
            },
            plce: {
                required:true,
                alphanumeric:true
            },
            gper: {
                required: true,
                number: true,
            }
        },
        messages: {
            ptname: {
                required:"Enter party name",
                alphanumeric:"Only letters, numbers, and underscores allowed"
            }, //firstname is corresponding input name   
            mobl: {
                required: "Enter Mobile",
                number: "Invalid mobile number",
                minlength: "Invalid mobile number",
                maxlength: "Invalid mobile number"
            },
            plce: {
                required:"Enter place",
                alphanumeric:"Only letters, numbers, and underscores allowed"
            },
            gper: {
                required: "Enter Grace Period",
                number: "Invalid Grace Period",
            }
        },
        submitHandler: function (form) {
            //form.submit();
        }
    });

    _loadData({_apiUrl:_GetAllPartyUrl,tableId,colDef:cols});

    $('#example tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    $('#deleterecord').click(function () {
    
        var data = table.api().row('.selected').data();
        var partyMasterId=data.partyMasterId;
        var dataToPost = JSON.stringify({partyMasterId});

        $.ajax({
            type: "POST",
            url: "http://satvikaitsolutions.com/bindalapi/api/party/DeleteParty",
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
            success: function (response) {
                _loadData({_apiUrl:_GetAllPartyUrl,tableId,colDef:cols});
                console.log(response)
                Alert.success("Record deleted");
            },
            error: function (err) {
                console.log(err);
                Alert.error();
            }
        });
    });

    $('#addrecord').click(function () {
        if (!$("#partform").valid()) {
            return false;
        }
        let { ptname, mobl, plce, gper } = serilizeData("#partform");
        var Contact = {
            "gracePeriod": gper,
            "partyname": ptname,
            "place": plce,
            "mobile": mobl
        };
        var dataToPost = JSON.stringify(Contact);
        $.ajax({
            type: "POST",
            url: "http://satvikaitsolutions.com/bindalapi/api/party/InsertParty",
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
            success: function () {
                _loadData({_apiUrl:_GetAllPartyUrl,tableId,colDef:cols});
                resetForm("#partform");
                $('#exampleModal').modal('hide');
            },
            error: function (err) {
                console.log(err);
                showMsgErr();
            }
        });

    });

});


