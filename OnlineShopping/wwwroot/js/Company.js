$(document).ready(function () {
    $('#DataTable1').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "address", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "region", "width": "10%" },
            { "data": "postalCode", "width": "10%" },
            { "data": "phoneNumber", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                                <div class="w-75 btn-group" role="group">
                                    <a href="/Admin/Company/Upsert/${data}" class="btn btn-primary text-white me-2">
                                        <i class="bi bi-pencil-square"></i> Edit
                                    </a>
                                    <a onclick=Delete("/Admin/Company/Delete/${data}") class="btn btn-danger text-white">
                                        <i class="bi bi-trash-fill"></i> Delete
                                    </a>
                                </div>
                            `;
                }, "width": "25%"
            }
        ]
    });
});
function Delete(url) {
    if (confirm('Are you sure you want to delete this company?')) {
        $.ajax({
            type: 'DELETE',
            url: url,
            success: function (data) {
                if (data.success) {
                    toastr.success(data.message);
                    $('#DataTable').DataTable().ajax.reload();
                }
                else {
                    toastr.error(data.message);
                }
            }
        });
    }
}
    