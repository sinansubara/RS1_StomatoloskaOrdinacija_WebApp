jQuery.validator.addMethod("emailExt", function(value, element, param) {
    return value.match(/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/);
},'Email nije ispravan');

$(function() {

    $("form[name='formadodajosoblje']").validate({

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
            datumZaposlenja: {
                required: true
            },
            titulaid: {
                required: true,
                min: 1
            },
            adresa: {
                required: true,
                minlength: 2,
                maxlength: 200
            },
            brojziroracuna: {
                required: true,
                digits: true,
                minlength: 6,
                maxlength: 20
            },
            opisposla: {
                required: true,
                minlength: 2,
                maxlength: 200
            },
            email: {
                required: true,
                emailExt: true,
                maxlength: 320
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
            datumZaposlenja: {
                required: "Molimo vas odaberite datum zaposlenja"
            },
            titulaid: {
                required: "Molimo vas odaberite titulu",
                min: "Niste odabrali titulu!"
            },
            adresa: {
                required: "Molimo vas unesite adresu",
                minlength: "Adresa mora imat minimalno 2 karaktera",
                maxlength: "Adresa ne moze biti duza od 200 karaktera"
            },
            brojziroracuna: {
                required: "Molimo vas unesite broj ziro racuna",
                digits: "Broj ziro racuna moze sadrzavat samo brojeve",
                minlength: "Broz ziro racuna mora minimalno sadrzavat 6 brojeva",
                maxlength: "Broj ziro racuna ne moze biti duzi od 20 brojeva"
            },
            opisposla: {
                required: "Molimo vas unesite opis posla",
                minlength: "Opis posla mora biti duzi od 2 karaktera",
                maxlength: "Opis posla ne moze biti duzi od 200 karaktera"
            },
            email: {
                required: "Molimo vas unesite email",
                emailExt: "Email koji ste unijeli nije validan",
                maxlength: "Email ne moze sadrzavat vise od 320 karaktera"
            }
        },

        submitHandler: function(form) {
            form.submit();
        }
    });
});