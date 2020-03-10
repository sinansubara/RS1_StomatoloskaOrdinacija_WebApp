
jQuery.validator.addMethod("lozinkaExt", function(value, element, param) {
    return value.match(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$/);
},'Lozinka nije ispravna');

$(function() {

    $("form[name='formapromjenazaboravljenelozinke']").validate({

        rules: {
            NovaLozinka : {
                lozinkaExt: true
            },
            PotvrdaNoveLozinke : {
                equalTo : '[name="NovaLozinka"]'
            }
        },

        messages: {
            NovaLozinka : {
                lozinkaExt: "Lozinka mora da sadrzi 8 karaktera[mala-velika slova, brojeve i specijalne znakove]"
            },
            PotvrdaNoveLozinke : {
                equalTo : "Niste unijeli istu lozinku"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});