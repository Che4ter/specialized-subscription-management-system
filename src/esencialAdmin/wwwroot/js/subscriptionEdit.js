var pageSize = 50;

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

    $('.btn-periode-edit').on('click', function (e) {
        e.preventDefault();
        $(this).parent().find(".editPeriodeDuration").toggle();

    });

    $('.btn-periode-save').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            url: '/Subscription/updatePeriodeDates',
            type: 'POST',
            data: {
                periodID: $(this).parent().parent().find("#periodeID").val(), periodStartDate: $(this).parent().parent().find(".periodeStartDate").val(), periodEndDate: $(this).parent().find(".periodeEndDate").val()
            },
            statusCode: {
                200: function () {
                    location.reload();
                },
                500: function () {
                    alert("Fehler beim Speichern");
                },
            }
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

    //if ($("#StatusLabel").data("statusId") === 4){
    //    console.log("status4");
    //    $(".periodesFields").prop("disabled", true);
    //}

    $('#giverSelect').select2(
        {
            placeholder: 'Name eingeben',
            theme: "material",
            //Does the user have to enter any data before sending the ajax request
            minimumInputLength: 0,
            allowClear: true,
            multiple: false,
            language: "de",
            ajax: {
                url: '/Subscription/GetCustomers',
                dataType: 'json',
                delay: 120,
                width: 'resolve',
                data: function (params) {
                    var query = {
                        search: params.term,
                        page: params.page,
                        pageSize: pageSize
                    };
                    // Query parameters will be ?search=[term]&page=[page]
                    return query;
                },
                processResults: function (data, params) {
                    // parse the results into the format expected by Select2
                    // since we are using custom formatting functions we do not need to
                    // alter the remote JSON data, except to indicate that infinite
                    // scrolling can be used
                    params.page = params.page || 1;

                    return {
                        results: data.items,
                        pagination: {
                            more: (params.page * pageSize) < data.total
                        }
                    };
                },
                cache: true,
                minimumInputLength: 2
            }
        });

    $('#giverSelect').on('change', function (e) {
        if (e.originalEvent) {
            $.ajax({
                url: '/Subscription/updatePeriodeGiver',
                type: 'POST',
                data: { periodID: $("#periodeID").val(), giverId: $('#giverSelect').val() },
                statusCode: {
                    200: function (xhr) {
                        alert("Schenker wurde erfolgreich gespeichert.");
                    },
                    500: function (xhr) {
                        alert("Error: Schenker konnte nicht gespeichert werden.");
                    },
                }
            });
        }     
    });
   
    $('#giverDeleteButton').on('click', function (e) {
        e.preventDefault();
        $.ajax({
            url: '/Subscription/updatePeriodeGiver',
            type: 'POST',
            data: { periodID: $("#periodeID").val(), giverId: -1 },
            statusCode: {
                200: function (xhr) {
                    $("#giverDeleteButton").parent().parent().find(".customerAddressBlock").empty();
                    $("#giverSelect").val([]).trigger('change');
                    alert("Schenker wurde erfolgreich entfernt.");
                },
                500: function (xhr) {
                    alert("Error: Schenker konnte nicht entfernt werden.");
                },
            }
        });

    });
    
    $('#giverSelectionButton').on('click', function (e) {
        e.preventDefault();
        $(this).parent().parent().find("#giverSelectContainer").toggle();
        $(this).parent().parent().find(".customerAddressBlock").toggle();

    });

    $(".select2-selection__arrow")
        .addClass("material-icons")
        .html("arrow_drop_down");
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