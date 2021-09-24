$(function () {

	var retreiveCustomers = $("#retreive-customers").val(); 

	console.log(retreiveCustomers)

	var table = $('#kt_datatable').DataTable({

		//width of column siwll be auto 
		"autoWidth": false,



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
			url: retreiveCustomers,
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
			{ name: 'Checkbox',data: null, orderable: false },
			{ name:'Id', data: 'Id' },
			{ name: 'FirstName',data: 'FirstName' },
			{ name: 'LastName', data: 'LastName' },
			{ name: 'Contact' , data: 'Contact' },
			{ name: 'Email',data: 'Email' },
			{ name: 'DateOfBirth',data: 'DateOfBirth' },
			//responsive priority is an option to state the priority of the column to be view when the screen is smaller
			//data: null means it is not retreiveing data from the server
			{ name: 'Actions', data: null, responsivePriority: -1, orderable: false},
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
					return '\
							<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" title="Delete" id="delete-btn">\
								<i class="fas fa-trash"></i>\
							</a>\
						';
				},
			},

			/*code snippet to render bootstrap badges for status */

			//{
			//	width: '75px',
			//	targets: 8,
			//	render: function (data, type, full, meta) {
			//		var status = {
			//			1: { 'title': 'Pending', 'class': 'label-light-primary' },
			//			2: { 'title': 'Delivered', 'class': ' label-light-danger' },
			//			3: { 'title': 'Canceled', 'class': ' label-light-primary' },
			//			4: { 'title': 'Success', 'class': ' label-light-success' },
			//			5: { 'title': 'Info', 'class': ' label-light-info' },
			//			6: { 'title': 'Danger', 'class': ' label-light-danger' },
			//			7: { 'title': 'Warning', 'class': ' label-light-warning' },
			//		};
			//		if (typeof status[data] === 'undefined') {
			//			return data;
			//		}
			//		return '<span class="label label-lg font-weight-bold' + status[data].class + ' label-inline">' + status[data].title + '</span>';
			//	},
			//},


			/*code snippet to render buttons for status */

			//{
			//	targets: -1,
			//	title: 'Actions',
			//	orderable: false,
			//	render: function (data, type, full, meta) {
			//		return '\
			//				<div class="dropdown dropdown-inline">\
			//					<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" data-toggle="dropdown">\
	  //                              <i class="la la-cog"></i>\
	  //                          </a>\
			//				  	<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right">\
			//						<ul class="nav nav-hoverable flex-column">\
			//				    		<li class="nav-item"><a class="nav-link" href="#"><i class="nav-icon la la-edit"></i><span class="nav-text">Edit Details</span></a></li>\
			//				    		<li class="nav-item"><a class="nav-link" href="#"><i class="nav-icon la la-leaf"></i><span class="nav-text">Update Status</span></a></li>\
			//				    		<li class="nav-item"><a class="nav-link" href="#"><i class="nav-icon la la-print"></i><span class="nav-text">Print</span></a></li>\
			//						</ul>\
			//				  	</div>\
			//				</div>\
			//				<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" title="Edit details">\
			//					<i class="la la-edit"></i>\
			//				</a>\
			//				<a href="javascript:;" class="btn btn-sm btn-clean btn-icon" title="Delete">\
			//					<i class="la la-trash"></i>\
			//				</a>\
			//			';
			//	},
			//},

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


	//polling the database every 30s for data (fyi only , pretty stupid to use it here )
	//can use the ajax reload everytime you delete a row or search something

	//setInterval(function () {
	//	table.ajax.reload(null, false); // user paging is not reset on reload
	//}, 30000);



	table.on('change', '.group-checkable', function () {
		var set = $(this).closest('table').find('td:first-child .checkable');
		var checked = $(this).is(':checked');

		$(set).each(function () {
			if (checked) {
				$(this).prop('checked', true);
				table.rows($(this).closest('tr')).select();
				console.log(90)
			}
			else {
				$(this).prop('checked', false);
				table.rows($(this).closest('tr')).deselect();
			}
		});
	});
});