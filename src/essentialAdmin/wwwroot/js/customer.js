$(document).ready(function () {
    $("#customerTable").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "language": {
            "url": "/lib/DataTables/dataTablesGerman.json"
        },
        "ajax": {
            "url": "/Customer/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
        [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }], 
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },  
            { "data": "firstName", "name": "Vorname", "autoWidth": true },
            { "data": "lastName", "name": "Nachname", "autoWidth": true },
            { "data": "street", "name": "Strasse", "autoWidth": true },
            { "data": "zip", "name": "PLz", "autoWidth": true },
            { "data": "city", "name": "Ort", "autoWidth": true },
            { "data": "email", "name": "Email", "autoWidth": true },

            {
                "render": function (data, type, full, meta)
                { return '<a class="btn btn-info" href="/Customer/Edit/' + full.id + '">Edit</a>'; }
            },
            {
                data: null, render: function (data, type, row) {
                    return "<a href='#' class='btn btn-danger' onclick=DeleteData('" + row.id + "'); >Delete</a>";
                }
            },
        ]

    });
});


function DeleteData(CustomerID) {
    if (confirm("Are you sure you want to delete ...?")) {
        Delete(CustomerID);
    }
    else {
        return false;
    }
}


function Delete(CustomerID) {
    var url = '@Url.Content("~/")' + "Customer/Delete";

    $.post(url, { ID: CustomerID }, function (data) {
        if (data) {
            oTable = $('#customerTable').DataTable();
            oTable.draw();
        }
        else {
            alert("Something Went Wrong!");
        }
    });
}