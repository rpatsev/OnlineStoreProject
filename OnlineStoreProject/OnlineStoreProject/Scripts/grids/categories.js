$(function () {


    $("#jsGrid").jsGrid({
        width: "100%",
        height: "400px",

        inserting: true,
        editing: true,
        sorting: true,
        paging: true,
        autoload: true,


        //data: clients,
        pageSize: 10,

        controller: {
            loadData: function () {
                var deferred = $.Deferred();

                $.ajax({
                    type: "GET",
                    url: "/Admin/Category/GetAllCategories",
                    dataType: "json",
                    success: function (data) {
                        deferred.resolve(data);
                    }
                });
                return deferred.promise();
            },

            insertItem: function (item) {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Category/Create/",
                    data: item,
                    dataType: "json"
                });
            },

            updateItem: function (item) {
                var id = item.CategoryId;
                $.ajax({
                    type: "POST",
                    url: "/Admin/Category/Edit/" + id,
                    data: item,
                    dataType: "json"
                });
            },

            deleteItem: function (item) {
                var id = item.CategoryId;
                $.ajax({
                    type: "POST",
                    url: "/Admin/Category/Remove/" + id,
                    dataType: "json"
                });
            }
        },

        fields: [
            { name: "Name", type: "text", width: 150, validate: "required" },
            { name: "Description", type: "text", width: 150 },
            { type: "control" }
        ],
    });



})