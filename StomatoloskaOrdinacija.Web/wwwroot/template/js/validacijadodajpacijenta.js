jQuery.validator.addMethod("emailExt", function(value, element, param) {
    return value.match(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/);
},'Email nije ispravan');

$(function() {

    $("form[name='formadodajpacijenta']").validate({

        rules: {

            ime: {
                required: true,
                minlength: 2,
                maxlength: 100
            },
            prezime: {
                required: true,
                minlength: 3,
                maxlength: 100
            },
            jmbg: {
                required: true,
                digits: true,
                minlength: 13,
                maxlength: 13
            },
            mobitel: {
                required: true,
                digits: true,
                minlength: 9,
                maxlength: 30
            },
            datumRodjenja: {
                required: true
            },
            gradid: {
                required: true,
                min: 1
            },
            spol: {
                required: true
            },
            adresa: {
                required: true,
                minlength: 2,
                maxlength: 200
            },
            email: {
                required: true,
                emailExt: true,
                maxlength: 320,
                email: true
            }
        },

        messages: {
            ime: {
                required: "Molimo vas unesite ime",
                minlength: "Ime mora sadrzavat najmanje 2 slova",
                maxlength: "Ime ne smije sadrzavat preko 100 slova"
            },
            prezime: {
                required: "Molimo vas unesite prezime",
                minlength: "Prezime mora sadrzavat najmanje 3 slova",
                maxlength: "Prezime ne smije sadrzavat preko 100 slova"
            },
            jmbg: {
                required: "Molimo vas unesite JMBG",
                digits: "JMBG mora sadrzavat samo brojeve",
                minlength: "JMBG mora sadrzavat tacno 13 brojeva",
                maxlength: "JMBG mora sadrzavat tacno 13 brojeva"
            },
            mobitel: {
                required: "Molimo vas unesite broj telefona",
                digits: "Broj telefona mora sadrzavat samo brojeve, primjer: 061222333",
                minlength: "Broj telefona mora imat minimalno 9 brojeva",
                maxlength: "Broj telefona ne moze biti duzi od 30"
            },
            datumRodjenja: {
                required: "Molimo vas odaberite datum rodjenja"
            },
            gradid: {
                required: "Molimo vas odaberite grad",
                min: "Niste odabrali grad!"
            },
            spol: {
                required: "Molimo vas odaberite spol"
            },
            adresa: {
                required: "Molimo vas unesite adresu",
                minlength: "Adresa mora imat minimalno 2 karaktera",
                maxlength: "Adresa ne moze biti duza od 200 karaktera"
            },
            email: {
                required: "Molimo vas unesite email",
                emailExt: "Email koji ste unijeli nije validan",
                maxlength: "Email ne moze sadrzavat vise od 320 karaktera",
                email: "Email koji ste unijeli nije validan"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});