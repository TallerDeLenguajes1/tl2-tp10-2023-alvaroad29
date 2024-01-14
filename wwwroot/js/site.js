﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
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
        {orderable: false, target: [5,6]},
        { responsivePriority: 1, targets: 1},
        { responsivePriority: 2, targets: 6},
        { responsivePriority: 3, targets: 5},
        { responsivePriority: 4, targets: 4},
        { responsivePriority: 5, targets: 2},
        { responsivePriority: 6, targets: 3},
        {
            className: 'dtr-control',
            orderable: false,
            targets: 0
        },
        // {
        //     targets: 2,
        //     // data: "Descripcion",
        //     render: function (data, type) {
        //         console.log("Data:", data, "Type:", type);
        //         if (type === 'display') {
        //             if (data.length > 10) {
        //                return '<span title="'+data+'">'+data.substr( 0, 5 )+'...</span>';
        //             }
        //             else {
        //                 return data;
        //             }
        //         }
        //     }
        // }
        
        // {classNmae: "centered", target: [2,3,4,5,6]}
    ],
    order: [1, 'asc'],
    responsive: {
        details: {
            type: 'column',
            target: 'tr',
            // display: $.fn.dataTable.Responsive.display.modal({
            //     header: function (row) {
            //         var data = row.data();
            //         return 'Tarea: ' + data[1];
            //     }
            // }),
            // renderer: $.fn.dataTable.Responsive.renderer.tableAll()
        }
     
    },
    order: [[9, 'asc']],
    rowGroup: {
        dataSrc: 9
    }

    // responsive: true,
    // paging: false,
    // scrollCollapse: true,
    // scrollY: '50vh',
    
    // autoFill: true
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


// $(document).ready( function () {
//     $('#myTable').DataTable(
//         dataTableOptions
//     );
// } );

// let table = new DataTable('#myTable', {
//     responsive: true
// });
