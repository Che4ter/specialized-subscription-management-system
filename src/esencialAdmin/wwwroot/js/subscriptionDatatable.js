$(document).ready(function () {
    var subscriptiontable = $("#subscriptionTable").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "searchDelay": 500,
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 25,
        "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
        "language": {
            "url": "/lib/DataTables/dataTablesGerman.json"
        },
        "ajax": {
            "url": "/Subscription/LoadDefaultData",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.planId = $("#PlanMethodSelect option:selected").val() ;
                d.statusId = $("#StatusMethodSelect option:selected").val() ;
                //d.goody = $("#goodyCheckbox").is(":checked");

            }
        },
        "columnDefs":
        [{
            "targets": [0],
            "visible": false,
            "searchable": false
        },
        { "width": "100px", "targets": 1 },
        { "width": "300px", "targets": 2 },
        { "width": "300px", "targets": 3 },
        { "width": "110px", "targets": 4 },
        { "width": "110px", "targets": 5 },
        { "width": "110px", "targets": 6 },

     ],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "plantNr", "name": "RebstockNr", "autoWidth": false },
            { "data": "customer", "name": "Kunde", "autoWidth": false },
            { "data": "plan", "name": "Patenschaft", "autoWidth": false },
            { "data": "periode", "name": "Laufzeit", "autoWidth": false },
            { "data": "payed", "name": "Bezahlt", "autoWidth": false },
            { "data": "status", "name": "Status", "autoWidth": true },
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

    $('#subscriptionTable').on('draw.dt', function () {

        $(".paginate_button").removeClass("paginate_button").addClass("mui-btn mui-btn--flat");
        $(".mui-btn mui-btn--flat.current").addClass("mui-btn--primary");
    });

    $('select').on('change', function () {

        subscriptiontable.ajax.reload();

    });

    $.contextMenu({
        selector: '#subscriptionTable tbody td',
        callback: function (key, options) {
            var cellIndex = parseInt(options.$trigger[0].cellIndex),
                row = subscriptiontable.row(options.$trigger[0].parentNode),
                rowIndex = row.index();
            switch (key) {
                case 'edit':
                    window.location.href = '/Subscription/Edit/' + subscriptiontable.cell(rowIndex, 0).data();
                    //edit action here
                    break;
                default:
                    break;
            }
        },
        items: {
            "edit": { name: "Bearbeiten", icon: "edit" },
        }
    });

    $(function () {
        $("select").removeAttr("multiple");
    });
});
