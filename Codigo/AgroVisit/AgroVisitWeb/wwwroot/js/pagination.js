$(document).ready(function () {
    const items = @Html.Raw(JsonSerializer.Serialize(Model)); // Obtém os dados do Model
    let filteredItems = items;
    let pageSize = 5;
    let currentPage = 1;

    function renderCards() {
        const container = $('#containerCards');
        container.empty();

        if (filteredItems.length === 0) {
            container.append('<p>Nenhum cliente encontrado.</p>');
            return;
        }

        const start = (currentPage - 1) * pageSize;
        const end = pageSize === -1 ? filteredItems.length : start + pageSize;
        const paginatedItems = filteredItems.slice(start, end);

        paginatedItems.forEach(item => {
            container.append(`
                                <div id="border" class="col-10 card radius-10 border-start border-0 border-5 border-success m-3 ms-0"
                                        style="box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25); height:min-content">
                                    <div class="card-body row">
                                        <h4 class="card-title col-6 m-2">Cliente: ${item.Nome}</h4>
                                        <h4 class="card-text col-4 m-2">CPF: ${item.Cpf}</h4>
                                        <h4 class="card-text col-6 m-2">Telefone: ${item.Telefone || "Sem telefone"}</h4>
                                        <h4 class="card-text col-4 m-2">Cidade: ${item.Cidade}</h4>
                                        <div class="row">
                                            <a href="/Cliente/Details/${item.Id}" id="border" class="btn borda-concluir radius-10 btn-outline-success col-3 m-2">Detalhes</a>
                                            <a href="/Cliente/Delete/${item.Id}" id="border" class="btn border-danger radius-10 btn-danger col-3 m-2">Excluir</a>
                                        </div>
                                    </div>
                                </div>
                            `);
        });

        updatePaginationButtons();
    }

    // Aplica filtros de pesquisa
    function applyFilters() {
        const searchText = $('#searchInput').val().toLowerCase();
        filteredItems = items.filter(item =>
            item.Nome.toLowerCase().includes(searchText)
        );
        currentPage = 1;
        renderCards();
        updatePaginationButtons();
    }

    // Atualiza páginação
    function updatePaginationButtons() {
        var buttonNext = document.getElementById('nextPage');
        var buttonPrev = document.getElementById('prevPage');

        $(buttonPrev).prop('disabled', currentPage === 1);
        $(buttonNext).prop('disabled', currentPage * pageSize >= filteredItems.length && pageSize !== -1);
    };

    $('#searchInput').on('input', applyFilters);

    $('#pageSize').on('change', function () {
        pageSize = parseInt($(this).val());
        currentPage = 1;

        renderCards();
        updatePaginationButtons();
    });

    // Atualiza página ao clicar no botão Anterior
    $('#prevPage').on('click', function () {
        if (currentPage > 1) {
            currentPage--;
            renderCards();
        }

        updatePaginationButtons();
    });

    // Atualiza página ao clicar no botão Próximo
    $('#nextPage').on('click', function () {
        if (currentPage * pageSize < filteredItems.length || pageSize === -1) {
            currentPage++;
            renderCards();
        }

        updatePaginationButtons();
    });

    // Renderizar pela primeira vez
    renderCards();
});