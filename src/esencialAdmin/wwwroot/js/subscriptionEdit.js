    $(document).ready(function () {
        $(".periodePayedCheckbox").change(function () {
            var isChecked = $(this).is(":checked") ? true : false;
            $.ajax({
                url: '/Subscription/updatePayedStatus',
                type: 'POST',
                data: { periodID: $(this).val(), paymentState: isChecked }
            });

            if (this.checked) {
                $(this).parent().parent().children(".paymentMethodContainer").show();
            } else {
                $(this).parent().parent().children(".paymentMethodContainer").hide();
            }
        });
        $(".goodyCheckbox").change(function () {
            var isChecked = $(this).is(":checked") ? true : false;
            $.ajax({
                url: '/Subscription/updateReceivedGoodie',
                type: 'POST',
                data: { goodyID: $(this).val(), received: isChecked }
            });

            if (this.checked) {
                $(this).parent().parent().children(".paymentMethodContainer").show();
            } else {
                $(this).parent().parent().children(".paymentMethodContainer").hide();
            }
        });
        $('.paymentMethodSelect').on('change', function () {

            $.ajax({
                url: '/Subscription/updatePaymentMethod',
                type: 'POST',
                data: { periodID: $(this).parent().parent().parent().find(".periodePayedCheckbox").val(), paymentMethodID: this.value }
            });
        });

        $(".periodePaymentReminderCheckbox").change(function () {
            var isChecked = $(this).is(":checked") ? true : false;
            $.ajax({
                url: '/Subscription/updatePaymentReminderStatus',
                type: 'POST',
                data: { periodID: $(this).val(), reminderState: isChecked }
            });
        });
        $('.periodeWrapper').slick({
            infinite: false,
            slidesToScroll: 1,
            prevArrow: $('.slick-prev'),
            nextArrow: $('.slick-next')
        });

        if ($("#StatusLabel").data("statusId") === 4){
            console.log("status4");
            $(".periodesFields").prop("disabled", true);
        }
});

function SubscriptionDeleteConfirmation(SubID) {
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
        $.post(url, { ID: SubID }, function (data) {
            if (data) {
                window.location.replace("/Subscription/Index");

            }
            else {
                alert("Es gab ein Problem beim löschen!");
            }
        });

    }).catch(swal.noop);
}