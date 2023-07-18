$("#blogs").on("click", ".blog", function () {
    $("#blogs .blog").removeClass("text-primary");
    $(this).addClass("text-primary");
    $(this).removeClass("text-dark");
    $("#blog-frame").attr("src", $(this).data("src"));
});

$(() => {
    var base64 = $("#blogs").data("current");
    if (base64 != "") {
        $("#blogs .blog").each((index,ele) => {
            if ($(ele).data("base64") == base64) {
                $(ele).click();
            }
        });
    }
    else {
        $("#blogs .blog").first().click();
    }

    var route = $("#blog-category").data("route");
    $("#blog-category a").removeClass("text-primary").addClass("text-dark");;
    $("#blog-category a").each((i, ele) => {
        if ($(ele).data("category") == route) {
            $(ele).addClass("text-primary");
            $(ele).removeClass("text-dark");
        }
    });
});