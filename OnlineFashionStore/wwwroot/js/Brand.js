
$(document).ready(function () {
    GetBrands();
});
function GetBrands()
{
    $.ajax({
        url: '/brand/GetBrands',
        type: 'get',
        dataType: 'json', 
        contentType: 'application/json;charset=utf-8',
        success: function (response) { 
            if (response == null || response == undefined || response.length == 0) {
                var object = '';

                object += '<tr>';
                object += '<td>' + 'Brands not available' + '</td>';
                object += '</tr>';
                $('#tblBody').html(object);
            } else {
                var object = '';
                $.each(response, function (index, item) {
                    object += '<tr>';
                    object += '<td>' + item.Id + '</td>';
                    object += '<td>' + item.Name + '</td>';
                   object += '<td><a href="#" class="btn btn-primary btn-sm" onclick="Edit(' + item.id + ')">Edit</a> <a href="#" class="btn btn-danger btn-sm" onclick="Delete(' + item.id + ')">Delete</a></td>';
                    object += '</tr>';
                });
                $('#tblBody').html(object);
            }
        },
        error: function () {
            alert('Unable to fetch data'); 
        }
    });
}