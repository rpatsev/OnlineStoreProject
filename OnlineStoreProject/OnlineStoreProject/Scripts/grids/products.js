var categories;


function getCategories() {
    $.ajax({
        method: "GET",
        url: "/Admin/Category/GetCategoriesSelectList",
        dataType: "json",
        success: function (response) {
            categories = response;
            $("#jsGrid").jsGrid({
                width: "100%",
                height: "auto",

                inserting: true,
                editing: true,
                sorting: true,
                paging: true,
                autoload: true,

                pageSize: 10,

                controller: {
                    loadData: function () {
                        var deferred = $.Deferred();

                        $.ajax({
                            type: "GET",
                            url: "/Admin/Product/GetAllProducts",
                            dataType: "json",
                            success: function (data) {
                                deferred.resolve(data);
                            }
                        });
                        return deferred.promise();

                    },

                    insertItem: function (item) {
                        item.CategoryId = item.Category;
                        $.ajax({
                            type: "POST",
                            url: "/Admin/Product/Create/",
                            data: item,
                            dataType: "json"
                        });
                    },

                    updateItem: function (item) {
                        var load = this.loadData;
                        var id = item.ProductId;
                        $.ajax({
                            type: "POST",
                            url: "/Admin/Product/Edit/" + id,
                            data: item,
                            dataType: "json",
                        });
                    },

                    deleteItem: function (item) {
                        var id = item.ProductId;
                        $.ajax({
                            type: "POST",
                            url: "/Admin/Product/Remove/" + id,
                            dataType: "json"
                        });
                    },
                },
                fields: [
                    { name: "Name", type: "text", validate: "required" },
                    { name: "Price", type: "number", validate: "required", width: 60 },
                    { name: "Volume", type: "number", width: 60},
                    { name: "Alcohol", type: "number", width: 60},
                    { name: "Country", type: "text" },
                    { name: "Manufacturer", type: "text", width: 150 },
                    { name: "CategoryId", title: "Category", type: "select", items: categories, valueField: "CategoryId", textField: "Name", validate: "required"},
                    { name: "Description", type: "textarea", width: 320 },
                    { name: "InStock", type: "checkbox", items: [{ Id: true, Name: "true" }, { Id: false, Name: "false" }], valueField: "Id", textField: "Name"},
                    { type: "control" }
                ],
            });
        }
    });
}


getCategories();
