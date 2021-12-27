$(function () {
    if ($('a.ConfirmDelete').length) {
        $('a.ConfirmDelete').click(() => {
            if (!confirm("Confirm delete")) return false

        });

    }


    if ($('div.alert.notification').length) {
        setTimeout(() => {
            $('div.alert.notification').fadeOut()
        }, 2000)

    }


});