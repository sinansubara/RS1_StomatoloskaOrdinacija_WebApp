jQuery.validator.addMethod("vrijemeExt", function(value, element, param) {
    return value.match(/^([0]?[8-9]|[1]?[0-9]):[03][0]$/);
},'Vrijeme nije ispravno');

$(function() {

    $("form[name='formadodajureditermin']").validate({

        rules: {

            razlog: {
                required: true,
                minlength: 5,
                maxlength: 200
            },
            Vrijeme: {
                required: true,
                vrijemeExt: true
            },
            Datum: {
                required: true
            }
        },

        messages: {
            razlog: {
                required: "Molimo vas unesite razlog pravljenja novog termina",
                minlength: "Razlog termina treba imat minimalno 5 karaktera",
                maxlength: "Razlog termina ne moze imat vise od 200 karaktera"
            },
            Vrijeme: {
                required: "Molimo vas unesite vrijeme pregleda",
                vrijemeExt: "Uneseno vrijeme nije validno"
            },
            Datum: {
                required: "Molimo vas unesite datum termina"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});