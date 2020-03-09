// Wait for the DOM to be ready
$(function() {
    // Initialize form validation on the registration form.
    // It has the name attribute "registration"
    $("form[name='formapregledprofila']").validate({
        // Specify validation rules
        rules: {
            // The key name on the left side is the name attribute
            // of an input field. Validation rules are defined
            // on the right side
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
            mobitel: {
                required: true,
                digits: true,
                minlength: 9,
                maxlength: 30
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
            }
        },
        // Specify validation error messages
        messages: {
            ime: {
                required: "Molimo vas unesite vase ime",
                minlength: "Ime mora sadrzavat najmanje 2 slova",
                maxlength: "Ime ne smije sadrzavat preko 100 slova"
            },
            prezime: {
                required: "Molimo vas unesite vase prezime",
                minlength: "Prezime mora sadrzavat najmanje 3 slova",
                maxlength: "Prezime ne smije sadrzavat preko 100 slova"
            },
            mobitel: {
                required: "Molimo vas unesite vas broj telefona",
                digits: "Broj telefona mora sadrzavat samo brojeve, primjer: 061222333",
                minlength: "Broj telefona mora imat minimalno 9 brojeva",
                maxlength: "Broj telefona ne moze biti duzi od 30"
            },
            adresa: {
                required: "Molimo vas unesite vasu adresu",
                minlength: "Adresa mora imat minimalno 2 karaktera",
                maxlength: "Adresa ne moze biti duza od 200 karaktera"
            },
            brojziroracuna: {
                required: "Molimo vas unesite svoj broj ziro racuna",
                digits: "Broj ziro racuna moze sadrzavat samo brojeve",
                minlength: "Broz ziro racuna mora minimalno sadrzavat 6 brojeva",
                maxlength: "Broj ziro racuna ne moze biti duzi od 20 brojeva"
            },
            opisposla: {
                required: "Molimo vas unesite opis vaseg posla",
                minlength: "Opis posla mora biti duzi od 2 karaktera",
                maxlength: "Opis posla ne moze biti duzi od 200 karaktera"
            }
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function(form) {
            form.submit();
        }
    });
});