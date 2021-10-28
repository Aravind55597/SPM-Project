$(document).ready(function () {
	viewRequestDT("No Filter");
	filterHandler();
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
		globalPosition: 'top center'
	});
}



function filterHandler() {
	$('#dropdownFilter').on('change', function () {
		var filterInput = this.value;
		//destroy DT
		$('#request_datatable').DataTable().clear().destroy();
		//remove previous event listeners
		$('#request_datatable').off();
		//initialize DT with filter
		viewRequestDT(filterInput);
	});
}



function ApproveRejectHandler(table, action) {
	var buttonName = null;
	var message = null;
	var failMsg = null;
	var query = null;


	if (action == "approve") {
		buttonName = ".ApproveRequestBtn";
		message = "Successfully Approved Request";
		failMsg = "Failed to Approve Request";

	}
	else if (action == "reject") {
		buttonName = ".RejectRequestBtn"
		message = "Successfully Rejected Request";
		failMsg = "Failed to Reject Request";
	}


	table.on('click', buttonName, function () {

		var row = $(this).parents('tr')[0];
		//for row data
		var row_data = table.row(row).data();

		console.log(row_data)

		
		$.ajax({
			url: query,
			method: "POST",
			success: function (data) {
				table.ajax.reload();
				notification(message, "success");
			},
			error: function (data) {
				notification(failMsg, "failed");
			},
			async: false
		});
	
		
	});

}


function viewRequestDT(filterInput) {
	var filterValue = null;

	if (filterInput =="No Filter") {
		filterValue = [];
	}
	else if (filterInput == "Enrolled"){
		filterValue = [{ column: "RecordStatus", value: "Enrolled" }]
	}
	else if (filterInput == "RequestedEnrollment") {
		filterValue = [{ column: "RecordStatus", value: "RequestedEnrollment" }]
	}

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
				d.filter = filterValue;
				console.log(JSON.stringify(d));
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
					//only render approve button to requests with status RequestedEnrollment
					if (data.RecordStatus == "RequestedEnrollment") {
						return `<a href="javascript:;" class="btn btn-success ApproveRequestBtn">Approve</a>
								<a href="javascript:;" class="btn btn-danger RejectRequestBtn">Reject</a>
								`;
					}
					else {
						return null;
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

	ApproveRejectHandler(table, "approve");
	ApproveRejectHandler(table, "reject");

}







