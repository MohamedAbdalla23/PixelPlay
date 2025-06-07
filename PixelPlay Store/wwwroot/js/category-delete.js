$(document).ready(function () {
    $('.js-cat-delete').on('click', function () {
        var btn = $(this);
        var id = btn.data('id');

        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger",
                cancelButton: "btn btn-dark mx-2 border-light"
            },
            buttonsStyling: false
        });

        swalWithBootstrapButtons.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Categories/Delete/${id}`,
                    method: 'DELETE',
                    success: function () {
                        swalWithBootstrapButtons.fire({
                            title: "Deleted!",
                            text: "Category has been deleted.",
                            icon: "success"
                        }).then(() => {
                            window.location.reload();
                        });
                    },
                    error: function () {
                        swalWithBootstrapButtons.fire({
                            title: "Oops!",
                            text: "Something went wrong.",
                            icon: "error"
                        });
                    }
                });
            }
        });
    });
});
