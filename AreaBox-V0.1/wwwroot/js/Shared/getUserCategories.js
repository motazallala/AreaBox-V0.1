$(document).ready(function () {
    $.ajax({
        url: '/UserApi/GetAllUserCategories',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var categoryDropdownFilter = $('#categoryDropdown');
            categoryDropdownFilter.append($('<option></option>').attr('value', 0).text('All'));
            $.each(data, function (key, entry) {
                categoryDropdownFilter.append($('<option></option>').attr('value', entry.categoryId).text(entry.categoryName));
            });
        },
        error: function (error) {
            console.error('Error fetching category data:', error);
        }
    });
});