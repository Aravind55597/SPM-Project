$(document).ready(function () {
    var userID = $('#userid').val();
	viewCourseClassDT(userID);
});


function closeEventHandler() {
	$(".btn-close").click(function () {
		//clear away data inside modal before closing 
		$("#view_classes").html("");
		$(".overlay").hide();
	});
}

function viewCourseClassDT(userid) {

	var RetrieveCourseClass = $("#get-course-classes-datatable").val() + "?isTrainer=true&lmsUserId=" + userid;

	var table = $('#course_class_datatable').DataTable({

		//width of column siwll be auto 
		autoWidth: false,

		filter: true,

		searching: true,

		//make the table responsive 
		responsive: true,
		//show that the table is processign while retrieiving data 
		processing: true,
		//sentence to be shown when table is showing that it is retrieving data from the server 
		language: {
			processing: "DataTables is currently busy"
		},

		//enable server side 
		serverSide: true,


		//send ajax request to server to Retrieve customers
		ajax: {
			url: RetrieveCourseClass,
			type: "POST",
			contentType: "application/json",
			dataType: "json",
			data: function (d) {
				console.log(JSON.stringify(d))
				return JSON.stringify(d);
			},
			error: function (xhr, error, code) {
				console.log(xhr);
				console.log(code);
			}
		},


		//default order and sort. In this case ,order by ID in ascending order (Id is column number 1)
		order: [[0, "asc"]],


		//define the properties of each column (This takes precedence, need to define for each column)
		//use this to state the array key 
		//data table will receive an array of objects where each object is for each row
		//the key for the object corresponds to the columns
		columns: [
			//data: null means it is not Retrieveing data from the server
			//column can't be ordered
			//regarding name (https://datatables.net/reference/option/columns.name)
			{ name: 'CourseName', data: 'CourseName' },
			{ name: 'ClassName', data: 'ClassName' },
			{ name: 'StartDate', data: 'StartDate' },
			{ name: 'EndDate', data: 'EndDate' },
			{ name: 'NumOfChapters', data: 'NumOfChapters' },
			//{ name: 'NumOfStudents', data: 'NumOfStudents' },
			//responsive priority is an option to state the priority of the column to be view when the screen is smaller
			//data: null means it is not Retrieveing data from the server
			{ name: 'Ungraded', data: null, responsivePriority: -1, orderable: false },
			{ name: 'Graded', data: null, responsivePriority: -1, orderable: false },
		],

		//define the properties of each column (very similar function as columns option above.Don't need to define all the column )
		//I suggest to use this just to render stuff such as buttons/any elements OR processign the result to display in diff format eg. format date string
		columnDefs: [

			{
				targets: [2, 3],
				render: function (data, type, full, meta) {
					return moment(data).format('Do MMMM YYYY, h:mm a')
						;

				},
			},

			{

				targets: 5,
				render: function (data, type, full, meta) {

					var courseClassId = data.DT_RowId
					var boolean = dateComparison(data.StartDate, data.EndDate);

					if (boolean == true ) {
						return `
							<button class="btn btn-primary viewChaptersbtn" onclick="viewChapters(${courseClassId})">View Chapters</button>
							`
							;
					}
					else {
						return `
							<a href="javascript:;" class="btn btn-primary viewChaptersbtn disabled">View Chapters</a>
							`
							;
					}

				},
			},

			{

				targets: 6,
				render: function (data, type, full, meta) {

					var courseClassId = data.DT_RowId

					var boolean = dateComparison(data.StartDate, data.EndDate);

					if (boolean== true ) {
						return `
							<button class="btn btn-primary editGradedbtn" style="width:100px" onclick="createGradedQuiz(${courseClassId})" >Create Quiz</button>
							<br/>
							<br/>
							<a href="javascript:;" class="btn btn-primary updateGradedbtn" style="width:100px" >Update Quiz</a>
							`
							;
					}
					else {
						return `
							<a href="javascript:;" class="btn btn-primary editGradedbtn disabled" style="width:100px">Create Quiz</a>
							<br/>
							<br/>
							<a href="javascript:;" class="btn btn-primary updateGradedbtn disabled" style="width:100px">Update Quiz</a>
							`
							;
					}


				},
			},


		],


		//https://datatables.net/reference/option/createdRow
		// add events, class name information or otherwise format the row when it is created
		//Retrieve DT_RowData from data and add the object to row using jquery (read the serversiderendering of datatables )
		createdRow: function (row, data, dataIndex) {

			//i am sending the data for the row again just for testing purposes (just id , firstname , lastname)
			$(row).data(data.DT_RowData);

			//can use DT_RowAttr for toher stuff (check https://datatables.net/manual/server-side)
		},



	});

}


function dateComparison(StartDate, EndDate) {
	var startdate = moment(StartDate)
	var enddate = moment(EndDate)
	var now = moment();

	if (startdate > now && enddate > now) {
		return true
	}
	else {
		return false
    }
}

function viewChapters(courseClassId) {
	//pass course id to the get_chapter function 
	get_chapter(courseClassId);
}

function get_chapter(courseClassId) {
	var retrieveChapters = $("#get-chapters").val() + "?courseClassId=" + courseClassId;

	$.ajax({
		url: retrieveChapters, success: function (result) {
			// sort chapters in ascending order
			result.data.sort((a, b) => (a.name > b.name ? 1 : -1))
			var dataHtml = ``;
			var chapterId = 0;
			$.each(result.data, function (index, item) {
				var chapterNum = index + 1
				var chapterDescription = item.description
				chapterId = item.id
				dataHtml += `<div class="row align-items-center">
								<div class="col">
									<div class="card mb-3" style="max-width: 540px; background-color: fefaf4;">
										<div class="row g-0">
											<div class="col">
												<div class="card-body">
													<h5 class="card-title">Chapter ${chapterNum.toString()}</h5>

													<p class="card-text">${chapterDescription}</p>


													<p class="card-text"><small class="text-muted"></small></p>
													<div class="row">
														<div class="col"></div>
														<div class="col-6">

															
															<a style="width:180px" href="/Trainer/SubmitQuiz?chapterId=${chapterId}&courseClassId=${courseClassId}" class="btn btn-primary                       EditUngradedbtn">Create Ungraded Quiz</a>


															<br/>
															<br/>
															<a style="width:180px" href="javascript:;" class="btn btn-primary UpdateUngradedbtn" >Update Ungraded Quiz</a>
															
														</div>
														<div class="col"></div>
													</div>
												</div>
											</div>
										</div>
									</div>
								</div>
							</div>`;
			});

			
			$("#view_classes").html(dataHtml);

			//show overlay
			$(".overlay").show();
			//event handler for close button
			closeEventHandler();
		}
	});
}


function createUngradedQuiz(chapterID, courseClassId) {
	//need to send chapterID + courseClassId +  json strcuture for ungraded

	window.location.href = `/Trainer/SubmitQuiz?chapterId=${chapterId}&courseClassId=${courseClassId}`


}	


function createGradedQuiz(courseClassId) {
	//need to send courseClassID +  json strcuture for ungraded
	window.location.href = `/Trainer/SubmitQuiz?courseClassId=${courseClassId}`
}

