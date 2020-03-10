
$(function() {

    $("form[name='formazaunosgrada']").validate({

        rules: {
            drzavaid: {
                required: true,
                min: 1
            },
            imegrada: {
                required: true,
                minlength: 2,
                maxlength: 100
            },
            postanskibroj: {
                required: true,
                minlength: 2,
                maxlength: 20
            }
        },
        messages: {
            drzavaid: {
                required: "Morate izabrat drzavu",
                min: "Morate izabrat drzavu"
            },
            imegrada: {
                required: "Morate unijeti ime grada",
                minlength: "Ime grada ne moze biti krace od 2 slova",
                maxlength: "Ime grada ne moze biti duze od 100 slova"
            },
            postanskibroj: {
                required: "Morate unijeti postanski broj",
                minlength: "Postanski broj ne moze biti kraci od 2 broja",
                maxlength: "Postanski broj ne moze biti duzi od 20 brojeva"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});