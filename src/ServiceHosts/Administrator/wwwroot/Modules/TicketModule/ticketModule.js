$(function () {
    $("#form-SendMessage").on("submit", function (event) {
        debugger;
        let msg = $("#MessageContent").val();
        event.preventDefault();
        if (msg.length > 0) {
            var form = $(this);
            var data = new FormData();
            var actionUrl = form.attr("action");
            var form_data = $(this).serializeArray();
            $.each(form_data, function (key, input) {
                data.append(input.name, input.value);
            });
            var file_data = $('input[name="File"]')[0].files;
            for (var i = 0; i < file_data.length; i++) {
                data.append("File", file_data[i]);
            }

            $.ajax({
                type: "Post",
                url: actionUrl,
                data: data,
                processData: false,
                contentType: false,
                success: function (response) {
                    if (response.isSuccessed) {
                        ShowSweetSuccessAlert(response.message);
                        location.reload();
                        return;
                    }
                    ShowSweetErrorAlert();
                },
            });
        } else {
            alert("پیام خود را وارد کنید");
        }
    });
});
