function loadContent(contentUrl, contentType) {
    var contentHtml = ``;
    contentHtml += `<iframe src="${contentUrl}" width="600" height="480" allow="autoplay"></iframe>`;
    $("#view_course_material").html(contentHtml);

}

function view(courseClassId, chapterId) {
    var currentChapDTO = {};

    // retrieve chapter list based on the courseClassId
    var retrieveChapters = $("#get-chapters").val() + "?courseClassId=" + courseClassId;
    $.ajax({
        url: retrieveChapters, success: function (result) {
            // sort chapters in ascending order
            result.data.sort((a, b) => (a.name > b.name ? 1 : -1))

            var prevChap = 0;
            var nextChap = 0;
            var prevChapHtml = "";
            var nextChapHtml = "";
            var buttonState = "";
            var buttonType = "";

            // retrieve previous and next chapter id according to the current chapter id user is in
            $.each(result.data, function (index, item) {
                if (item.id == chapterId) {
                    currentChapDTO = result.data[index]
                    console.log(currentChapDTO)
                    var chapName = index + 1
                    $('#current_chap_name').html(`<h1 class="align-items-center">Chapter ${chapName.toString()}</h1>`)
                    if (index != 0) {
                        prevChap = result.data[index - 1]
                    } 

                    if (index < result.data.length - 1) {
                        nextChap = result.data[index + 1]
                    } 
                    console.log(prevChap)
                    console.log(nextChap)
                }
            });

            // disable previous chapter button if there is current chapter is the first chapter of the class
            if (prevChap == 0) {
                buttonState = "disabled";
                buttonType = "btn-secondary";
            }

            if (prevChap != 0) {
                buttonState = "";
                buttonType = "btn-primary";
            }

            // insert previous chapter button html into html page
            prevChapHtml += `
                    <form action="/Learner/ViewCourseMaterial" method="get">
                        <button type="submit" name="chapterId" value="${prevChap.id}" class="btn-sign-up btn ${buttonType} ${buttonState}">
                            Previous Chapter
                        </button>
                        <input type="hidden" name="courseClassId" value="${courseClassId}">
                    </form>`;
            $("#prev_chap_btn").html(prevChapHtml);

            // disable previous chapter button if there is current chapter is the last chapter of the class
            if (nextChap == 0) {
                buttonState = "disabled";
                buttonType = "btn-secondary";
            }

            if (nextChap != 0) {
                buttonState = "";
                buttonType = "btn-primary";

            }

            // insert next chapter button html into html page
            nextChapHtml += `
                    <form action="/Learner/ViewCourseMaterial" method="get">
                        <button type="submit" name="chapterId" value="${nextChap.id}" class="btn-sign-up btn ${buttonType} ${buttonState}">
                            Next Chapter
                        </button>
                        <input type="hidden" name="courseClassId" value="${courseClassId}">
                    </form>`;
            $("#next_chap_btn").html(nextChapHtml);



            // next ajax call 
            // retrieve resources based on chapterId
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
                            // display course material
                            contentHtml += `<iframe src="${contentUrl}" width="600" height="480" allow="autoplay"></iframe>`;
                            var accordionHeader = "show"
                            var accordionButton = ""
                        } else {
                            var accordionHeader = ""
                            var accordionButton = "collapsed"
                        }

                        // display accordion
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

                    // add quiz into accordion
                    var quizNum = result.data.length + 1;
                    var quizId = currentChapDTO.quizIds[0]
                    console.log(quizId)

                    accordionHtml += `
                        <div class="accordion-item">
                            <h2 class="accordion-header" id="heading${quizNum}">
                                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#collapse${quizNum}" aria-expanded="true" aria-controls="collapse${quizNum}">
                                    ${quizNum}) Ungraded Quiz
                                </button>
                            </h2>
                            <div id="collapse${quizNum}" class="accordion-collapse collapse " aria-labelledby="heading${quizNum}" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <button class="btn btn-primary" type="submit" name="quizId" value="${quizId}" onclick="displayQuiz(${quizId})">Take Quiz</button>
                                </div>
                            </div>
                        </div>`;

                    contentHtml += ``;
                    $("#view_accordion").html(accordionHtml);

                    $("#view_course_material").html(contentHtml);



                }
            });
        }
    });

    

    // retrieve quiz resources based on chapterId
    //var retrieveResources = $("#get-resources").val() + "?chapterId=" + chapterId;
    //$.ajax({
    //    url: retrieveResources, success: function (result) {
    //        console.log(result)
    //        var contentHtml = ``;
    //        var accordionHtml = ``;
            
    //        $.each(result.data, function (index, item) {
                
    //        });

            
            


    //    }
    //});
}

function displayQuiz(quizId) {
    console.log(quizId)
    //var retrieveResources = $("#get-resources").val() + "?chapterId=" + chapterId;
    //$.ajax({
    //    url: retrieveResources, success: function (result) {
    //        console.log(result)
    //        var contentHtml = ``;

    //        $.each(result.data, function (index, item) {

    //        });



    //        contentHtml += ``;

    //        $("#view_course_material").html(contentHtml);

    //    }
    //});
}


