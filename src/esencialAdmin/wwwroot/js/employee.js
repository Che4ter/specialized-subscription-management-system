function EmployeeDeleteConfirmation(Username) {
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
            var url = "/Employee/Delete";
            $.post(url, { username: Username }, function (data) {
                if (data) {
                    window.location.replace("/Employee/Index");

                }
                else {
                    alert("Es gab ein Problem beim löschen!");
                }
            });
        } else {

        }
    });
}