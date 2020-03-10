
$(function() {

    $("form[name='formatitula']").validate({

        rules: {
            imetitule: {
                required: true,
                minlength: 1,
                maxlength: 50
            }
        },
        messages: {
            imetitule: {
                required: "Molimo vas unesite ime drzave",
                minlength: "Drzava ne moze imat manje od 1 slova",
                maxlength: "Drzava ne moze imat vise od 50 slova"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});