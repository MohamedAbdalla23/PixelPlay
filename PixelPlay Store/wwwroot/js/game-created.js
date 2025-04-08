$(document).ready(function () {
    $('.js-create').on('submit', function (e) {
        e.preventDefault();

        var form = $(this);
        var formData = new FormData(this); // for file upload support

        $.ajax({
            url: '/Games/Create', // POST to your Create action
            method: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.success) {
                    // Success - Show SweetAlert
                    Swal.fire({
                        title: "Success!",
                        text: response.message,
                        icon: "success",
                        confirmButtonText: "OK"
                    }).then((result) => {
                        if (result.isConfirmed) {
                            // Redirect after OK
                            window.location.href = '/Games';
                        }
                    });
                } else {
                    // Error handling (form is invalid)
                    Swal.fire({
                        title: "Error!",
                        text: response.message,
                        icon: "error",
                        confirmButtonText: "Try Again"
                    });
                }
            },
            error: function () {
                Swal.fire({
                    title: "Oops!",
                    text: "Something went wrong. Please try again.",
                    icon: "error",
                    confirmButtonText: "Try Again"
                });
            }
        });
    });
});
