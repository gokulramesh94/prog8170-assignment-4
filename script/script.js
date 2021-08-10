$(document).ready(function () {
  let $formField = $(".form-wrapper .form .form-field"),
    $submitBtn = $(".form-wrapper .form input:submit"),
    postalCodeRegex = /^(\d{5}(-\d{4})?|\d[A-Z]\d ?[A-Z]\d[A-Z])$/,
    phoneNumberRegex = /^\(?\d{3}\)?[-]?\d{3}[\s.-]\d{4}$/,
    emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/,
    USER_KEY = "user";

  $submitBtn.on("click", function (event) {
    event.preventDefault();
    event.stopPropagation();
    functions.validateFields();
  });

  let data = JSON.parse(localStorage.getItem(USER_KEY)) || [];
  if (data.length > 0) {
    $(".last-saved-record").removeClass("d-none");
  }

  let functions = (function () {
    let validateFields = function () {
      let check = true;
      $formField.each((index, element) => {
        if ($(element).val() == null || $(element).val() === "") {
          alert("All fields are mandatory!");
          check = false;
          return false;
        }
      });
      if (check) {
        if (
          !emailRegex.test($(".form-wrapper .form input[name='email']").val())
        ) {
          check = false;
          alert("Invalid Email!");
        }
        if (
          !postalCodeRegex.test(
            $(".form-wrapper .form input[name='postalCode']").val()
          )
        ) {
          check = false;
          alert(
            "Postal Code (Acceptable format: NAN ANA – where 'A' is any alphabetic character and 'N' is avalid numeric digit)"
          );
        }
        if (
          !phoneNumberRegex.test(
            $(".form-wrapper .form input[name='phoneNumber']").val()
          )
        ) {
          check = false;
          alert("Acceptable formats: 123-123-1234, or (123)123-1234");
        }
        if (check) {
          var formData = $(".form-wrapper .form").serializeArray();
          var data = JSON.parse(localStorage.getItem(USER_KEY)) || [];
          var obj = {};
          formData.forEach((element) => {
            obj[element.name] = element.value;
          });
          data.push(obj);
          localStorage.setItem(USER_KEY, JSON.stringify(data));
          $formField.each((index, element) => {
            $(element).val("");
          });
          $(".last-saved-record").removeClass("d-none");
          alert("User Data added successfully!");
        }
      }
    };

    return {
      validateFields: validateFields,
    };
  })();
});
