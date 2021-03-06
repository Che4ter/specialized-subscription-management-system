﻿$(document).ready(function () {
    var plantable = $("#planTable").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 25,
        "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],

        "language": {
            "url": "/lib/DataTables/dataTablesGerman.json"
        },
        "ajax": {
            "url": "/Plan/LoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
        [{
            "targets": [0],
            "visible": false,
            "searchable": false
        },
            { "width": "300px", "targets": 1 },
            { "width": "130px", "targets": 2 },
            { "width": "130px", "targets": 3 }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "name", "name": "Name", "autoWidth": true },
            { "data": "price", "name": "Preis", "autoWidth": true },
            { "data": "duration", "name": "Laufzeit", "autoWidth": true },
            { "data": "inuse", "name": "In Gebrauch", "autoWidth": true },

            //{
            //    "render": function (data, type, full, meta)
            //    { return '<a class="btn btn-info" href="/Plan/Edit/' + full.id + '">Edit</a>'; }
            //},
            //{
            //    data: null, render: function (data, type, row) {
            //        return "<a href='#' class='btn btn-danger' onclick=PlanDeleteConfirmation('" + row.id + "'); >Delete</a>";
            //    }
            //},
        ]

    });


    $('#planTable').on('draw.dt', function () {

        $(".paginate_button").removeClass("paginate_button").addClass("mui-btn mui-btn--flat");
        $(".mui-btn mui-btn--flat.current").addClass("mui-btn--primary");
    });

    $.contextMenu({
        selector: '#planTable tbody td',
        callback: function (key, options) {
            var cellIndex = parseInt(options.$trigger[0].cellIndex),
                row = plantable.row(options.$trigger[0].parentNode),
                rowIndex = row.index();
            switch (key) {
                case 'edit':
                    window.location.href = '/Plan/Edit/' + plantable.cell(rowIndex, 0).data();
                    //edit action here
                    break;
                case 'delete':
                    PlanDeleteConfirmation(plantable.cell(rowIndex, 0).data());
                    break;
                default:
                    break;
            }
        },
        items: {
            "edit": { name: "Bearbeiten", icon: "edit" },
            "delete": { name: "Löschen", icon: "delete" },
        }
    });


});

function PlanDeleteConfirmation(PlanID) {
    swal({
        title: 'Bist du sicher?',
        text: "Das löschen kann nicht rückgängig gemacht werden!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#929292',
        confirmButtonText: 'Ja, löschen.'
    }).then(result => {
        if (result.value) {
            var url = "/Plan/Delete";
            $.post(url, { ID: PlanID }, function (data) {
                if (data) {
                    oTable = $('#planTable').DataTable();
                    oTable.draw();
                }
                else {
                    alert("Es gab ein Problem beim löschen!");
                }
            });
        } else {

        }
    });
}
