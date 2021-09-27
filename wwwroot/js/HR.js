
$(document).ready(function () {
	viewEngineerDT();
	//viewClassDT();

});





function viewEngineerDT() {

	var retreiveEngineers = $("#get-engineers-datatable").val();

	var table = $('#engineer_datatable').DataTable({

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

		//send ajax request to server to retreive customers
		ajax: {
			url: retreiveEngineers,
			type: 'POST',
			//modify the json sent to server . 
			data: function (d) {
				//add properties if needed. Tell me if yall are adding it 
				return d
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
			//data: null means it is not retreiveing data from the server
			//column can't be ordered
			//regarding name (https://datatables.net/reference/option/columns.name)
			{ name: 'Checkbox', data: null, orderable: false },
			{ name: 'Id', data: 'Id' },
			{ name: 'Name', data: 'Name' },
			{ name: 'Role', data: 'Role' },
			//responsive priority is an option to state the priority of the column to be view when the screen is smaller
			//data: null means it is not retreiveing data from the server
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
		//retreive DT_RowData from data and add the object to row using jquery (read the serversiderendering of datatables )
		createdRow: function (row, data, dataIndex) {

			//i am sending the data for the row again just for testing purposes (just id , firstname , lastname)
			$(row).data(data.DT_RowData);

			//can use DT_RowAttr for toher stuff (check https://datatables.net/manual/server-side)
		},



	});

	//check box 
	table.on('change', '.group-checkable', function () {


		var set = $(this).closest('table').find('td:first-child .checkable');
		var checked = $(this).is(':checked');



		$(set).each(function () {
			if (checked) {
				$(this).prop('checked', true);
				table.rows($(this).closest('tr')).select();
				

			}
			else {
				$(this).prop('checked', false);
				table.rows($(this).closest('tr')).deselect();
			}
		});
	});


}



function viewClassDT() {

	var retreiveEngineers = $(".get-engineers-datatable").val();


	var table = $('#class_datatable').DataTable({

		//width of column siwll be auto 
		"autoWidth":false,

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

		//enable select in the table 
		select: {
			//allow us to select multiple rows
			style: 'multi',
			//retricts which cells in the table that will trigger table selection 
			//td first child (for each td tag , only the first item (cell) will allow selection. Within the cell , the element with .checkable class is only allowed)
			//this is essentially a css selector used here 
			selector: 'td:first-child .checkable',
		},

		//send ajax request to server to retreive customers
		ajax: {
			url: retreiveEngineers,
			type: 'POST',
			//modify the json sent to server . 
			data: function (d) {
				//add properties if needed. Tell me if yall are adding it 
				return d
			}
		},



		//default order and sort. In this case ,order by ID in ascending order (Id is column number 1)
		order: [[1, "asc"]],

		//define the properties of each column (This takes precedence, need to define for each column)
		//use this to state the array key 
		//data table will receive an array of objects where each object is for each row
		//the key for the object corresponds to the columns
		columns: [
			//data: null means it is not retreiveing data from the server
			//column can't be ordered
			//regarding name (https://datatables.net/reference/option/columns.name)
			{ name: 'Id', data: 'Id' },
			{ name: 'FirstName', data: 'FirstName' },
			//responsive priority is an option to state the priority of the column to be view when the screen is smaller
			//data: null means it is not retreiveing data from the server
			{ name: 'Actions', data: null, responsivePriority: -1, orderable: false },
		],

		//define the properties of each column (very similar function as columns option above.Don't need to define all the column )
		//I suggest to use this just to render stuff such as buttons/any elements OR processign the result to display in diff format eg. format date string
		columnDefs: [
			{
				//target last column
				targets: -1,
				render: function (data, type, full, meta) {
					return `<a href="javascript:;" class="btn btn-primary" title="ViewClassList" id="ViewClassbtn">View Class List</a>`
						;
				},
			},


		],


		//https://datatables.net/reference/option/createdRow
		// add events, class name information or otherwise format the row when it is created
		//retreive DT_RowData from data and add the object to row using jquery (read the serversiderendering of datatables )
		createdRow: function (row, data, dataIndex) {

			//i am sending the data for the row again just for testing purposes (just id , firstname , lastname)
			$(row).data(data.DT_RowData);

			//can use DT_RowAttr for toher stuff (check https://datatables.net/manual/server-side)
		},

	});
}

