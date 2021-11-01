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

function viewCourseClassDT(userID) {

	var RetrieveCourseClass = $("#get-course-classes-datatable").val() + "?isLearner=true&lmsUserId=" + userID;

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
				/*console.log(JSON.stringify(d))*/
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
			//responsive priority is an option to state the priority of the column to be view when the screen is smaller
			//data: null means it is not Retrieveing data from the server
			{ name: 'Actions', data: null, responsivePriority: -1, orderable: false },
		],

		//define the properties of each column (very similar function as columns option above.Don't need to define all the column )
		//I suggest to use this just to render stuff such as buttons/any elements OR processign the result to display in diff format eg. format date string
		columnDefs: [

			{
				targets: [2, 3],
				render: function (data, type, full, meta) {
					return moment(data).format('Do MMMM YYYY, h:mm a');
				},
			},

			{
				//target last column
				targets: -1,
				render: function (data, type, full, meta) {
					return `<a href="javascript:;" class="btn btn-primary viewChaptersbtn" >View Chapters</a>`;
				},
			}

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

	//view chapters event handler
	viewChaptersEventHandler(table);
}


function viewChaptersEventHandler(table) {
	table.on('click', '.viewChaptersbtn', function () {
		var row = $(this).parents('tr')[0];
		var row_data = table.row(row).data();
		console.log(row_data)
		var courseClassId = row_data.DT_RowId;
		//pass course id to the get_chapter function 
		get_chapter(courseClassId);
	});
}

function get_chapter(courseClassId) {
    var retrieveChapters = $("#get-chapters").val() + "?courseClassId=" + courseClassId;

    $.ajax({
        url: retrieveChapters, success: function (result) {
            // sort chapters in ascending order
            result.data.sort((a, b) => (a.name > b.name ? 1 : -1))
            console.log(result)

            var dataHtml = ``;
            $.each(result.data, function (index, item) {
                var name = item.name
                var index = name.indexOf("Chapter");
                var chapterName = name.slice(index, name.length)
                var chapterDescription = item.description
                var resources = item.resourceIds
                var chapterId = item.id
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

                                            <form action="/Learner/ViewCourseMaterial" method="get">
                                            <button type="submit" name="chapterId" value="${chapterId}" class="btn-sign-up btn btn-primary w-100">
                                                  View Course Material
                                                </button>
                                            </form>

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

			//show overlay
			$(".overlay").show();
			//event handler for close button
			closeEventHandler();
        }
    });
}