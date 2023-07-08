var currentUser;
var selected = [];
var _GetAllPartyUrl = "user/GetAllUser";
var tableId = "#example";
var cols = [{
    'data': 'partyName'
}, {
    'data': 'mobile'
}, {
    'data': 'place'
}, {
    'data': 'address'
}, {
    'data': 'userType'
}
    , {
    'data': 'approvedDesc'
},
{
    "mRender": function (data, type, row) {
        if (row.isApproved == 0) {
            return '<button type="submit" onclick="event.stopPropagation(); showModal(' + row?.mobile + ')" class="btn btn-secondary" on- id="addrecord" >Approve</button>';
        }
        return '<button type="submit" onclick="event.stopPropagation(); deActivate(' + row?.mobile + ')" class="btn btn-secondary" on- id="addrecord" >Deactivate</button>';
    }
}
];

function showModal(mobile) {
    const rows = table.api().rows().data();
    const currentRow = rows.filter(x => x.mobile === mobile)
    currentUser = currentRow.length ? currentRow[0] : undefined;
    $('#exampleModal').modal('show');
}

function deActivate(mobile) {
    if (confirm('Are you sure you want to lock the user?')) {
        const rows = table.api().rows().data();
        const currentRow = rows.filter(x => x.mobile === mobile)
        const dataToPost = JSON.stringify({
            mobile: currentRow[0].mobile
        });

        $.ajax({
            type: "PATCH",
            url: `${apiBaseUrl}user/LockUser`,
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
            success: function () {
                _loadData({ _apiUrl: _GetAllPartyUrl, tableId, colDef: cols });
            },
            error: function (err) {
                console.log(err);
                showMsgErr();
            }
        });

    } 
}

$(document).ready(function () {
  
   

    // // Setup form validation on the #register-form element
    $("#partform").validate({
        rules: {
            password: {
                required: true,
            },
            confirmPassword: {
                required: true,
                equalTo: "#password"
            }
        },
        messages: {
            password: {
                required: "Password is required",
            },
            confirmPassword: {
                required: "Confirm password is required",
                equalTo: "Confirm password did not match"
            }
        },
        submitHandler: function (form) {
            //form.submit();
        }
    });

    _loadData({ _apiUrl: _GetAllPartyUrl, tableId, colDef: cols });

    // $('#example tbody').on('click', 'tr', function () {
    //     if ($(this).hasClass('selected')) {
    //         $(this).removeClass('selected');
    //     } else {
    //         table.$('tr.selected').removeClass('selected');
    //         $(this).addClass('selected');
    //     }
    // });


    $('#addrecord').click(function () {
        if (!$("#partform").valid()) {
            return false;
        }
        const { password } = serilizeData("#partform");
        const dataToPost = JSON.stringify({
            passCode: password,
            mobile: currentUser.mobile
        });

        $.ajax({
            type: "PATCH",
            url: `${apiBaseUrl}user/ApproveUser`,
            contentType: "application/json; charset=utf-8",
            data: dataToPost,
            success: function () {
                _loadData({ _apiUrl: _GetAllPartyUrl, tableId, colDef: cols });
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


