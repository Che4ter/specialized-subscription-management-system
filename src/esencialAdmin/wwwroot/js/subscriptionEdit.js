    $(document).ready(function () {
        $(".periodePayedCheckbox").change(function () {
            var isChecked = $("periodePayedCheckbox").is(":checked") ? true : false;
            $.ajax({
                url: '/Subscription/updatePayedStatus',
                type: 'POST',
                data: { periodID: $("input:checkbox").val(), paymentState: isChecked }
            });
        });
    });