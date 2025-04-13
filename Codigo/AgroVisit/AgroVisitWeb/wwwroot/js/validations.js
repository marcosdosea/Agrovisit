document.addEventListener('DOMContentLoaded', function () {
    const inputTel = document.getElementById('telefone');
    const inputCpf = document.getElementById('cpf');

    if (inputTel) {
        inputTel.addEventListener('input', function () {
            let valor = inputTel.value.replace(/\D/g, '');
            if (valor.length > 11) valor = valor.slice(0, 11);

            if (valor.length > 0) {
                valor = valor.replace(/^(\d{2})(\d)/, '($1) $2');
                if (valor.length > 9) {
                    valor = valor.replace(/(\d{5})(\d{4})$/, '$1-$2');
                }
            }

            inputTel.value = valor;
        });
    }

    if (inputCpf) {
        inputCpf.addEventListener('input', function () {
            let valor = inputCpf.value.replace(/\D/g, '');
            if (valor.length > 11) valor = valor.slice(0, 11);

            if (valor.length > 0) {
                valor = valor.replace(/(\d{3})(\d)/, '$1.$2');
                if (valor.length > 7) {
                    valor = valor.replace(/(\d{3})\.(\d{3})(\d)/, '$1.$2.$3');
                    valor = valor.replace(/(\d{3})\.(\d{3})\.(\d{3})(\d{2})$/, '$1.$2.$3-$4');
                }
            }

            inputCpf.value = valor;
        });
    }
});
