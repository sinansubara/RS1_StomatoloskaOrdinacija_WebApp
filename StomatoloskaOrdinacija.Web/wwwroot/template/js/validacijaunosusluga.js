
$(function() {

    $("form[name='formaunosdrzave']").validate({

        rules: {

            imedrzave: {
                required: true,
                minlength: 2,
                maxlength: 100
            }
        },
        messages: {
            imedrzave: {
                required: "Molimo vas unesite ime drzave",
                minlength: "Drzava ne moze imat manje od 2 slova",
                maxlength: "Drzava ne moze imat vise od 100 slova"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});