var userLogin = (function () {
    var init = function() {
        $('input[name="userName"]').focus();
        $('#loginForm').bind('submit', function() {
            $("#loginMessage").text("");
            var url = $(this).attr("action");
            var data = $(this).serialize();
            $.ajax({
                url: url,
                type: "POST",
                data: data,
                dataType: "JSON",
                success: function(result) {
                    if (result) {
                        if (result.error) {
                            $("#loginMessage").text(result.error);
                        } else if (result.redirect) {
                            top.location.href = result.redirect;
                        }
                    }
                }
            });
            return false;
        });
    }
    init();

})();