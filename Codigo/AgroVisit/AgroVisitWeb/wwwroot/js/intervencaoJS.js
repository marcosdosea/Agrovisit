document.addEventListener('DOMContentLoaded', function () {
    $(document).on('submit', '#createIntervencaoForm', function (e) {
        e.preventDefault();
        var form = $(this);

        if (typeof form.valid === "function" && !form.valid()) return;

        $.ajax({
            url: form.attr("action"),
            type: "POST",
            data: form.serialize(),
            success: function (response) {
                if (response.success) {
                    var modal = bootstrap.Modal.getInstance(document.getElementById('createIntervencaoModal'));
                    modal.hide();

                    setTimeout(function () {
                        location.reload();
                    }, 500);
                } else {
                    $(".modal-body").html(response);
                }
            },
            error: function (xhr) {
                alert("Erro ao salvar: " + xhr.statusText);
            }
        });
    });

    $('#createIntervencaoModal').on('hidden.bs.modal', function () {
        $(this).find('form')[0].reset();
        $(this).find('.text-danger').text('');
    });

    $(document).on('submit', '#editIntervencaoForm', function (e) {
        e.preventDefault();
        var form = $(this);

        $.ajax({
            url: form.attr('action'),
            type: 'POST',
            data: form.serialize(),
            success: function (response) {
                if (response.success) {
                    var modal = bootstrap.Modal.getInstance(document.getElementById('editIntervencaoModal'));
                    modal.hide();

                    setTimeout(function () {
                        location.reload();
                    }, 500);
                } else {
                    $('.modal-body').html(response);
                }
            },
            error: function (xhr) {
                alert('Erro ao salvar: ' + xhr.statusText);
            }
        });
    });

    $('#editIntervencaoModal').on('hidden.bs.modal', function () {
        $(this).find('form')[0].reset();
        $(this).find('.text-danger').text('');
    });

    $(document).on('click', '#abrirIntervencaoModalCreate', function () {
        const projetoId = $(this).data('projeto-id');
        const url = $(this).data('url-create');

        $.get(url, { idProjeto: projetoId }, function (data) {
            $('#createIntervencaoModal .modal-body').html(data);
            new bootstrap.Modal('#createIntervencaoModal').show();
        });
    });

    $(document).on('click', '[data-url-edit]', function () {
        const idIntervencao = $(this).data('intervencao-id');
        const url = $(this).data('url-edit');

        $.get(url, { id: idIntervencao }, function (data) {
            $('#editIntervencaoModal .modal-body').html(data);
            new bootstrap.Modal('#editIntervencaoModal').show();
        }).fail(function (xhr) {
            alert("Erro: " + xhr.statusText);
        });
    });
});