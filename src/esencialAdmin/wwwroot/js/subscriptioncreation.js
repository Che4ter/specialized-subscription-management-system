


//The url we will send our get request to
var pageSize =25;

$('#customersSelect').select2(
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
            delay: 150 ,
            width: 'resolve',
            data: function (params) {
                var query = {
                    search: params.term,
                    page: params.page,
                    pageSize: pageSize,
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
            delay: 150,
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

$('#planSelect').select2(
    {
        placeholder: 'Name eingeben',
        theme: "material",
        //Does the user have to enter any data before sending the ajax request
        minimumInputLength: 0,
        allowClear: true,
        multiple: false,
        language: "de",
        ajax: {
            url: '/Subscription/GetPlans',
            dataType: 'json',
            delay: 150,
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

$("#payedCheckbox").change(function () {
    if (this.checked) {
        $("#paymentMethodContainer").show();
    } else {
        $("#paymentMethodContainer").hide();

    }
});

$(".select2-selection__arrow")
    .addClass("material-icons")
    .html("arrow_drop_down");