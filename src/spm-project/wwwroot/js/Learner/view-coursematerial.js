function view(chapterId) {
    var retrieveResources = $("#get-resources").val() + "?chapterId=" + chapterId;
    $.ajax({
        url: retrieveResources, success: function (result) {
            console.log(result)
            var contentHtml = ``;
            var accordionHtml = `<div class="accordion" id="accordionExample">`;
            var contentHtml = ``;
            var checkFirstLoad = true;
            $.each(result.data, function (index, item) {
                index = index + 1
                var indexStr = index.toString()
                var contentType = item.content
                var contentUrl = item.contentUrl
                var downloadableContentUrl = item.downloadableContentUrl
                if (checkFirstLoad) {
                    contentHtml += `<iframe src="${contentUrl}" width="600" height="480" allow="autoplay"></iframe>`;
                    var accordionHeader = "show"
                    var accordionButton = ""
                } else {
                    var accordionHeader = ""
                    var accordionButton = "collapsed"
                }
                
                accordionHtml += `
                <div class="accordion-item">
                    <h2 class="accordion-header" id="heading${indexStr}">
                        <button class="accordion-button ${accordionButton}" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${indexStr}" aria-expanded="true" aria-controls="collapse${indexStr}">
                            ${indexStr}) ${contentType}
                        </button>
                    </h2>
                    <div id="collapse${indexStr}" class="accordion-collapse collapse ${accordionHeader}" aria-labelledby="heading${indexStr}" data-bs-parent="#accordionExample">
                        <div class="accordion-body">
                            <a onclick="loadContent('${contentUrl}', '${contentType}')">View content</a>
                        </div>
                        <div class="accordion-body">
                            <a href="${downloadableContentUrl}" download>Download Content</a>

                        </div>
                    </div>
                </div>`;
                checkFirstLoad = false
            });
            accordionHtml += `</div>`;
            contentHtml += ``;
            $("#view_accordion").html(accordionHtml);

            $("#view_course_material").html(contentHtml);



        }
    });
}

function loadContent(contentUrl, contentType) {
    var contentHtml = ``;
    contentHtml += `<iframe src="${contentUrl}" width="600" height="480" allow="autoplay"></iframe>`;
    $("#view_course_material").html(contentHtml);

}
