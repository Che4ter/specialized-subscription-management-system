


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
            delay: 250 ,
            width: 'resolve',
            data: function (params) {
                var query = {
                    search: params.term,
                    page: params.page,
                    pageSize: pageSize,
                }
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
       

          
            minimumInputLength: 2,
        }
    }); 

$(".select2-selection__arrow")
    .addClass("material-icons")
    .html("arrow_drop_down");