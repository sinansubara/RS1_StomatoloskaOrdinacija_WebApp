
$(function() {

    $("form[name='formatitula']").validate({

        rules: {
            imetitule: {
                required: true,
                minlength: 2,
                maxlength: 50
            }
        },
        messages: {
            imetitule: {
                required: "Molimo vas unesite ime titule",
                minlength: "Titula ne moze imat manje od 2 slova",
                maxlength: "Titula ne moze imat vise od 50 slova"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});