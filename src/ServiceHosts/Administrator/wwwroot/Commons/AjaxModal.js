$(function () {
    var placeholder = $("#Modal_PlaceHolder");
    $(document).on('click', 'button[data-toggle="ajax-modal"]', function () {
        var url = $(this).data('url');
        $.ajax({
            url: url,
            beforeSend: function () { ShowLoading(); },
            complete: function () { RemoveLoading(); },
            error: function () {
                ShowSweetErrorAlert();
            }
        }).done(function (result) {
            placeholder.empty();
            placeholder.html(result);
            placeholder.find('.modal').modal('show');
        });
    });
    $(document).on('click', 'a[data-toggle="ajax-modal"]', function () {
        var url = $(this).data('url');
        $.ajax({
            url: url,
            beforeSend: function () { ShowLoading(); },
            complete: function () { RemoveLoading(); },
            error: function () {
                ShowSweetErrorAlert();
            }
        }).done(function (result) {
            placeholder.empty();
            placeholder.html(result);
            placeholder.find('.modal').modal('show');
        });
    });


   
    placeholder.on('click', 'button[data-save="modal"]', function () {
         ShowLoading();
        var form = $(this).parents(".modal").find('form');
        var actionUrl = form.attr('action');
        if (form.length == 0) {
            form = $(".card-body").find('form');
            actionUrl = form.attr('action') + '/' + $(".modal").attr('id');
        }
        var dataToSend = new FormData(form.get(0));
        $.ajax({
            url: actionUrl, type: "post", data: dataToSend, processData: false, contentType: false, error: function () {
                ShowSweetErrorAlert();
                RemoveLoading();
            }
        }).done(function (data) {
            var newBody = $(".modal-body", data);
            var newFooter = $(".modal-footer", data);
            placeholder.find(".modal-body").replaceWith(newBody);
            placeholder.find(".modal-footer").replaceWith(newFooter);
            var IsValid = newBody.find("input[name='IsValid']").val() === "True";
            var IsRefreshLoaction = newBody.find("input[name='IsRefreshLoaction']").val();

            if (IsValid) {
                $.ajax({
                    url: '/Base/SwalNotificationActionResult',
                    error: function () {
                        alert("Error...");
                       
                    }
                }).done(function (notification) {
                    ShowSweetSuccessAlert(notification)
                    try {
                        placeholder.find(".modal").modal('hide');
                        if (IsRefreshLoaction.toLocaleLowerCase() === "true") {
                            setInterval(function () { location.reload() }, 1500);
                        }
                    } catch (e) {
                        if (IsRefreshLoaction.toLocaleLowerCase() === "true") {
                            setInterval(function () { location.reload() }, 1500);
                        }
                        placeholder.find(".modal").modal('hide');

                    }

                });
            }

        });

        RemoveLoading();
    });
    placeholder.on('click', 'button[data-save="tab"]', function (e) {
        // ShowLoading();
        var form = $(this).parents('#tab-from');
        debugger
        console.log(e);
        var actionUrl = form.attr('action');
        //if (form.length == 0) {
        //    form = $(".card-body").find('form');
        //    actionUrl = form.attr('action') + '/' + $(".modal").attr('id');
        //}
        var dataToSend = new FormData(form.get(0));
        $.ajax({
            url: actionUrl, type: "post", data: dataToSend, processData: false, contentType: false, error: function () {
                ShowSweetErrorAlert();
                RemoveLoading();
            }
        }).done(function (data) {
          debugger
            var tabPlaceHolder = form.parents(".tabcontainer");
            var newbody = $(data);
            tabPlaceHolder.replaceWith(data);
            var IsValid = newbody.find("input[name='IsValid']").val() === "True";
            var IsRefreshLoaction = newbody.find("input[name='IsRefreshLoaction']").val();
            if (IsValid) {
                if (IsRefreshLoaction.toLocaleLowerCase() === "true") {
                    setInterval(function () { location.reload() }, 1000);
                }
            }

        });

        RemoveLoading();
    });

});
function ShowSweetSuccessAlert(message) {

    try {
        Swal.fire({
            position: "centered",
            icon: "success",
            title: message,
            showConfirmButton: !1,
            timer: 1500,
            customClass: { confirmButton: "btn btn-primary" },
            buttonsStyling: !1,
        });
    } catch (e) {
        Swal.fire({
            title: "موفقیت امیز",
            text: message,
            icon: "success",
            button: "بستن",
        })
    }

    RemoveLoading();
}
function ShowSweetErrorAlert() {

    try {
        Swal.fire({
            icon: "error",
            title: "خطایی سمت سرور رخ داده است.",
            showConfirmButton: !1,
            timer: 1500,
            customClass: { confirmButton: "btn btn-primary" },
            buttonsStyling: !1,
        });
    } catch (e) {
        Swal.fire({
            title: " خطاا",
            text: "خطاای رخ داده لطفا منتظر بمانید",
            icon: "error",
            button: "بستن",
        })
    }
}
function ShowLoading() {
    $(".sk-chase").removeClass("d-none");
}
function RemoveLoading() {
    $(".sk-chase").addClass("d-none");
}
