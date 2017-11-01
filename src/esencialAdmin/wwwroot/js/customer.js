


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
                window.location.replace("/Customer/Index");

            }
            else {
                alert("Something Went Wrong!");
            }
        });

    }).catch(swal.noop);
}


