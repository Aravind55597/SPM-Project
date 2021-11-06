function loadContent(contentUrl, contentType) {
    var contentHtml = ``;
    contentHtml += `<iframe src="${contentUrl}" width="600" height="480" allow="autoplay"></iframe>`;
    $("#view_course_material").html(contentHtml);

}

function view(courseClassId, chapterId, gradedQuizId) {
    var currentChapDTO = {};
    if (gradedQuizId != 0) {
        // remove accordion when in graded quiz
        $("#view_accordion").html(``);

        //display graded quiz question
        displayQuiz(gradedQuizId, 'graded');

    } else {

    

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
                                    <button class="btn btn-primary" type="submit" name="quizId" value="${quizId}" onclick="displayQuiz(${quizId}, 'ungraded')">Take Quiz</button>
                                </div>
                            </div>
                        </div>`;

                    contentHtml += ``;
                    $("#view_accordion").html(accordionHtml);

                    $("#view_course_material").html(contentHtml);



                }
            });
        }
    })
};


}

function displayQuiz(quizId, typeOfQuiz) {
    
    console.log(quizId)
    var retrieveQuiz = "/api/Quizzes/" + quizId;
    console.log(retrieveQuiz)
    $.ajax({
        url: retrieveQuiz, success: function (result) {
            if (typeOfQuiz == 'graded') {
                $("#current_chap_name").html(`<h1 class="align-items-center">Chapter ${result.name}</h1>`);
            }
            console.log(result)
            var numOfQuestions = result.questions.length
            var contentHtml = `<form id="quizForm">`;
            var tfAnswerHtml = ``;
            var multiSelectHtml = ``;
            var inputType = ``;
            var mcqAnswerHtml = ``;
            var selectType = ``;


            $.each(result.questions, function (index, item) {
                var questionNum = index + 1
                var questionName = item.question
                var questionType = item.questionType
                var isMultiSelect = item.isMultiSelect
                var questionId = item.id
                
                if (questionType == "TFQuestion") {
                    var tfAnswerHtml = `<div class="card-footer">
                                          <input type="radio" id="trueOption" name="tfAnswer" value="true" required>
                                          <label for="trueOption">True</label><br>
                                          <input type="radio" id="falseOption" name="tfAnswer" value="false" required>
                                          <label for="falseOption">False</label><br>
                                          <input type="hidden" name="tfAnswerQnId" value="${questionId}">
                                        </div>`;

                } else {
                    var option1 = item.option1
                    var option2 = item.option2
                    var option3 = item.option3
                    var option4 = item.option4
                    if (isMultiSelect) {
                        inputType = "checkbox";
                        selectType = "multiSelect"
                    } else {
                        inputType = "radio";
                        selectType = "singleSelect"
                    }

                    mcqAnswerHtml = `<div class="card-footer">
                                        <input type="${inputType}" id="option1" name="mcq${selectType}" value="${option1}">
                                        <label for="option1">${option1}</label><br>
                                        <input type="${inputType}" id="option2" name="mcq${selectType}" value="${option2}">
                                        <label for="option2">${option2}</label><br>
                                        <input type="${inputType}" id="option3" name="mcq${selectType}" value="${option3}">
                                        <label for="option3">${option3}</label><br>
                                        <input type="${inputType}" id="option4" name="mcq${selectType}" value="${option4}">
                                        <label for="option4">${option4}</label><br>
                                        <input type="hidden" name="mcq${selectType}QnId" value="${questionId}">
                                    </div>`;
                }

                contentHtml += `
                    <div class="row m-4">
                        <div class="col">
                            <div class="card" style="width: 620px; min-height: 200px;">
                              <div class="card-body">
                                
                                <h5 class="card-title">Question ${questionNum}</h5>
                                <p class="card-text">${questionName}</p>`;
                if (isMultiSelect) {
                    multiSelectHtml += `
                        <strong>This is a multi select question</strong>`;
                } else {
                    multiSelectHtml += ``;

                }
                contentHtml += multiSelectHtml

                contentHtml +=`</div>`;
                
                if (questionType == "TFQuestion") {
                    contentHtml += tfAnswerHtml
                } else {
                    contentHtml += mcqAnswerHtml
                }


                contentHtml += `
                              
                            </div>
                        </div>
                    </div>`;
                multiSelectHtml = ``;
            });

            contentHtml += `
                            <div class="row">
                                <div class="col-8"></div>
                                <div class="col">
                                    <button class="btn btn-primary" onclick="submitForm(${numOfQuestions}, ${quizId})" id="ajaxBtn">Submit</button>
                                </div>
                                <div class="col-3"></div>

                            </div>`;
            contentHtml += `</form>`;

            $("#view_course_material").html(contentHtml);

        }
    });
}



function submitForm(numOfQuestions, quizId) {
    event.preventDefault();
    //form validation
    const singleSelectRadio = document.querySelectorAll('input[name="mcqsingleSelect"]:checked');
    const multiSelectCheckBoxes = document.querySelectorAll('input[name="mcqmultiSelect"]:checked');
    const tfAnswer = document.querySelectorAll('input[name="tfAnswer"]:checked');

    if (singleSelectRadio.length == 0 || multiSelectCheckBoxes.length == 0 || tfAnswer.length == 0) {
        alert("All question must be answered")
        return;
    }


    var dataList = [];

    // retrieve single select answer
    var singleSelectQnId = document.querySelectorAll('input[name="mcqsingleSelectQnId"]')[0].value;

    let singleSelectList = [];
    let singleSelectObj = {};
    singleSelectRadio.forEach((checkbox) => {
        var strArr = checkbox.value.split(" ")
        var singleSelectAns = strArr[strArr.length - 1]
        singleSelectObj["questionId"] = singleSelectQnId
        singleSelectObj["answer"] = singleSelectAns
/*        singleSelectObj[singleSelectQnId] = singleSelectAns*/
    });
    dataList.push(singleSelectObj);

    console.log(dataList)

    // retrieve multi select answer
    var multiSelectQnId = document.querySelectorAll('input[name="mcqmultiSelectQnId"]')[0].value;

    let multiSelectList = [];
    let multiSelectObj = {};
    multiSelectCheckBoxes.forEach((checkbox) => {
        var strArr = checkbox.value.split(" ")
        var multiSelectAns = strArr[strArr.length - 1]
        if (multiSelectList.length != 0) {
            var currentStr = multiSelectObj["answer"]
            currentStr += "," + multiSelectAns
            multiSelectObj["answer"] = currentStr
        } else {
            multiSelectObj["questionId"] = multiSelectQnId
            multiSelectObj["answer"] = multiSelectAns
            multiSelectList.push(multiSelectObj);
        }
    });
    dataList.push(multiSelectList[0])
    console.log(dataList)

     //retrieve true or false answer
    var tfAnswerQnId = document.querySelectorAll('input[name="tfAnswerQnId"]')[0].value;
    let tfAnswerList = [];
    let trAnswerObj = {};
    tfAnswer.forEach((checkbox) => {
        if (checkbox.checked) {
            trAnswerObj["questionId"] = tfAnswerQnId
            trAnswerObj["answer"] = checkbox.value
            tfAnswerList.push(trAnswerObj);
        }
    });
    dataList.push(tfAnswerList[0])
    console.log(dataList)

    
    //Get request to retrieve user ans
    var attemptedQuiz = true
    var prevAttemptAnswer = {}
    $.ajax(`/api/UserAnswers?quizId=${quizId}`, {
        type: 'GET',  // http method
        // async need to be false in order to access variable outside of ajax function
        async: false,
        success: function (data, status, xhr) {
            console.log(data, status)
            // check if user has attempted the quiz
            if (data.length != 0) {
                console.log("User has attempted this quiz")
                attemptedQuiz = true
                prevAttemptAnswer = data

            } else {
                attemptedQuiz = false
            }
        },
        error: function (jqXhr, textStatus, errorMessage) {
            console.log(typeof (errorMessage), textStatus, jqXhr)
        }
    });
    console.log(prevAttemptAnswer)
    console.log(attemptedQuiz)

    // retrieve question answer
    $.ajax(`/api/Quizzes/${quizId}`, {
        type: 'GET',  // http method
        // async need to be false in order to access variable outside of ajax function
        async: false,
        success: function (data, status, xhr) {
            console.log(data, status)
            
        },
        error: function (jqXhr, textStatus, errorMessage) {
            console.log(typeof (errorMessage), textStatus, jqXhr)
        }
    });

    //for (let i = 0; i < prevAttemptAnswer.length; i++) {
    //    let prevAnsObj = prevAttemptAnswer[i]
    //    let prevQnId = prevAnsObj["questionId"]
    //    let prevAnsId = prevAnsObj["id"]
    //    for (let x = 0; x < dataList.length; x++) {
    //        let userAnsObj = dataList[i]
    //        let currQnId = userAnsObj["questionId"]
    //        if (prevQnId == currQnId) {
    //            userAnsObj["id"] = prevAnsId
    //        }

    //    }
    //}

    var httpMethod = ""
    if (attemptedQuiz) {
        httpMethod = "PUT"
    } else {
        httpMethod = "POST"
        

    }

    var dataJson = JSON.stringify(dataList);
    console.log(dataJson)
    //$.ajax('/api/UserAnswers', {
    //    type: httpMethod,  // http method
    //    contentType: "application/json",
    //    dataType: "json",
    //    data: dataJson,  // data to submit
    //    success: function (data, status, xhr) {
    //        console.log(data, status)
    //    },
    //    error: function (jqXhr, textStatus, errorMessage) {
    //        console.log(typeof (errorMessage), textStatus, jqXhr)
    //    }
    //});

}

