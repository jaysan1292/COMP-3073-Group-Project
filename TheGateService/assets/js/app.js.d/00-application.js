(function ($) {
    $(document).ready(function () {
        // Register global variables

        // Get the root of the site, but remove the trailing slash, if any.
        window.site_root = ($('head').data('site-root')).replace(/\/$/, '');

        $('#navbar-search .search-query').typeahead({
            source: function (query, process) {
                return $.get('/typeahead', { q: query }, function (data) {
                    console.log(JSON.stringify(data));
                    var results = data.results.map(function (item) {
                        return JSON.stringify({ name: item.name, url: item.url });
                    });
                    console.log(results);
                    return process(results);
                });
            },
            matcher: function (obj) {
                var item = JSON.parse(obj);
                return ~item.name.toLowerCase().indexOf(this.query.toLowerCase());
            },
            sorter: function (items) {
                var beginswith = [], caseSensitive = [], caseInsensitive = [], item, aItem;
                while (aItem = items.shift()) {
                    item = JSON.parse(aItem);
                    if (!item.name.toLowerCase().indexOf(this.query.toLowerCase)) beginswith.push(JSON.stringify(item));
                    else if (~item.name.indexOf(this.query)) caseSensitive.push(JSON.stringify(item));
                    else caseInsensitive.push(JSON.stringify(item));
                }
                return beginswith.concat(caseSensitive, caseInsensitive);
            },
            highlighter: function (obj) {
                // Bold all search query matches
                var item = JSON.parse(obj);
                var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&');
                return item.name.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
                    return '<b>' + match + '</b>';
                });
            },
            updater: function (obj) {
                // When an item is selected in the dropdown, navigate to that item's product page.
                var item = JSON.parse(obj);
                window.location.href = item.url;
                return item.name;
            }
        });
    });
})(window.jQuery);
