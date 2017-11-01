$(document).ready(function () {
    var customertable = $("#customerTable").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 25,
        "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
        "order": [[1, "asc"]],
        //"responsive": {
        //    breakpoints: [
        //        { name: 'desktop', width: Infinity },
        //        { name: 'tablet', width: 992 },
        //        { name: 'fablet', width: 768 },
        //        { name: 'phone', width: 544 }
        //    ]
        //},
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
            "targets": [0], "visible": false, "searchable": false
        },
        { "width": "200px", "targets": 1 },
        { "width": "200px", "targets": 2 },
        { "width": "300px", "targets": 3 },
        { "width": "80px", "targets": 4 },
        { "width": "200px", "targets": 5 }
        ],

        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "firstName", "name": "Vorname", "autoWidth": false },
            { "data": "lastName", "name": "Nachname", "autoWidth": false },
            { "data": "street", "name": "Strasse", "autoWidth": true },
            { "data": "zip", "name": "PLz", "autoWidth": false },
            { "data": "city", "name": "Ort", "autoWidth": true },
            { "data": "email", "name": "Email", "autoWidth": true },
            //{
            //    "render": function (data, type, full, meta)
            //    { return '<a class="btn btn-info" href="/Customer/Edit/' + full.id + '">Edit</a>'; }
            //},
            //{
            //    data: null, render: function (data, type, row) {
            //        return "<a href='#' class='btn btn-danger' onclick=CustomerDeleteConfirmation('" + row.id + "'); >Delete</a>";
            //    }
            //},
        ]

    });


    $('#customerTable').on('draw.dt', function () {

        $(".paginate_button").removeClass("paginate_button").addClass("mui-btn mui-btn--flat");
        $(".mui-btn mui-btn--flat.current").addClass("mui-btn--primary");
    });

    $.contextMenu({
        selector: '#customerTable tbody td',
        callback: function (key, options) {
            var cellIndex = parseInt(options.$trigger[0].cellIndex),
                row = customertable.row(options.$trigger[0].parentNode),
                rowIndex = row.index();
            switch (key) {
                case 'edit':
                    window.location.href = '/Customer/Edit/' + customertable.cell(rowIndex, 0).data();
                    //edit action here
                    break;
                case 'delete':
                    CustomerDeleteConfirmation(customertable.cell(rowIndex, 0).data());
                    break;
                default:
                    break;
            }
        },
        items: {
            "edit": { name: "Edit", icon: "edit" },
            "delete": { name: "Delete", icon: "delete" },
        }
    });


});

function CustomerDeleteConfirmation(CustomerID) {
    swal({
        title: 'Bist du sicher?',
        text: "Das löschen kann nicht rückgängig gemacht werden!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#929292',
        confirmButtonText: 'Ja, löschen.'
    }).then(function () {
        var url = "/Customer/Delete";
        $.post(url, { ID: CustomerID }, function (data) {
            if (data) {
                oTable = $('#customerTable').DataTable();
                oTable.draw();
            }
            else {
                alert("Something Went Wrong!");
            }
        });

    }).catch(swal.noop);
}
