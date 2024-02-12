$(document).ready(function () {
    $('.adm-row').on("dblclick", function (value) {
        let admissionId = value.currentTarget.id;
        $.ajax({
            url: '/MedicalReports/Partial',
            type: 'GET',
            data: { admissionId: admissionId },
            success: function (data) {
                // Handle the response data here
                $('#medRecPartialView').html(data);
            },
            error: function () {
                alert('Error occurred while fetching data.');
            }
        });
    });
});

$(document).ready(function () {
    $('.table tbody tr').on("dblclick", function () {
        $('.table tbody tr').removeClass('selected');
        $(this).addClass('selected');
    });
});
