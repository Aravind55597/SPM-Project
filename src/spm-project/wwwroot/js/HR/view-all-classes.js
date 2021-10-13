
$(document).ready(function () {
	viewCourseClassDT();

});



function closeModal() {
	$(".btn-close").click(function () {
		$(".overlay").hide();
	});
}

function notification(notificationString) {

	$.notify(notificationString, {
		className: 'success',
		globalPosition: 'top center'
	});
}

function openViewClassModal() {
	//modal must always show classlist tab upon open 
	$(".nav-link").removeClass("active");
	$('a[href="#ClassList"]').addClass("active");
	$(".overlay").show();
	closeModal();
}



function viewClassEvent(table) {

	//event handler for view class list  
	table.on('click', '.viewClassbtn', function () {

		var row = $(this).parents('tr')[0];
		var row_data = table.row(row).data();

		var classname = row_data.ClassName;
		$(".classNameInput").html(classname);


		//get of selected row classID
		var classID = row_data.DT_RowId;
		console.log(classID)

		//destroy previous datatable before you can initialize a new table 
		$("#individual_class_datatable").DataTable().clear().destroy();
		$("#assign_trainer_datatable").DataTable().clear().destroy();
		$("#assign_learner_datatable").DataTable().clear().destroy();




		//initialize class list DT using classID
		generalDT("viewClassList", classID);
		//initialize eligible trainers DT using classID
		generalDT("assignTrainer", classID);
		//initialize eligible Learners DT using classID
		generalDT("assignLearner", classID);

		//open modal 
		openViewClassModal();
		

	});

}




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

//group select checkbox 
function groupSelectHandler(table) {
	table.on('change', '.group-checkable', function () {

		var set = $(this).closest('table').find('td:first-child .checkable');
		var checked = $(this).is(':checked');

		var selectedList = [];

		$(set).each(function () {
			if (checked) {
				$(this).prop('checked', true);
				table.rows($(this).closest('tr')).select();
				//get data of group select rows
				selectedList.push(table.rows($(this).closest('tr')).data()[0]);

			}
			else {
				$(this).prop('checked', false);
				table.rows($(this).closest('tr')).deselect();
			}
		});

		console.log(selectedList);

	});

}

//individual select checkbox
function individualSelectHandler(table) {

	table.on('change', '.checkable', function () {

		//RETREIVE row where select was triiggered (check whether it is selceted )
		//$(row).data("DT_RowId")  -> COURSE ID 
		var selectedList = [];

		var NumSelected = $('.selected').length;

		var indexList = table.rows({ selected: true }).indexes();
		var rows_data = table.rows(indexList).data();

		for (i = 0; i < NumSelected; i++) {
			selectedList.push(rows_data[i])

		}
		console.log(selectedList);
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
							<a class="btn btn-danger deleteClassbtn" >Delete Class</a>`
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
	deleteClassEvent(table);

}



function generalDT(action, class_ID) {

	//optional_ID can be the class ID or isEligible = true
	var RetrieveValue = null;
	var htmlTableName = null;
	var EmptyTableMsg = null;
	

	if (action == "viewClassList") {

		RetrieveValue = $("#get-engineers-datatable").val() + "?classID=" + class_ID
		htmlTableName = "#individual_class_datatable";
		EmptyTableMsg = "Class is Empty"
		
	}


	else if (action == "assignTrainer") {
		RetrieveValue = $("#get-engineers-datatable").val() + "?classID=" + class_ID + "&isTrainer=True&isEligible=True";
		console.log(RetrieveValue)
		htmlTableName = "#assign_trainer_datatable";
		EmptyTableMsg = "Could not find Eligible Trainers"

	}

	else if (action == "assignLearner") {
		RetrieveValue = $("#get-engineers-datatable").val();
		htmlTableName = "#assign_learner_datatable";
		EmptyTableMsg = "Could not find Eligible Learners"
	}


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

		//enable select in the table 
		select: {
			//allow us to select multiple rows
			style: 'multi',
			//retricts which cells in the table that will trigger table selection 
			//td first child (for each td tag , only the first item (cell) will allow selection. Within the cell , the element with .checkable class is only allowed)
			//this is essentially a css selector used here 
			selector: 'td:first-child .checkable',
		},


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

		//every time the table get initialised (draw or ajax.reload()) , 
		//render this for the header 
		//in this case , render a checkbox for the first header 
		headerCallback: function (thead, data, start, end, display) {
			thead.getElementsByTagName('th')[0].innerHTML = `
                    <label class="checkbox checkbox-single checkbox-solid checkbox-primary mb-0">
                        <input type="checkbox" value="" class="group-checkable"/>
                        <span></span>
                    </label>`;
		},




		//default order and sort. In this case ,order by ID in ascending order (Id is column number 1)
		order: [[1, "asc"]],



		//define the properties of each column (This takes precedence, need to define for each column)
		//use this to state the array key 
		//data table will receive an array of objects where each object is for each row
		//the key for the object corresponds to the columns
		columns: [
			//data: null means it is not Retrieveing data from the server
			//column can't be ordered
			//regarding name (https://datatables.net/reference/option/columns.name)
			{ name: 'Checkbox', data: null, orderable: false },
			{ name: 'Id', data: 'Id' },
			{ name: 'Name', data: 'Name' }

		],

		//define the properties of each column (very similar function as columns option above.Don't need to define all the column )
		//I suggest to use this just to render stuff such as buttons/any elements OR processign the result to display in diff format eg. format date string
		columnDefs: [

			{
				//target first collumn 
				targets: 0,
				render: function (data, type, full, meta) {
					return `
                        <label class="checkbox checkbox-single checkbox-primary mb-0">
                            <input type="checkbox" value="" class="checkable"/>
                            <span></span>
                        </label>`;
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

	//group select checkbox 
	groupSelectHandler(table);
	//individual select checkbox
	individualSelectHandler(table);

}






