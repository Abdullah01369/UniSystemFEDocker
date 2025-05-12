$(document).ready(function () {
    $('.popup-with-zoom-anim').magnificPopup({
        type: 'inline',
        fixedContentPos: false,
        fixedBgPos: true,
        overflowY: 'auto',
        closeBtnInside: true,
        preloader: false,
        midClick: true,
        removalDelay: 300,
        mainClass: 'my-mfp-zoom-in'
    });
});

function StudentLogin() {
    var loginDto = {
        email: $('#StudentMail').val(),
        password: $('#StudentPassw').val()
    };
    $('#loader').show();

    $.ajax({
        url: '/Home/LoginStudent',  
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(loginDto),
        success: function (response) {
            $('#loader').hide();
            $('#StudentBtn').prop('disabled', true);

            if (response.success) {
                 localStorage.setItem('authToken', response.token);
                 window.location.href = '/Student/Home/Index';
            } else {
                var errors = response.errors;
                var errorMessages = "";
                for (var i = 0; i < errors.length; i++) {
                    errorMessages += errors[i] + "\n";
                }

                
                $('#StudentMail').val("");
                $('#StudentPassw').val("");
                $('#StudentBtn').prop('disabled', false);

                alert("\n" + errorMessages);
            }
        },
        error: function (xhr, status, error) {
            $('#loader').hide();
            $('#StudentBtn').prop('disabled', false);
            alert("HATA");
   
        }
    });
}





