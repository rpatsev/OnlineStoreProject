$(function () {
    $("#jsGrid").jsGrid({
        width: "100%",
        height: "auto",

        inserting: false,
        editing: false,
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
                    url: "/Admin/Admin/GetAdministratorsList",
                    dataType: "json",
                    success: function (data) {
                        deferred.resolve(data);
                    }
                });
                return deferred.promise();
            },

            deleteItem: function (item) {
                var id = item.CategoryId;
                $.ajax({
                    type: "POST",
                    url: "/Admin/Admin/Remove/" + item.Id,
                    dataType: "json"
                });
            }
        },

        fields: [
            { name: "Id", css: "hide", width: 0},
            { name: "UserName", type: "text" },
            { name: "Email", type: "text" },
            { type: "control", editButton:false }
        ],
    });
})