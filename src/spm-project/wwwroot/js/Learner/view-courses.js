function view_classes(course_id) {
    var RetrieveCoursesClasses = $("#get-course-classes").val() + "?courseId=" + course_id;
    $.ajax({
        url: RetrieveCoursesClasses, success: function (result) {
            console.log(result)
            var dataHtml = ``;
            $.each(result.data, function (index, item) {
                var className = item.name
                var trainerName = item.trainerName
                var startClass = ($.format.date(item.startClass, "dd-MMM-yyyy"))
                var startRegistration = ($.format.date(item.startRegistration, "dd-MMM-yyyy"))
                var endClass = ($.format.date(item.endClass, "dd-MMM-yyyy"))
                var endRegistration = ($.format.date(item.endRegistration, "dd-MMM-yyyy"))

                var slots = item.slots

                dataHtml += '<div class="row align-items-center"><div class="col">' + `<div class="card mb-3" style="max-width: 540px; background-color: fefaf4;">
                        <div class="row g-0">
                            <div class="col">
                                <div class="card-body">
                                    <h5 class="card-title">${className}</h5>
                                    <p class="card-text">Trainer: ${trainerName}</p>
                                    <p class="card-text">Class Duration: ${startClass} - ${endClass}</p>
                                    <p class="card-text">Registration Period: ${startRegistration} - ${endRegistration}</p>

                                    <p class="card-text">Slots Available: ${slots}</p>


                                    <p class="card-text"><small class="text-muted"></small></p>
                                    <div class="row">
                                        <div class="col"></div>
                                        <div class="col-6">
                                            <button type="button" value="course_id" onclick=""; class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                              Sign up for this class
                                            </button>
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

        }
    });


}

$(function () {
    (function (name) {
        var RetrieveCourses = $("#get-eligible-courses").val();
        var container = $('#pagination-' + name);

        var options = {
            dataSource: RetrieveCourses,
            locator: 'items',
            pageSize: 5,
            callback: function (response, pagination) {
               console.log(response, pagination);

                var dataHtml = '';

                $.each(response, function (index, item) {
                    dataHtml += '<div class="row align-items-center"><div class="col">' + `<div class="card mb-3" style="max-width: 540px;">
                        <div class="row g-0">
                            <div class="col-md-4">
                                <img src="..." class="img-fluid rounded-start" alt="...">
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <h5 class="card-title">${item.Name}</h5>
                                    <p class="card-text">${item.Description}</p>
                                    <p class="card-text"><small class="text-muted"></small></p>
                                    <button type="button" value="course_id" onclick="view_classes(${item.Id})"; class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                      View Classes Available
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>` + '</div></div>';
                });

                dataHtml += '';

                container.prev().html(dataHtml);
            }
        };

        //$.pagination(container, options);

        container.addHook('beforeInit', function () {
            window.console && console.log('beforeInit...');
        });
        container.pagination(options);

        container.addHook('beforePageOnClick', function () {
            window.console && console.log('beforePageOnClick...');
            //return false
        });
    })('view-courses');
})