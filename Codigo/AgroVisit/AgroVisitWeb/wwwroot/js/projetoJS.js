﻿window.items = __INITIAL_DATA__;
window.pageSize = 5;
window.currentPage = 1;
window.filteredItems = [...window.items];

window.renderCards = function () {
    const container = $('#containerCards');
    container.empty();

    const data = window.filteredItems.length > 0 ? window.filteredItems : window.items;

    if (data.length === 0) {
        container.append('<p>Nenhum proejto encontrado.</p>');
        return;
    }

    const start = (window.currentPage - 1) * window.pageSize;
    const end = window.pageSize === -1 ? data.length : start + window.pageSize;
    const paginatedItems = data.slice(start, end);

    paginatedItems.forEach(item => {
        container.append(`
            <div class="col-10 card radius-10 border-start border-0 border-5 border-success borda m-3 ms-0"
                style="box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25); height:min-content">
                <div class="card-body row">
                    <h3 class="card-title col-6 m-2"> ${item.Nome}</h3>
                    <h4 class="card-text col-4 m-2">Status: ${item.Status.startsWith("C") ? "Concluído" : "Em Execução"}</h4>
                    <h4 class="card-text col-6 m-2">Propriedade: ${item.NomePropriedade}</h4>
                    <h4 class="card-text col-6 m-2">Cliente: ${item.NomeCliente}</h4>
                    <h4 class="card-text col-6 m-2">Data de inicio: ${item.DataInicioFormatada}</h4>
                    <h4 class="card-text col-4 m-2">Valor: ${item.Valor}</h4>
                    <div class="row">
                        <a href="/Projeto/Details/${item.Id}" class="borda btn borda-concluir btn-outline-success col-3 m-2">Detalhes</a>
                        <a href="/Projeto/Delete/${item.Id}" class="borda btn border-danger btn-danger col-3 m-2">Excluir</a>
                    </div>
                </div>
            </div>
        `);
    });

    window.updatePaginationButtons();
}

window.renderCards();