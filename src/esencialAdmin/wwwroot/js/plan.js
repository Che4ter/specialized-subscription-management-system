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
                    window.location.replace("/Plan/Index");

                }
                else {
                    alert("Es gab ein Problem beim löschen!");
                }
            });
        } else {

        }
    });
}