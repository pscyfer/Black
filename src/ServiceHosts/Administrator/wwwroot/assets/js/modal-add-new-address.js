"use strict";
$(function () {
  var e = $(".select2");
  e.length &&
    e.each(function () {
      var e = $(this);
      e.wrap('<div class="position-relative"></div>').select2({
        placeholder: "انتخاب",
        dropdownParent: e.parent(),
      });
    });
}),
  document.addEventListener("DOMContentLoaded", function () {
    document
      .getElementById("addNewAddress")
      .addEventListener("show.bs.modal", function (e) {
        window.Helpers.initCustomOptionCheck();
      }),
      FormValidation.formValidation(
        document.getElementById("addNewAddressForm"),
        {
          fields: {
            modalAddressFirstName: {
              validators: {
                notEmpty: { message: "لطفا نام خود را وارد کنید" },
                regexp: {
                  regexp: /^[a-zA-Zs]+$/,
                  message: "نام فقط می‌تواند شامل حروف الفبا باشد",
                },
              },
            },
            modalAddressLastName: {
              validators: {
                notEmpty: { message: "لطفا نام خانوادگی خود را وارد کنید" },
                regexp: {
                  regexp: /^[a-zA-Zs]+$/,
                  message: "نام فقط می‌تواند شامل حروف الفبا باشد",
                },
              },
            },
          },
          plugins: {
            trigger: new FormValidation.plugins.Trigger(),
            bootstrap5: new FormValidation.plugins.Bootstrap5({
              eleValidClass: "",
              rowSelector: ".col-12",
            }),
            submitButton: new FormValidation.plugins.SubmitButton(),
            autoFocus: new FormValidation.plugins.AutoFocus(),
          },
        }
      );
  });
