function get_chapter(courseClassId) {
    var retrieveChapters = $("#get-chapters").val() + "?courseClassId=" + courseClassId;

    $.ajax({
        url: retrieveChapters, success: function (result) {
            console.log(result)

            var dataHtml = ``;
            $.each(result.data, function (index, item) {
                var name = item.name
                var index = name.indexOf("Chapter");
                var chapterName = name.slice(index, name.length)
                var chapterDescription = item.description
                dataHtml += '<div class="row align-items-center"><div class="col">' + `<div class="card mb-3" style="max-width: 540px; background-color: fefaf4;">
                            <div class="row g-0">
                                <div class="col">
                                    <div class="card-body">
                                        <h5 class="card-title">${chapterName}</h5>

                                        <p class="card-text">${chapterDescription}</p>


                                        <p class="card-text"><small class="text-muted"></small></p>
                                        <div class="row">
                                            <div class="col"></div>
                                            <div class="col-6">
                                                <button type="button" onclick=""; class="btn-sign-up btn btn-primary w-100">
                                                  View Course Material
                                                </button>
                                            </div>
                                            <div class="col"></div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>` + '</div></div>';;
            });
            dataHtml += ``;
            $("#view_classes").html(dataHtml);

        }
    });
}