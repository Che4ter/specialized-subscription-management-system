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

     
        $('.periodeWrapper').slick({
            infinite: false,
            slidesToScroll: 1,
            prevArrow: $('.slick-prev'),
            nextArrow: $('.slick-next')
        });
    });