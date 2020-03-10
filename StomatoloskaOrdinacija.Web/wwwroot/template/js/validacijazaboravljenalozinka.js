jQuery.validator.addMethod("emailExt", function(value, element, param) {
    return value.match(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/);
},'Email nije ispravan');
$(function() {

    $("form[name='formazaboravljenalozinka']").validate({

        rules: {

            email: {
                required: true,
                emailExt: true,
                email: true
            }
        },
        messages: {
            email: {
                required: "Molimo vas unesite email",
                emailExt: "Email koji ste unijeli nije validan",
                email: "Email koji ste unijeli nije validan"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});