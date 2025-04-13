window.items = __INITIAL_DATA__;
window.pageSize = 5;
window.currentPage = 1;
window.filteredItems = [...window.items];

window.renderCards = function () {
    const container = $('#containerCards');
    container.empty();

    const data = window.filteredItems.length > 0 ? window.filteredItems : window.items;

    if (data.length === 0) {
        container.append('<p>Nenhuma visita encontrada.</p>');
        return;
    }

    const start = window.pageSize === -1 ? 0 : (window.currentPage - 1) * window.pageSize;
    const end = window.pageSize === -1 ? data.length : start + window.pageSize;
    const paginatedItems = data.slice(start, end);

    paginatedItems.forEach(item => {
        container.append(`
            <div class="col-10 card radius-10 border-start border-0 border-5 border-success borda  m-3 ms-0" 
                style="box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25); height:min-content">
                <div class="card-header borda">Visita: ${new Date(item.DataHora).toLocaleString()}</div >
                <div class="card-body row">
                    <h4 class="card-title">Propriedade: ${item.NomePropriedade}</h4>
                    <h4 class="card-text">Status: ${item.Status.startsWith("C") ? "Concluída" : "Agendada"}</p>
                    <h4 class="card-text">Objetivo: ${item.Observacoes || ""}</p>
                    <div class="row">
                        <a href="/Visita/Details/${item.Id}" class="borda btn borda-concluir btn-outline-success col-3 m-2">Detalhes</a>
                        <a href="/Visita/Delete/${item.Id}" class="borda btn border-danger btn-danger col-3 m-2">Excluir</a>
                    </div>                    
                </div>
            </div>
        `);
    });

    window.updatePaginationButtons();
};

window.renderCards();