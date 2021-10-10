
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
			url: RetrieveCourses,
			type: "POST",
			contentType: "application/json",
			dataType: "json",
			data: function (d) {
				console.log(JSON.stringify(d))
				return JSON.stringify(d);
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


			{
				targets: [3,4] ,
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

			//$(row).data("DT_RowId" , data.DT_RowId);


			//can use DT_RowAttr for toher stuff (check https://datatables.net/manual/server-side)
		},



	});


	//event handlers
	addClassEvent(table);
	addCourseEvent(table);
	deleteCourseEvent(table);

	//group select checkbox 
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

	//individual select checkbox
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





