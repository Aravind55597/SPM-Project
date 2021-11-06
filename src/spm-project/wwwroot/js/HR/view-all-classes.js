

$(document).ready(function () {
	viewCourseClassDT();

});

function notification(notificationString, value) {
	var CLASSNAME = null;
	if (value == "success") {
		CLASSNAME = "Success"
	}
	else if (value == "failed") {
		CLASSNAME = "error"
	}

	$.notify(notificationString, {
		className: CLASSNAME,
		globalPosition: 'top center',
	});
}



function closeModal() {
	$(".btn-close").click(function () {
		$(".overlay").hide();

	});
}

function tabsReloadDT() {
	$(".nav-tabs").click(function () {
		var ID = $('a[class="active"]').prevObject[0].activeElement.id;
		console.log(ID)
		if (ID == "ViewClassListTAB") {
			$('#individual_class_datatable').DataTable().ajax.reload();
		}
		else if (ID == "AssignTrainerTAB") {
			$('#individual_class_datatable').DataTable().ajax.reload();
		}
		else if (ID == "AssignLearnerTAB") {
			$('#assign_learner_datatable').DataTable().ajax.reload();
		}
	});
}


function openViewClassModal() {
	//modal must always show classlist tab upon open 
	$(".nav-link").removeClass("active");
	$('a[href="#ClassList"]').addClass("active");

	$(".tab-pane").removeClass("show active");
	$('#ClassList').addClass("show active");

	$(".overlay").show();

	closeModal();
	tabsReloadDT();
}



function viewClassEvent(table) {

	//event handler for view class list  
	table.on('click', '.viewClassbtn', function () {

		var row = $(this).parents('tr')[0];
		var row_data = table.row(row).data();

		var classname = row_data.ClassName;
		$(".classNameInput").html(classname);



		//get selected row classID
		var classID = row_data.DT_RowId;
		var presentCount = row_data.NumOfStudents
		var maxCount = row_data.Slots

		//destroy previous datatable before you can initialize a new table 
		destroyDT(["#individual_class_datatable", "#assign_trainer_datatable", "#assign_learner_datatable"]);


		//initialize class list DT using classID
		generalDT("viewClassList", classID, presentCount, maxCount );
		//initialize eligible trainers DT using classID
		generalDT("assignTrainer", classID, presentCount, maxCount);
		//initialize eligible Learners DT using classID
		generalDT("assignLearner", classID, presentCount, maxCount);

		//open modal 
		openViewClassModal();
		

	});

}



function destroyDT(list_DT_names) {
	for (i = 0; i < list_DT_names.length; i++) {
		// clear and destroy DT
		$(list_DT_names[i]).DataTable().clear().destroy();
		//remove event listeners attached to DTs that are destroyed
		$(list_DT_names[i]).off();

    }
}


/*
function deleteClassEvent(table) {

	table.on('click', '.deleteClassbtn', function () {
		var row = $(this).parents('tr')[0];
		//for row data
		var row_data = table.row(row).data();
		var classname = row_data.ClassName;
		console.log(classname)

		//ajax success: after ajax is successful 

		//reload table
		table.ajax.reload();

		notification("Class has been Deleted")


	});
}
*/

function queryStringHandler(action, classid, userid) {
	var query = null;

	if (action == "addTrainer") {
		query = "/api/CourseClasses/AssignTrainerToClass" + "?trainerId=" + userid + "&" + "classId=" +  classid ;
	}

	else if (action == "addLearner") {
		query = "/api/CourseClasses/addlearner" + "?learnerId=" + userid + "&" + "classId=" + classid;
    }
	else if (action == "withdrawLearner") {
		query = "/api/CourseClasses/WithdrawLearner" + "?learnerId=" + userid + "&" + "classId=" + classid;
	}

	return query

}

function AddWithdrawEvent(table, class_ID, action) {

	var buttonName = null;
	var message = null;
	var failMsg = null;
	var query = null;


	if (action == "addLearner") {
		buttonName = '.addLearner';
		message = "Learner has been Added";
		failMsg = "Learner could not be added";

	}

	else if (action == "addTrainer") {
		buttonName = '.addTrainer';
		message = "Trainer has been Added";
		failMsg = "Trainer could not be added";
	}

	else if (action == "withdrawLearner") {
		buttonName = '.withdrawLearner';
		message = "Learner has been Withdrawn";
		failMsg = "Learner could not be withdrawn";
	}

	table.on('click', buttonName, function () {
		var row = $(this).parents('tr')[0];
		//for row data
		var row_data = table.row(row).data();
		var userID = row_data.Id;
		var classID = class_ID;

		console.log(userID);
		console.log(classID);

		query = queryStringHandler(action, classID, userID);
		console.log(query);

		if (query != null) {
			$.ajax({
				url: query,
				method: "POST",
				success: function (data) {
					$('#individual_class_datatable').DataTable().ajax.reload();
					$('#assign_trainer_datatable').DataTable().ajax.reload();
					$('#assign_learner_datatable').DataTable().ajax.reload();
					notification(message,"success");
				},
				error: function (data) {
					notification(failMsg,"failed");
				},
				async: true
			});
		}

		
	});
}




function viewCourseClassDT() {

	var RetrieveCourseClass = $("#get-course-classes-datatable").val();

	var table = $('#course_class_datatable').DataTable({

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
			{ name: 'NumOfStudents', data: 'NumOfStudents' },
			{ name: 'Slots', data: 'Slots' },
			//responsive priority is an option to state the priority of the column to be view when the screen is smaller
			//data: null means it is not Retrieveing data from the server
			{ name: 'Actions', data: null, responsivePriority: -1, orderable: false },
		],

		//define the properties of each column (very similar function as columns option above.Don't need to define all the column )
		//I suggest to use this just to render stuff such as buttons/any elements OR processign the result to display in diff format eg. format date string
		columnDefs: [

			{
				//target last column
				targets: -1,
				render: function (data, type, full, meta) {
					return `<a href="javascript:;" class="btn btn-primary viewClassbtn" >View Class List</a>
							`
					;
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

	viewClassEvent(table);
	//deleteClassEvent(table);

}



function generalDT(action, class_ID, presentCount, maxCount) {

	//optional_ID can be the class ID or isEligible = true
	var RetrieveValue = null;
	var htmlTableName = null;
	var EmptyTableMsg = null;
	var button = null;
	

	if (action == "viewClassList") {

		RetrieveValue = $("#get-engineers-datatable").val() + "?classId=" + class_ID;
		htmlTableName = "#individual_class_datatable";
		EmptyTableMsg = "Class is Empty"
		button = `<a href="javascript:;" class="btn btn-danger withdrawLearner" >Remove</a>`
		
	}


	else if (action == "assignTrainer") {
		RetrieveValue = $("#get-engineers-datatable").val() + "?classId=" + class_ID + "&isTrainer=True&isEligible=True";
		htmlTableName = "#assign_trainer_datatable";
		EmptyTableMsg = "Could not find Eligible Trainers"
		button = `<a href="javascript:;" class="btn btn-success addTrainer" >Add Trainer</a>`

	}

	else if (action == "assignLearner") {
		RetrieveValue = $("#get-engineers-datatable").val() + "?classId=" + class_ID + "&isLearner=True&isEligible=True";
		htmlTableName = "#assign_learner_datatable";
		EmptyTableMsg = "Could not find Eligible Learners"
		button = `<a href="javascript:;" class="btn btn-success addLearner" >Add Learner</a>`
	}

	console.log(RetrieveValue)


	var table = $(htmlTableName).DataTable({


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
			processing: "DataTables is currently busy",
			emptyTable: EmptyTableMsg
		},

		//enable server side 
		serverSide: true,


		//send ajax request to server to Retrieve customers
		ajax: {
			url: RetrieveValue,
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
	
			{ name: 'Id', data: 'Id' },
			{ name: 'Name', data: 'Name' },
			{ name: 'Actions', data: null, responsivePriority: -1, orderable: false },

		],

		//define the properties of each column (very similar function as columns option above.Don't need to define all the column )
		//I suggest to use this just to render stuff such as buttons/any elements OR processign the result to display in diff format eg. format date string
		columnDefs: [

			{
				//target last column
				targets: -1,
				render: function (data, type, full, meta) {
					// prevent withdrawal of trainer or assiging of learner if present class slots is more than or equal to max slots
					if ((action == "viewClassList" && data.Role == "Trainer") || (action == "assignLearner" && presentCount >= maxCount) ) {
						return null;
						
					}
					else {
						return button;
					}
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
	AddWithdrawEvent(table, class_ID, "withdrawLearner");
	AddWithdrawEvent(table, class_ID, "addLearner");
	AddWithdrawEvent(table, class_ID, "addTrainer");


}






