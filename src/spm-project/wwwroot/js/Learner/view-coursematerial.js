function view(chapterId) {
    var retrieveResources = $("#get-resources").val() + "?chapterId=" + chapterId;
    $.ajax({
        url: retrieveResources, success: function (result) {
            console.log(result)

            var accordionHtml = `<div class="accordion" id="accordionExample">`;
            var contentHtml = ``;
            $.each(result.data, function (index, item) {
                index = index + 1
                var indexStr = index.toString()
                var contentType = item.content
                var contentUrl = item.contentUrl
                accordionHtml += `
                <div class="accordion-item">
                    <h2 class="accordion-header" id="heading${indexStr}">
                        <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${indexStr}" aria-expanded="true" aria-controls="collapse${indexStr}">
                            ${indexStr}) ${contentType}
                        </button>
                    </h2>
                    <div id="collapse${indexStr}" class="accordion-collapse collapse" aria-labelledby="heading${indexStr}" data-bs-parent="#accordionExample">
                        <div class="accordion-body">
                            <a onclick="loadContent('${contentUrl}', '${contentType}')">View content</a>
                        </div>
                    </div>
                </div>`;
            });
            accordionHtml += `</div>`;
            contentHtml += ``;
            $("#view_course_material").html(contentHtml);
            $("#view_accordion").html(accordionHtml);




        }
    });
}

function loadContent(contentUrl, contentType) {
    console.log(contentUrl)
    console.log(contentType)

    var contentHtml = ``;
    if (contentType == "Video") {
        contentHtml += `<iframe src="${contentUrl}" width="600" height="480" allow="autoplay"></iframe>`;
    } else if (contentType == "Word") {
        contentHtml += `<iframe src="${contentUrl}" width="600" height="480" allow="autoplay"></iframe>`;
    } else if (contentType == "PowerPoint") {
        contentHtml += `<iframe src="${contentUrl}" width="600" height="480" allow="autoplay"></iframe>`;
    } else if (contentType == "Excel") {
        contentHtml += `<iframe src="${contentUrl}" width="600" height="480" allow="autoplay"></iframe>`;
    } else if (contentType == "PDF") {
        contentHtml += `<iframe src="${contentUrl}" width="600" height="480" allow="autoplay"></iframe>`;
    }
    $("#view_course_material").html(contentHtml);

}
