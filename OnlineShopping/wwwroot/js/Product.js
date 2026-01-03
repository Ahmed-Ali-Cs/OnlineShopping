$(document).ready(function () {
    $('#DataTable').DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "isbn", "width": "15%" },
            { "data": "listPrice", "width": "10%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "10%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                                <div class="w-75 btn-group" role="group">
                                    <a href="/Admin/Product/Upsert/${data}" class="btn btn-primary text-white me-2">
                                        <i class="bi bi-pencil-square"></i> Edit
                                    </a>
                                    <a onclick=Delete("/Admin/Product/Delete/${data}") class="btn btn-danger text-white">
                                        <i class="bi bi-trash-fill"></i> Delete
                                    </a>
                                </div>
                            `;
                }, "width": "30%"
            }
        ]
    });
});
function Delete(url) {
    if (confirm('Are you sure you want to delete this product?')) {
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
    