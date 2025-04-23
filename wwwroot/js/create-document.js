document.addEventListener('DOMContentLoaded', function () {
    const selectTipo = document.getElementById('TipoDocumentoId');
    const inputNovoTipo = document.getElementById('NovoTipo');

    if (!selectTipo || !inputNovoTipo) return;

    function atualizarEstadoCampos() {
        if (selectTipo.value && selectTipo.value !== "") {
            inputNovoTipo.disabled = true;
            inputNovoTipo.value = '';
        } else {
            inputNovoTipo.disabled = false;
        }

        if (inputNovoTipo.value.trim().length > 0) {
            selectTipo.disabled = true;
            selectTipo.value = "";
        } else {
            selectTipo.disabled = false;
        }
    }

    atualizarEstadoCampos();

    selectTipo.addEventListener('change', atualizarEstadoCampos);
    inputNovoTipo.addEventListener('input', atualizarEstadoCampos);
});