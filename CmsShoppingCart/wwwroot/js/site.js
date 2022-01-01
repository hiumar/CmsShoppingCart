

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

function readUlr(input) {
    debugger
    if (input.files && input.files[0]) {
        let reader = new FileReader();
        reader.onload = function (e) {
            $("img#imagePreview").attr("src", e.target.result).width(100).height(80);
            

        }
        reader.readAsDataURL(input.files[0]);
    }

}