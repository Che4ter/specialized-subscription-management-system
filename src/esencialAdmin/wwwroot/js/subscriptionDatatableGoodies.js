$(document).ready(function () {
    var goodiestable = $("#goodiesTable").DataTable({
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
            "url": "/Subscription/LoadGoodiesData",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
        [{
            "targets": [0],
            "visible": false,
            "searchable": false
        },
        { "width": "90px", "targets": 1 },
        { "width": "300px", "targets": 2 },
        { "width": "200px", "targets": 3 },
        { "width": "200px", "targets": 4 },
        { "width": "110px", "targets": 5 }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "plantNr", "name": "RebstockNr", "autoWidth": false },
            { "data": "customer", "name": "Kunde", "autoWidth": false },
            { "data": "plan", "name": "Patenschaft", "autoWidth": false },
            { "data": "goodies", "name": "Ernteanteil", "autoWidth": false },
            { "data": "periode", "name": "Laufzeit", "autoWidth": false },
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


    $('#goodiestable').on('draw.dt', function () {

        $(".paginate_button").removeClass("paginate_button").addClass("mui-btn mui-btn--flat");
        $(".mui-btn mui-btn--flat.current").addClass("mui-btn--primary");
    });

    $.contextMenu({
        selector: '#goodiesTable tbody td',
        callback: function (key, options) {
            var cellIndex = parseInt(options.$trigger[0].cellIndex),
                row = goodiestable.row(options.$trigger[0].parentNode),
                rowIndex = row.index();
            switch (key) {
                case 'edit':
                    window.location.href = '/Subscription/Edit/' + goodiestable.cell(rowIndex, 0).data();
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

    $(document).on('change', '.datatableGoodieCheckbox', function () {
        var isChecked = $(this).is(":checked") ? true : false;
        $.ajax({
            url: '/Subscription/updateReceivedGoodie',
            type: 'POST',
            data: { goodyID: $(this).val(), received: isChecked },
            success: function (resp) {
                goodiestable.ajax.reload();

            },
            error: function (req, status, err) {
                console.log('something went wrong', status, err);
            }
        });

    });
});
