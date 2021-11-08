



$("#create_MCQqn").click(function () {
	$("#question_cards").append(`<div class="questions card p-4 m-4">

                                            <div class="form-check form-check-inline justify-content-center">
                                                <input class="form-check-input multiSelectRadio" type="radio" name="multiselectRadio" id="multiselectTrue" value="true" checked>
                                                <label class="form-check-label" for="inlineRadio2">MultiSelect Qn</label>
                                            </div>
                                            <div class="form-check form-check-inline justify-content-center">
                                                <input class="form-check-input multiSelectRadio" type="radio" name="multiselectRadio" id="multiselectFalse" value="false" >
                                                <label class="form-check-label" for="inlineRadio2">Single Answer Qn</label>
                                            </div>

                                        <textarea class="form-control my-4 ml-2 qnText" id="exampleFormControlTextarea1" rows="2" placeholder="Write question here"></textarea>
                                        <div class="question_options">
                                            <div class="input-group mb-3 qnOption">
                                                <span class="input-group-text" id="inputGroup-sizing-default">1.</span>
                                                <input type="text" class="form-control qnOption" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" id="option1">
                                            </div>
                                            <div class="input-group mb-3 qnOption">
                                                <span class="input-group-text" id="inputGroup-sizing-default">2.</span>
                                                <input type="text" class="form-control qnOption" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" id="option2">
                                            </div>
                                            <div class="input-group mb-3 qnOption">
                                                <span class="input-group-text" id="inputGroup-sizing-default">3.</span>
                                                <input type="text" class="form-control qnOption" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" id="option3">
                                            </div>
                                            <div class="input-group mb-3 qnOption">
                                                <span class="input-group-text" id="inputGroup-sizing-default">4.</span>
                                                <input type="text" class="form-control qnOption" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" id="option4">
                                            </div>

                                        </div>
                                        <div class="input-group mb-3">
                                            <span class="input-group-text" id="inputGroup-sizing-default">Set Answer</span>
                                            <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" id="qnAnswer">
                                        </div>
                                        <div class="input-group mb-3">
                                            <span class="input-group-text" id="inputGroup-sizing-default">Set Marks</span>
                                            <input type="number" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" id="qnMarks">
                                        </div>
                                        

                                    </div>`);
});
$("#create_TFqn").click(function () {
	
	$("#question_cards").append(`<div class="questions card p-4 m-4">
                                        <textarea class="form-control my-4 ml-2 qnText" id="exampleFormControlTextarea1" rows="2" placeholder="Write question here"></textarea>
                                        <div class="question_options">
                                            <div class="input-group mb-3 qnOption">
                                                <span class="input-group-text" id="inputGroup-sizing-default">True.</span>
                                                <input type="text" class="form-control qnOption" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" id="trueOption">
                                            </div>
                                            <div class="input-group mb-3 qnOption">
                                                <span class="input-group-text" id="inputGroup-sizing-default">False.</span>
                                                <input type="text" class="form-control qnOption" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" id="FalseOption">
                                            </div>
                                            

                                        </div>
                                        <div class="input-group mb-3">
                                            <span class="input-group-text" id="inputGroup-sizing-default">Set Answer</span>
                                            <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" id="qnAnswer">
                                        </div>
                                        <div class="input-group mb-3">
                                            <span class="input-group-text" id="inputGroup-sizing-default">Set Marks</span>
                                            <input type="number" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-default" id="qnMarks">
                                        </div>
                                        

                                    </div>`);
});


	$("#createQuiz").click(function () {
		var jsObjectForm = {
			"Name": "Test",
			"IsGraded": false,
			"Description": "Test descriptionsdfsdfsdfsd",
			"ChapterId": null,
			"CourseClassId": null,
			"TimeLimit": 20,
			"Questions": []
		};
		var questionObject = {
			"ImageUrl": "https://www.howtogeek.com/wp-content/uploads/2018/06/shutterstock_1006988770.png?height=200p&trim=2,2,2,2",
			"Marks": 0,
			"Question": "Test",
			"QuestionType": "TFQuestion",
			"Answer": "true",
			"TrueOption": null,
			"FalseOption": null,
			"Option1": null,
			"Option2": null,
			"Option3": null,
			"Option4": null,
			"IsMultiSelect": false
		};

		//Check if graded
		jsObjectForm.Name = $("#quizName").val();
		jsObjectForm.Description = $("#quizDescription").val();
		jsObjectForm.TimeLimit = parseInt($("#quizDuration").val());
		/// Issue at finding if checked
		if ($('.gradedGrp').find('#gradedQuiz').is(':checked')) {
			jsObjectForm.IsGraded = true;
			jsObjectForm.ChapterId = null;
		} else {
			console.log("test");
			jsObjectForm.IsGraded = false;
			jsObjectForm.ChapterId = parseInt($("#ChapterID").val());
			console.log($("#ChapterID").val());
			console.log(typeof (jsObjectForm.ChapterId));
			console.log(jsObjectForm.ChapterId)
		};
		jsObjectForm.CourseClassId = parseInt($("#CourseClassID").val());
		
		//Loop through questions
		$(".questions").each(function () {
			//Retrieve qn from textarea
			questionObject.Question = $(this).find("textarea").val();
			//Retrieve answer from set answers field
			questionObject.Answer = $(this).find("#qnAnswer").val();
			//Retrieve marks
			questionObject.Marks = parseInt($(this).find("#qnMarks").val());
			//Retrieve option from options (count, if 2 = truefalseqn)
			if ($(this).find('.qnOption').length > 2) {
				//MCQ
				questionObject.QuestionType = "McqQuestion";
				questionObject.Option1 = $(this).find("#option1").val();
				questionObject.Option2 = $(this).find("#option2").val();
				questionObject.Option3 = $(this).find("#option3").val();
				questionObject.Option4 = $(this).find("#option4").val();
				questionObject.TrueOption = null;
				questionObject.FalseOption = null;
				if ($(this).find('#multiselectTrue').is(':checked')) {
					questionObject.IsMultiSelect = true;
				} else {
					questionObject.IsMultiSelect = false;
				};
			} else {
				//TF Qn
				questionObject.QuestionType = "TFQuestion";
				questionObject.TrueOption = $(this).find("#trueOption").val();
				questionObject.FalseOption = $(this).find("#falseOption").val();
				questionObject.IsMultiSelect = false;
				questionObject.Option1 = null;
				questionObject.Option2 = null;
				questionObject.Option3 = null;
				questionObject.Option4 = null;
			};
			
			jsObjectForm.Questions.push(questionObject);
		});
		console.log(jsObjectForm);
		$.ajax({
		   url: '/api/Quizzes',
			data: JSON.stringify(jsObjectForm),
			contentType: 'application/json',
			type: 'POST',
			success: function () {
				alert("success");
			},
		   error: function() {
		      alert("error");
		   }
		   
		});
	});

