$(document).ready(function () {
    viewCourses();

});

function viewCourses() {

}

$(function () {
    (function (name) {
        var container = $('#pagination-' + name);
        var sources = function () {
            var result = [];

            for (var i = 1; i < 196; i++) {
                result.push(i);
            }

            return result;
        }();

        var options = {
            dataSource: sources,
            pageSize: 5,
            callback: function (response, pagination) {
                window.console && console.log(response, pagination);

                var dataHtml = '';

                $.each(response, function (index, item) {
                    dataHtml += '<div class="row align-items-center"><div class="col">' + `<div class="card mb-3" style="max-width: 540px;">
                        <div class="row g-0">
                            <div class="col-md-4">
                                <img src="..." class="img-fluid rounded-start" alt="...">
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <h5 class="card-title">Card title</h5>
                                    <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
                                    <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
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
    })('demo1');

    (function (name) {
        var container = $('#pagination-' + name);
        container.pagination({
            dataSource: 'https://api.flickr.com/services/feeds/photos_public.gne?tags=cat&tagmode=any&format=json&jsoncallback=?',
            locator: 'items',
            totalNumber: 120,
            pageSize: 20,
            ajax: {
                beforeSend: function () {
                    container.prev().html('Loading data from flickr.com ...');
                }
            },
            callback: function (response, pagination) {
                window.console && console.log(22, response, pagination);
                var dataHtml = '<ul>';

                $.each(response, function (index, item) {
                    dataHtml += '<li>' + item.title + '</li>';
                });

                dataHtml += '</ul>';

                container.prev().html(dataHtml);
            }
        })
    })('demo2');
})