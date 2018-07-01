var pageSize = 50;

$(document).ready(function () {
    $(".periodePayedCheckbox").change(function () {
        var isChecked = $(this).is(":checked") ? true : false;
        $.ajax({
            url: '/Subscription/updatePayedStatus',
            type: 'POST',
            data: { periodID: $(this).val(), paymentState: isChecked }
        });
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

    $('.btnPeriodeDurationEdit').on('click', function (e) {
        e.preventDefault();
        $(this).parent().find(".editPeriodeDuration").toggle();

    });

    $('.btnPeriodePriceEdit').on('click', function (e) {
        e.preventDefault();
        $(this).parent().find(".editPeriodePrice").toggle();

    });

    $('.btnPeriodeDurationSave').on('click', function (e) {
        e.preventDefault();
        var parentelement = $(this).parent().parent();
        console.log(parentelement);
        $.ajax({
            url: '/Subscription/updatePeriodeDates',
            type: 'POST',
            data: {
                periodID: parentelement.find(".periodeID").val(), periodStartDate: parentelement.find(".periodeStartDate").val(), periodEndDate: parentelement.find(".periodeEndDate").val()
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

    $('.giverSelect').select2(
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

    $('.giverSelect').on('change', function (e,deleteflag) {
        if (deleteflag != "delete") {
            var pID = $(this).parent().find(".hidden-periode-id").val();
            var gID = $(this).val();

            $.ajax({
                url: '/Subscription/updatePeriodeGiver',
                type: 'POST',
                data: {
                    periodID: pID, giverID: gID
                },
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

    $('#subscriptionRemarks').on('change', function (e) {
        if (e.originalEvent) {
            $.ajax({
                url: '/Subscription/updateSubscriptionRemarks',
                type: 'POST',
                data: { subscriptionId: $("#subId").val(), subscriptionRemarks: $('#subscriptionRemarks').val() },
                statusCode: {
                    200: function (xhr) {
                    },
                    500: function (xhr) {
                        alert("Error: Bemerkung konnte nicht gespeichert werden.");
                    },
                }
            });
        }
    });

    $('.btnPeriodePriceUpdate').on('click', function (e) {
        e.preventDefault();
        var newPrice = $(this).parent().find(".txtPeriodePrice").val();
        console.log(newPrice);
        var tmp = $(this).parent().parent().parent().find(".periodePriceLabel");
        var pID = $(this).val()
        $.ajax({
            url: '/Subscription/updatePeriodePrice',
            type: 'POST',
            data: { periodeId: pID, newPeriodePrice: newPrice },
            statusCode: {
                200: function (xhr) {
                    tmp.text(" CHF " + newPrice);
                },
                500: function (xhr) {
                },
            }
        });

    });

    $('.giverDeleteButton').on('click', function (e) {
        e.preventDefault();
        console.log(pID);
        var sendercontainer = $(this).parent().parent();
        var pID = sendercontainer.find(".hidden-periode-id").val();

        $.ajax({
            url: '/Subscription/updatePeriodeGiver',
            type: 'POST',
            data: { periodID: pID, giverId: -1 },
            statusCode: {
                200: function (xhr) {
                    sendercontainer.find(".customerAddressBlock").empty();
                    sendercontainer.find(".giverSelect").val([]).trigger('change', ["delete"]);
                    alert("Schenker wurde erfolgreich entfernt.");
                },
                500: function (xhr) {
                    alert("Error: Schenker konnte nicht entfernt werden.");
                },
            }
        });

    });

    $('.giverSelectionButton').on('click', function (e) {
        e.preventDefault();
        $(this).parent().parent().find(".giverSelectContainer").toggle();
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
    }).then(result => {
        if (result.value) {
            var url = "/Subscription/Delete";
            $.post(url, { ID: SubID }, function (data) {
                if (data) {
                    window.location.replace("/Subscription/Index");

                }
                else {
                    alert("Es gab ein Problem beim löschen!");
                }
            });
        } else {

        }
    });
}