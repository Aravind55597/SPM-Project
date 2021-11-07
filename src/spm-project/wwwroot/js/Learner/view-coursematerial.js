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
                    var chapName = index + 1
                    $('#current_chap_name').html(`<h1 class="align-items-center">Chapter ${chapName.toString()}</h1>`)
                    if (index != 0) {
                        prevChap = result.data[index - 1]
                    }

                    if (index < result.data.length - 1) {
                        nextChap = result.data[index + 1]
                    }
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
function startTimer() {
    var presentTime = document.getElementById('countdownTime-text').innerHTML;
    var timeArray = presentTime.split(/[:]+/);
    var m = timeArray[0];
    var s = checkSecond((timeArray[1] - 1));
    if (s == 59) { m = m - 1 }
    if ((m + '').length == 1) {
        m = '0' + m;
    }
    if (m < 0) {
        //action to do when countdown time hit 0
        $("#countdownTime-body").attr("class", "card-body text-center")
        $("#countdownTime-timeup").html(`Time is up! Please submit the quiz now!`)
        return
    }
    document.getElementById('countdownTime-text').innerHTML = m + ":" + s;
    setTimeout(startTimer, 1000);
}

function checkSecond(sec) {
    if (sec < 10 && sec >= 0) { sec = "0" + sec }; // add zero in front of numbers < 10
    if (sec < 0) {
        sec = "59"
    };
    return sec;
}

function displayQuiz(quizId, typeOfQuiz) {
    var retrieveQuiz = "/api/Quizzes/" + quizId;
    console.log(retrieveQuiz)
    $.ajax({
        url: retrieveQuiz, success: function (result) {
            if (typeOfQuiz == 'graded') {
                $("#current_chap_name").html(`<h1 class="align-items-center">Chapter ${result.name}</h1>`);
            }
            console.log(result)
            var numOfQuestions = result.questions.length
            var contentHtml = ``;
            var tfAnswerHtml = ``;
            var multiSelectHtml = ``;
            var inputType = ``;
            var mcqAnswerHtml = ``;
            var selectType = ``;
            var timeLimit = result.timeLimit
            //display quiz countdown timer
            contentHtml += `<div class="row m-4">
                                <div class="col">
                                    <div id="countdownTime-card" class="">
                                        <div class="card" style="width: 620px; min-height: 50px;">
                                            <div class="card-body text-center">
                                                <strong id="countdownTime-text">${timeLimit}:00</strong>
                                            </div>
                                            <div id="countdownTime-body" class="card-body text-center d-none">
                                                <strong id="countdownTime-timeup" class="text-danger"></strong>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>`;

            //display user quiz score
            contentHtml += `<div class="row m-4">
                                <div class="col">
                                    <div id="displayUserMarks-card" class="d-none">
                                        <div class="card" style="width: 620px; min-height: 50px;">
                                            <div class="card-body text-center">
                                                <strong id="displayUserMarks-text">Marks</strong>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>`;

            contentHtml += `<form id="quizForm">`;
            $.each(result.questions, function (index, item) {
                var questionNum = index + 1
                var questionName = item.question
                var questionType = item.questionType
                var isMultiSelect = item.isMultiSelect
                var questionId = item.id
                contentHtml += `
                    <div class="row m-4">
                        <div class="col">
                            <div class="card" style="width: 620px; min-height: 200px;">
                              <div class="card-body">
                                
                                <h5 class="card-title">Question ${questionNum}</h5>
                                <p class="card-text">${questionName}</p>`;

                if (questionType == "TFQuestion") {
                    var tfAnswerHtml = `<div class="card-footer">
                                          <input type="radio" id="trueOption" class="tfAnswer" name="tfAnswer-${questionId}" value="True" required>
                                          <label for="trueOption">True</label><br>
                                          <input type="radio" id="falseOption" class="tfAnswer" name="tfAnswer-${questionId}" value="False" required>
                                          <label for="falseOption">False</label><br>
                                          <div class="row">
                                            <div id="TFQuesCorrectAnswer" class="col text-center p-4">
                                                <strong class="displayCorrectAnswer" id="tfAnswer-${questionId}"></strong>
                                            </div>
                                          </div>
                                        </div>
                                        `;

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
                                        <input type="${inputType}" id="option1" class="mcq${selectType}" name="mcq${selectType}-${questionId}" value="${option1}">
                                        <label for="option1">${option1}</label><br>
                                        <input type="${inputType}" id="option2" class="mcq${selectType}" name="mcq${selectType}-${questionId}" value="${option2}">
                                        <label for="option2">${option2}</label><br>
                                        <input type="${inputType}" id="option3" class="mcq${selectType}" name="mcq${selectType}-${questionId}" value="${option3}">
                                        <label for="option3">${option3}</label><br>
                                        <input type="${inputType}" id="option4" class="mcq${selectType}" name="mcq${selectType}-${questionId}" value="${option4}">
                                        <label for="option4">${option4}</label><br>
                                        <div class="row">
                                            <div class="col text-center p-4">
                                                <strong class="displayCorrectAnswer" id="mcq${selectType}-${questionId}"></strong>
                                            </div>
                                        </div>
                                    </div>
                                    `;
                }


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
                                <div class="col-6"></div>
                                <div class="col">
                                    <button class="btn btn-primary" onclick="resetForm()">Re-attempt</button>
                                    <button class="btn btn-primary" onclick="submitForm(${numOfQuestions}, ${quizId})" id="ajaxBtn">Submit</button>
                                </div>
                                <div class="col-2"></div>

                            </div>`;
            contentHtml += `</form>`;

            $("#view_course_material").html(contentHtml);
            startTimer()

        }
    });
}

function resetForm() {
    var quizForm = document.getElementById("quizForm")
    var dataList = []
    var quesObj = {}
    var multiSelectList = []
    var multiSelectObj = {}
    var checkQuesType = ""
    // Retrieve the input elements in the quizForm
    for (var i = 0; i < quizForm.length; i++) {
        if (quizForm[i].checked == true) {
            quizForm[i].checked = false
        }
    }
    $("#displayUserMarks-card").attr("class", "d-none")
    $(".displayCorrectAnswer").html(``)

}

function getUserAns() {
    // Loop through the quizForm
    var quizForm = document.getElementById("quizForm")
    var dataList = []
    var quesObj = {}
    var multiSelectList = []
    var multiSelectObj = {}
    var checkQuesType = ""
    // Retrieve the input elements in the quizForm
    for (var i = 0; i < quizForm.length; i++) {
        if (quizForm[i].checked == true) {
            var quesId = quizForm[i].name.split("-")[1]
            var quesType = quizForm[i].name.split("-")[0]

            let tempArr = quizForm[i].value.split(" ")
            var ansOption = tempArr[tempArr.length - 1]
            if (quesType == "mcqmultiSelect") {
                checkQuesType = true

                if (multiSelectList.length != 0) {
                    var currentStr = multiSelectObj["answer"]
                    currentStr += "," + ansOption
                    multiSelectObj["answer"] = currentStr
                } else {
                    multiSelectObj["questionId"] = parseInt(quesId)
                    multiSelectObj["answer"] = ansOption
                    multiSelectList.push(multiSelectObj);
                }
            } else {
                if (checkQuesType == true) {
                    dataList.push(multiSelectList[0])

                }
                quesObj["questionId"] = parseInt(quesId)
                quesObj["answer"] = ansOption
                dataList.push(quesObj)
            }

            quesObj = {}

        }
    }
    return dataList
}

function submitForm(numOfQuestions, quizId) {
    event.preventDefault();
    //form validation
    const singleSelectRadio = document.querySelectorAll('input[class="mcqsingleSelect"]:checked');
    const multiSelectCheckBoxes = document.querySelectorAll('input[class="mcqmultiSelect"]:checked');
    const tfAnswer = document.querySelectorAll('input[class="tfAnswer"]:checked');

    if (singleSelectRadio.length == 0 || multiSelectCheckBoxes.length == 0 || tfAnswer.length == 0) {
        $('#liveToast').addClass('bg-danger')
        $('#toastHeader').addClass('bg-danger')
        var option = {
            animation: true,
            delay: 5000
        }
        var myAlert = document.getElementById('liveToast')
        var dataHtml = `All question must be answered`;
        $('#toastBody').html(dataHtml)
        var bsAlert = new bootstrap.Toast(myAlert, option);
        bsAlert.show();
        return;
    }


    // Get user answer
    var userAnswer = getUserAns()
    console.log(userAnswer)

    // check if user has attemptted the quiz using GET request
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
                attemptedQuiz = true
                prevAttemptAnswer = data.data

            }
        },
        error: function (jqXhr, textStatus, errorMessage) {
            console.log(typeof (errorMessage), textStatus, jqXhr)
            attemptedQuiz = false
            console.log("there is no user answer")

        }
    });
    console.log(prevAttemptAnswer)

    var totalMarks = 0
    var correctOrWrong = ""
    var updatedDataList = []
    var newObj = {}
    // retrieve question answer
    $.ajax(`/api/Quizzes/${quizId}`, {
        type: 'GET',  // http method
        // async need to be false in order to access variable outside of ajax function
        async: false,
        success: function (data, status, xhr) {
            console.log(data, status)
            var questionsArr = data.questions
            var isCorrect = ""
            var marksAwarded = 0
            for (let x = 0; x < questionsArr.length; x++) {
                let quesObj = questionsArr[x]
                let currQnId = quesObj["questionId"]
                let actualQnAnswer = quesObj["answer"]
                let userAnsObj = userAnswer[x]
                console.log(userAnsObj)

                // Mark user answer
                if (actualQnAnswer == userAnsObj["answer"]) {
                    correctOrWrong = "<span class='text-success'>You are correct! </span>"
                    totalMarks += quesObj.marks
                    marksAwarded += quesObj.marks
                    isCorrect = true
                } else {
                    correctOrWrong = "<span class='text-danger'>Sorry, that is the wrong answer... </span>"
                    isCorrect = false
                }

                // display correct answer on UI
                // Retrieve the displayCorrectAnswer elements in the quizForm
                $('.displayCorrectAnswer').each(function () {
                    var elementID = this.id.split("-")[this.id.split("-").length - 1]
                    if (elementID == userAnsObj["questionId"]) {
                        this.innerHTML = correctOrWrong + "The answer is " + actualQnAnswer
                    }

                })

                console.log(!jQuery.isEmptyObject(prevAttemptAnswer))
                if (!jQuery.isEmptyObject(prevAttemptAnswer)) {
                    prevAttemptObj = prevAttemptAnswer.find(obj => obj.questionId == userAnsObj["questionId"]);
                    newObj["id"] = prevAttemptObj["id"]
                    newObj["questionId"] = userAnsObj["questionId"]
                    newObj["answer"] = userAnsObj["answer"]
                    newObj["isCorrect"] = isCorrect
                    newObj["marks"] = marksAwarded
                    marksAwarded = 0
                    updatedDataList.push(newObj)
                    newObj = {}
                }

                






            }

        },
        error: function (jqXhr, textStatus, errorMessage) {
            console.log(typeof (errorMessage), textStatus, jqXhr)
        }
    });
    console.log(updatedDataList)
    console.log(totalMarks)
    // Display user total marks
    $("#displayUserMarks-card").attr("class", "")
    $("#displayUserMarks-text").html(`Your quiz score is ${totalMarks}`)



    // Have not attempt, use POST request with qn & ans jsonData

    // Attempted the quiz, use PUT request with all the JSON data
    var httpMethod = ""
    var dataJson = ""
    if (attemptedQuiz) {
        httpMethod = "PUT"
        dataJson = JSON.stringify(updatedDataList);
        console.log(dataJson)
    } else {
        httpMethod = "POST"
        console.log(userAnswer)
        dataJson = JSON.stringify(userAnswer);
        console.log(dataJson)
    }

    $.ajax('/api/UserAnswers', {
        type: httpMethod,  // http method
        contentType: "application/json",
        dataType: "json",
        data: dataJson,  // data to submit
        success: function (data, status, xhr) {
            console.log(data, status)
            var option = {
                animation: true,
                delay: 5000
            }
            $('#liveToast').addClass('bg-success')
            $('#toastHeader').addClass('bg-success')
            var myAlert = document.getElementById('liveToast')
            var dataHtml = `Your quiz has been submitted successfully.`;
            $('#toastBody').html(dataHtml)
            var bsAlert = new bootstrap.Toast(myAlert, option);
            bsAlert.show();
        },
        error: function (jqXhr, textStatus, errorMessage) {
            console.log(typeof (errorMessage), textStatus, jqXhr)
            $('#liveToast').addClass('bg-danger')
            $('#toastHeader').addClass('bg-danger')
            var option = {
                animation: true,
                delay: 5000
            }
            var myAlert = document.getElementById('liveToast')
            var dataHtml = `There is an error in submitting the quiz. Error message: ${errorMessage}`;
            $('#toastBody').html(dataHtml)
            var bsAlert = new bootstrap.Toast(myAlert, option);
            bsAlert.show();
        }
    });
}

