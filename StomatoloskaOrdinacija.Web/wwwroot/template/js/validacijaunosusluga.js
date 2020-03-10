
$(function() {

    $("form[name='formausluga']").validate({

        rules: {

            imeusluge: {
                required: true,
                minlength: 2,
                maxlength: 150
            }
        },
        messages: {
            imeusluge: {
                required: "Molimo vas unesite ime usluge",
                minlength: "Usluga ne moze imat manje od 2 slova",
                maxlength: "Usluga ne moze imat vise od 150 slova"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});