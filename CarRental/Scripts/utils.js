// on user/manager deletion
function onDelete(actionUrl, redirectUrl) {
    return function (e) {
        e.preventDefault();
        var id = e.currentTarget.id;

        console.log(e);

        $.post(actionUrl, { id: id },
            function () { window.location.replace(redirectUrl); })
        .fail(function () {
            console.log("failed");
        });

        return true;
    }
}

function onBlockUser(e) {    
    e.preventDefault();
    var id = e.currentTarget.id;

    console.log(e);

    $.post("/Admin/BlockUser", { id: id },
        function (data) {
            $(`#${id}`).text(data["IsBlocked"] ? "Unblock" : "Block");
        })
    .fail(function () {
        console.log("failed");
    });    
}

