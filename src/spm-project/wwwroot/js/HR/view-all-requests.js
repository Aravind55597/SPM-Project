﻿$(document).ready(function () {
	viewRequestDT();
	
});



function filterHandler(table) {
	$('#dropdownFilter').on('change', function () {
		table.column(5).search(this.value).draw()
	});

}



function viewRequestDT() {


	var RetrieveRequest = $("#get-class-enrollment-records-datatable").val();

	var table = $('#request_datatable').DataTable({

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

			//spinner if DT is loading
			/*
			processing: `<div class="spinner-border" role="status">
							<span class="sr-only">Loading...</span>
						</div>`
			*/
		},

		//enable server side 
		serverSide: true,



		//send ajax request to server to Retrieve customers
		ajax: {
			url: RetrieveRequest,
			type: "POST",
			contentType: "application/json",
			dataType: "json",
			data: function (d) {
				//d.filter = [{ column: "CourseClassName", value: "Course 1 G2" }]
				console.log(JSON.stringify(d));
				return JSON.stringify(d);
			},
			error: function (xhr, error, code) {
				console.log(xhr);
				console.log(code);
			}
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
			{ name: 'Id', data: 'Id' },
			{ name: 'DateTimeRequested', data: 'DateTimeRequested' },
			{ name: 'UserId', data: 'UserId' },
			{ name: 'LearnerName', data: 'LearnerName' },
			{ name: 'CourseClassName', data: 'CourseClassName' },
			{ name: 'RecordStatus', data: 'RecordStatus' },
			//responsive priority is an option to state the priority of the column to be view when the screen is smaller
			//data: null means it is not Retrieveing data from the server
			{ name: 'Actions', data: null, responsivePriority: -1, orderable: false },
		],

		//define the properties of each column (very similar function as columns option above.Don't need to define all the column )
		//I suggest to use this just to render stuff such as buttons/any elements OR processign the result to display in diff format eg. format date string
		columnDefs: [

			{
				targets: 1,
				render: function (data, type, full, meta) {

					return moment(data).format('Do MMMM YYYY, h:mm a')
						;

				},
			},

			{
				//target last column
				targets: -1,
				render: function (data, type, full, meta) {
					return `<a href="javascript:;" class="btn btn-primary" title="AssignToClass" id="AssignClassbtn">Assign to Class</a>`
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

	
	filterHandler(table);

}



