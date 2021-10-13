
$(document).ready(function () {
	viewCoursesDT();
	
});



function closeModal() {
	$(".btn-close").click(function () {
		$(".overlay").hide();
		clearForm();
		$("#couseNameInput").html("");

	});
}

function clearForm() {
	$('#addCourseForm').trigger("reset");
	$('#addClassForm').trigger("reset");

}


function openAddCourseModal() {
	$("#addCoursebtn").click(function () {
		$(".overlay").show();
		$("#addCoursePopUp").show();
		$("#addClassPopUp").hide();
	});
	closeModal();
}


function submitCourseEvent(table) {
	$("#submitCourse").click(function () {
		event.preventDefault();
		//add ajax post to database
		//values to be sent in ajax
		var coursename = $('#coursename').val();

		//ajax success: after ajax is successful 

		//reload table
		table.ajax.reload();

		//clear form inputs
		clearForm();
		//close modal
		$(".overlay").hide();

		$.notify("Successfully added New Course", {
			className: 'success',
			globalPosition: 'top center'
		});


	});

}

function addCourseEvent(table) {
	//open modal to add class
	openAddCourseModal();
	submitCourseEvent(table);
}



function openAddClassModal() {
	$(".overlay").show();
	$("#addCoursePopUp").hide();
	$("#addClassPopUp").show();
	closeModal();

}



function submitClassEvent(table) {
	$("#submitClass").click(function () {
		event.preventDefault();
		//add ajax post to database

		//values to be sent in ajax
		var coursename = $("#couseNameInput").val();
		var classname = $("#classname").val();
		var slots = $("#inputSlots").val();



		//ajax success: after ajax is successful 

		//reload the table again 
		table.ajax.reload();

		//clear form inputs
		clearForm();
		//close modal
		$(".overlay").hide();

	
		$.notify("Successfully added New Class", {
			className: 'success',
			globalPosition: 'top center'
		});

	});

}

function addClassEvent(table) {

	//event handler for add classes 
	table.on('click', '.addClassbtn', function () {
		var row = $(this).parents('tr')[0];
		//for row data
		var row_data = table.row(row).data();
		console.log(row_data)

		var course = row_data.CourseName;
		$("#couseNameInput").html(course);

		//open modal to add class
		openAddClassModal();

	});


	//submit event 
	submitClassEvent(table);

}


function deleteCourseEvent(table) {

	table.on('click', '.deleteCoursebtn', function () {
		var row = $(this).parents('tr')[0];
		//for row data
		var row_data = table.row(row).data();
		var course = row_data.CourseName;
		console.log(row_data)

		//ajax success: after ajax is successful 


		//reload table
		table.ajax.reload();


		$.notify("Successfully Deleted Course", {
			className: 'success',
			globalPosition: 'top center'
		});
	});
}


function viewCoursesDT() {

	var RetrieveCourses = $("#get-courses-datatable").val();

	var table = $('#course_datatable').DataTable({

		//width of column siwll be auto 
		autoWidth: false,

		filter:true,

		searching:true,

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
			url: RetrieveCourses,
			type: "POST",
			contentType: "application/json",
			dataType: "json",
			data: function (d) {
				console.log(JSON.stringify(d))
				return JSON.stringify(d);
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
			{ name: 'NumberofClasses', data: 'NumberOfClasses' },
			{ name: 'CreatedDate', data: 'CreatedDate' },
			{ name: 'UpdatedDate', data: 'UpdatedDate' },

			//responsive priority is an option to state the priority of the column to be view when the screen is smaller
			//data: null means it is not Retrieveing data from the server
			{ name: 'Actions', data: null, responsivePriority: -1, orderable: false },
		],

		//define the properties of each column (very similar function as columns option above.Don't need to define all the column )
		//I suggest to use this just to render stuff such as buttons/any elements OR processign the result to display in diff format eg. format date string
		columnDefs: [


			{
				targets: [2,3] ,
				render: function (data, type, full, meta) {
			
					return moment(data).format('Do MMMM YYYY, h:mm a')
						;
				
				},
			},



			{
				//target last column
				targets: -1,
				render: function (data, type, full, meta) {
					return `<a  class="btn btn-success addClassbtn"> Add Class</a>
							<a class="btn btn-danger deleteCoursebtn" > Delete Course</a>
							`
					;
				},
			},

			
		],


		//https://datatables.net/reference/option/createdRow
		// add events, class name information or otherwise format the row when it is created
		//Retrieve DT_RowData from data and add the object to row using jquery (read the serversiderendering of datatables )
		createdRow: function (row, data, dataIndex) {

			$(row).data(data.DT_RowData);


			//can use DT_RowAttr for toher stuff (check https://datatables.net/manual/server-side)
		},


	});


	//event handlers
	addClassEvent(table);
	addCourseEvent(table);
	deleteCourseEvent(table);


}





