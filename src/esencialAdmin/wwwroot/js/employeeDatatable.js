$(document).ready(function () {
    var employeetable = $("#employeeTable").DataTable({
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
            "url": "/Employee/LoadData",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "username", "name": "Benutzername", "autoWidth": true },
            { "data": "firstName", "name": "Vorname", "autoWidth": true },
            { "data": "lastName", "name": "Nachname", "autoWidth": true },
            { "data": "role", "name": "Rolle", "autoWidth": true }
        ]
    });


    $('#employeeTable').on('draw.dt', function () {

        $(".paginate_button").removeClass("paginate_button").addClass("mui-btn mui-btn--flat");
        $(".mui-btn mui-btn--flat.current").addClass("mui-btn--primary");
        });

    $.contextMenu({
        selector: '#employeeTable tbody td',
        callback: function (key, options) {        
            var cellIndex = parseInt(options.$trigger[0].cellIndex),
                row = employeetable.row(options.$trigger[0].parentNode),
                rowIndex = row.index();
            switch (key) {
                case 'edit':
                    window.location.href = '/Employee/Edit/?username=' + employeetable.cell(rowIndex, 0).data() ;
                    //edit action here
                    break;          
                case 'delete':
                    EmployeeDeleteConfirmation(employeetable.cell(rowIndex, 0).data());
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

function EmployeeDeleteConfirmation(Username) {
    swal({
        title: 'Bist du sicher?',
        text: "Das löschen kann nicht rückgängig gemacht werden!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#929292',
        confirmButtonText: 'Ja, löschen.'
    }).then(function () {
        var url = "/Employee/Delete";
        $.post(url, { username: Username }, function (data) {
            if (data) {
                oTable = $('#employeeTable').DataTable();
                oTable.draw();
            }
            else {
                alert("Es gab ein Problem beim löschen!");
            }
        });

    }).catch(swal.noop);
}
