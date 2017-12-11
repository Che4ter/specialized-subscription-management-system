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
            window.location.href = "/Customer/Index";

        }).fail(function () {
            window.location.href = "/Customer/Edit/" + CustomerID;

        });

    }).catch(swal.noop);
}