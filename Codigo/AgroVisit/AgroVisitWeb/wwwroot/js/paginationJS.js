window.updatePaginationButtons = function () {
    const buttonNext = document.getElementById('nextPage');
    const buttonPrev = document.getElementById('prevPage');
    const totalItems = window.filteredItems.length;

    $(buttonPrev).prop('disabled', window.currentPage === 1);
    if (window.pageSize !== -1) {
        $(buttonNext).prop('disabled', window.currentPage * window.pageSize >= totalItems);
    } else {
        $(buttonNext).prop('disabled', true);
    }
    
}

function applyFilters() {
    const searchText = $('#searchInput').val().toLowerCase();
    window.filteredItems = window.items.filter(item =>
        item.Nome.toLowerCase().includes(searchText));

    window.currentPage = 1;
    window.renderCards();
    window.updatePaginationButtons();
}

$('#searchInput').on('input', applyFilters);

$('#pageSize').on('change', function () {
    window.pageSize = parseInt($(this).val());
    window.currentPage = 1;
    window.renderCards();
    window.updatePaginationButtons();
});

$('#prevPage').on('click', function () {
    if (window.currentPage > 1)
    {
        window.currentPage--;
        window.renderCards();
    }
    window.updatePaginationButtons();
});

$('#nextPage').on('click', function () {
    const totalItems = window.filteredItems.length;
    if (window.pageSize !== -1 || window.currentPage * window.pageSize < totalItems)
    {
        window.currentPage++;
        window.renderCards();
    }
    window.updatePaginationButtons();
});