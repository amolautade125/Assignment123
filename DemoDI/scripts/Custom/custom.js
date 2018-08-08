$(function () {

    $('.btnAddNew').unbind('click').bind('click', function () {
        $.ajax({
            url: 'http://localhost:49460/Contact/addContact',
            method: 'GET',
            success: function (data) {
                
                //alert('success');
                $('#myModal').modal('show');
                $('#myModal').find('.modal-title').text('Add Customer');
                $('#myModal').find('.modal-body').html(data);
            },
            error: function (e) {
                
                alert('error');
            }
        });
    });

    $('.btnEdit').unbind('click').bind('click', function () {
        
        var id = $(this).parents('tr').prop('id');
        ShowAddEditPopup(id);
        
    });

    $('.btnDelete').unbind('click').bind('click', function () {

        if (confirm("Are you sure you want to delete?"))
        {
            var id = $(this).parents('tr').prop('id');
            deleteContactById(id);
        }
    });
});

function deleteContactById(id)
{
    if (parseInt(id) > 0)
    {

    }
    else
    {
        alert("Invalid Contact id.");
    }
}

function ShowAddEditPopup(id)
{
    var url = 'http://localhost:49460/Contact/addContact';

    if (id != 0)
    {
        url = url + '/' + id;
    }

    $.ajax({
        url: url,
        method: 'GET',
        success: function (data) {
            
            //alert('success');
            $('#myModal').modal('show');
            $('#myModal').find('.modal-title').text('Add Customer');
            $('#myModal').find('.modal-body').html(data);
        },
        error: function (e) {
            
            alert('error');
        }
    });
}