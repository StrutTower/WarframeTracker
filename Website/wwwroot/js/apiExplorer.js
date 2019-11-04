$(document).ready(function () {

    var apiExplorer = $("#api-explorer");
    if (apiExplorer.length) {
        var apiData;
        $.getJSON('https://raw.githubusercontent.com/WFCD/warframe-items/development/data/json/All.json', function (data) {
            apiData = data;

            const result = [];
            const map = new Map();
            for (const item of apiData) {
                if (!map.has(item.category)) {
                    map.set(item.category, true);
                    result.push(item.category);
                }
            }
            result.sort();

            var select = document.getElementById('category-select');
            var defaultOption = document.createElement('option');
            defaultOption.textContent = 'All Categories';
            defaultOption.value = '';
            select.appendChild(defaultOption);

            for (var i = 0; i < result.length; i++) {
                var option = result[i];
                var el = document.createElement('option');
                el.textContent = option;
                el.value = option;
                select.appendChild(el);
            }
        });

        $('#name-search-form').on('submit', function (e) {
            e.preventDefault();
            var searchInput = $('#name-search-input').val();
            var category = $('#category-select').val();
            var searchCount = 0;
            var results = [];
            $.each(apiData, function (i, v) {
                if (category === '') {
                    searchCount++;
                } else {
                    if (v.category === category) {
                        searchCount++;
                    } else {
                        return;
                    }
                }

                if (v.name.toLowerCase().indexOf(searchInput.toLowerCase()) > -1) {
                    results.push(v);
                }
            });

            renderApiResults(results, searchCount);
        });
    }

    function renderApiResults(results, searchCount) {
        $('#api-explorer-count').text('Matched ' + results.length + ' out of ' + searchCount);

        var tbody = document.getElementById('api-explorer-results');
        tbody.innerHTML = '';
        $.each(results, function (i, v) {
            var row = tbody.insertRow();

            var cell1 = row.insertCell(0);
            cell1.textContent = v.name;

            var cell2 = row.insertCell(1);
            cell2.textContent = v.category;

            var cell3 = row.insertCell(2);
            cell3.textContent = v.uniqueName;
        });
    }
});