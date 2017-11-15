$(document).ready(function () {
    var plantable = $("#subscriptionTable").DataTable({
        "processing": true, // for show progress bar
        "serverSide": true, // for process server side
        "filter": true, // this is for disable filter (search box)
        "orderMulti": false, // for disable multiple column at once
        "pageLength": 25,
        "lengthMenu": [[10, 25, 50, 100], [10, 25, 50, 100]],
        "responsive": {
            breakpoints: [
                { name: 'desktop', width: Infinity },
                { name: 'tablet', width: 992 },
                { name: 'fablet', width: 768 },
                { name: 'phone', width: 544 }
            ]
        },
        "language": {
            "url": "/lib/DataTables/dataTablesGerman.json"
        },
        "ajax": {
            "url": "/Subscription/LoadDefaultData",
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
            { "data": "plantNr", "name": "RebstockNr", "autoWidth": true },
            { "data": "customer", "name": "Kunde", "autoWidth": true },
            { "data": "plan", "name": "Patenschaft", "autoWidth": true },
            { "data": "periode", "name": "Laufzeit", "autoWidth": true },
            { "data": "payed", "name": "Bezahlt", "autoWidth": true },

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

    $.contextMenu({
        selector: '#subscriptionTable tbody td',
        callback: function (key, options) {        
            var cellIndex = parseInt(options.$trigger[0].cellIndex),
                row = plantable.row(options.$trigger[0].parentNode),
                rowIndex = row.index();
            switch (key) {
                case 'edit':
                    window.location.href = '/Subscription/Edit/' + plantable.cell(rowIndex, 0).data() ;
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
            "edit": { name: "Edit", icon: "edit" },         
            "delete": { name: "Delete", icon: "delete" },
        }
    });


});

function SubscriptionDeleteConfirmation(PlanID) {
    swal({
        title: 'Bist du sicher?',
        text: "Das löschen kann nicht rückgängig gemacht werden!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#929292',
        confirmButtonText: 'Ja, löschen.'
    }).then(function () {
        var url = "/Subscription/Delete";
        $.post(url, { ID: PlanID }, function (data) {
            if (data) {
                oTable = $('#subscriptionTable').DataTable();
                oTable.draw();
            }
            else {
                alert("Something Went Wrong!");
            }
        });

    }).catch(swal.noop);
}
