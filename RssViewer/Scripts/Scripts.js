    function FuncOnClick(num) {
        var Number = {
            num: num
        };
        $.ajax({
            type: "POST",
            url: '/Home/NewsView',
            data: Number,
            success: function (data) {
                $("#themes-container").empty();
                $("#themes-container").append(data);
            }
        });
    }
