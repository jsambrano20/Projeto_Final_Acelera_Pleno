jQuery(document).ready(function ($) {
    $('#cpf-mask').mask('000.000.000-00', { reverse: true });

    $('#cpf-mask').on('blur', function () {
        if (!validaCPF($(this).cleanVal())) {
            alert('CPF inválido');
            $(this).val('');
        }
    });



    $('#enviar').click(function () {
        var cpf = $('#cpf-mask').cleanVal();
        if (!validaCPF(cpf)) {
            alert('CPF inválido');
            return false; // impede o envio do formulário
        } else {
            var cpf1 = $('#cpf-mask').val();
            cpf1 = cpf1.replace(/[^\d]+/g, '');
            $('#cpf-mask').val(cpf1);
        }
    });

    $('#btnSalvar').click(function () {
        var cpf = $('#cpf-mask').cleanVal();
        if (!validaCPF(cpf)) {
            alert('CPF inválido');
            return false; // impede o envio do formulário
        } else {
            var cpf1 = $('#cpf-mask').val();
            cpf1 = cpf1.replace(/[^\d]+/g, '');
            $('#cpf-mask').val(cpf1);
        }
    });


    function validaCPF(cpf) {
        cpf = cpf.replace(/[^\d]+/g, '');

        if (cpf == '') return false;

        // Elimina CPFs invalidos conhecidos
        if (cpf.length != 11 ||
            cpf == "00000000000" ||
            cpf == "11111111111" ||
            cpf == "22222222222" ||
            cpf == "33333333333" ||
            cpf == "44444444444" ||
            cpf == "55555555555" ||
            cpf == "66666666666" ||
            cpf == "77777777777" ||
            cpf == "88888888888" ||
            cpf == "99999999999")
            return false;

        // Valida 1o digito
        add = 0;
        for (i = 0; i < 9; i++)
            add += parseInt(cpf.charAt(i)) * (10 - i);
        rev = 11 - (add % 11);
        if (rev == 10 || rev == 11)
            rev = 0;
        if (rev != parseInt(cpf.charAt(9)))
            return false;

        // Valida 2o digito
        add = 0;
        for (i = 0; i < 10; i++)
            add += parseInt(cpf.charAt(i)) * (11 - i);
        rev = 11 - (add % 11);
        if (rev == 10 || rev == 11)
            rev = 0;
        if (rev != parseInt(cpf.charAt(10)))
            return false;

        return true;
    }
});
