document.addEventListener('DOMContentLoaded', function () {
    const mesAtual = new Date().toISOString().slice(0, 7);
    const filtroMes = document.getElementById('mesAnoInput');

    if (!filtroMes.value) {
        filtroMes.value = mesAtual;
    }

    filtrarVisitas();
});

document.getElementById('mesAnoInput').addEventListener('change', function (e) {
    const customMonthDisplay = document.getElementById('customMonthDisplay');

    if (!e.target.value) {
        customMonthDisplay.textContent = "Todos";
    } else {
        const [year, month] = e.target.value.split('-');
        const monthNames = [
            "Janeiro", "Fevereiro", "Março", "Abril",
            "Maio", "Junho", "Julho", "Agosto",
            "Setembro", "Outubro", "Novembro", "Dezembro"
        ];
        customMonthDisplay.textContent = `${monthNames[parseInt(month) - 1]} ${year}`;
    }

    filtrarVisitas();
});

document.getElementById('button_hoje').addEventListener('click', function (e) {
    const dataAtual = new Date();
    const mesAtual = (dataAtual.getMonth() + 1).toString().padStart(2, '0');
    const valorInput = `${dataAtual.getFullYear()}-${mesAtual}`;

    const monthInput = document.getElementById('mesAnoInput');
    monthInput.value = valorInput;
    monthInput.dispatchEvent(new Event('change'));
});

function filtrarVisitas() {
    const mes = document.getElementById('mesAnoInput').value;
    const containers = document.querySelectorAll('.mes-container');
    let visiveis = 0;

    containers.forEach(container => {
        const containerMes = container.dataset.mes;
        if (!mes || containerMes === mes) {
            container.style.display = 'block';
            visiveis++;
        } else {
            container.style.display = 'none';
        }
    });

    document.getElementById('mensagemSemResultados').style.display =
        visiveis > 0 ? 'none' : 'block';
}