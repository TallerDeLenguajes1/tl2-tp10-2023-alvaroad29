// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// document.getElementById("estadoSelect").addEventListener("change", function () {
//     document.getElementById("cambiarEstadoForm").querySelector('button[type="submit"]').click();
// });



function enviarFormulario(formId) {
    document.getElementById(formId).submit();
}


const dataTableOptions = {
    language: {
        lengthMenu: "Mostrar _MENU_ registros por página",
        zeroRecords: "No se encontraron resultados",
        info: "Mostrando de _START_ a _END_ de un total de _TOTAL_ registros",
        infoEmpty: "Ningún dato disponible en esta tabla",
        infoFiltered: "(filtrados desde _MAX_ registros totales)",
        search: "Buscar:",
        loadingRecords: "Cargando...",
        paginate: {
            first: "Primero",
            last: "Último",
            next: "Siguiente",
            previous: "Anterior"
        }
    },
    columnDefs: [
        {orderable: false, target: [3]},
        { responsivePriority: 1, targets: 1},
        { responsivePriority: 2, targets: 3},
        { responsivePriority: 3, targets: 2},
        {
            className: 'dtr-control',
            orderable: false,
            targets: 0
        }
    ],
    order: [1, 'asc'],
    responsive: {
        details: {
            type: 'column',
            target: 'tr'
        }
    }
};

let dataTable;
let dataTableIsInitialized = false;
const initDataTable = async () => {
    if(dataTableIsInitialized)
    {
        dataTable.destroy();
    }

    dataTable = $("#myTable").DataTable(
        dataTableOptions
    );
    dataTableIsInitialized = true;
};


window.addEventListener("load", async () => {
    await initDataTable();
});

