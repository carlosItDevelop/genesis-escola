﻿$(document).ready(function () {
    $('#Tabela').DataTable({
        lengthChange: !1,
        responsive: true,
        buttons: ["copy", "print"],
        "language": {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando de _START_ at&eacute _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 at&eacute 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ resultados por p&aacutegina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Pr&oacuteximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "&Uacuteltimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        },
        "order": [[1, "desc"]]
    });
    $('#Tabela td').css('white-space', 'initial');
});