
jQuery.validator.addMethod("lozinkaExt", function(value, element, param) {
    return value.match(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$/);
},'Lozinka nije ispravna');

$(function() {

    $("form[name='formazaizmjenulozinkeprofila']").validate({

        rules: {
            staralozinka : {
                required: true
            },
            novalozinka : {
                lozinkaExt: true
            },
            potvrdanovelozinke : {
                equalTo : '[name="novalozinka"]'
            }
        },

        messages: {
            staralozinka : {
                required: "Morate unijeti staru lozinku"
            },
            novalozinka : {
                lozinkaExt: "Lozinka mora da sadrzi 8 karaktera[mala-velika slova, brojeve i specijalne znakove]"
            },
            potvrdanovelozinke : {
                equalTo : "Niste unijeli istu lozinku"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});