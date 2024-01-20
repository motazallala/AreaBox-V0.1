$(document).ready(function () {
    SetLocationName();
});
$("#refreshLoc").click(function () {
    // Check if geolocation is supported by the browser
    if (navigator.geolocation) {
        // Get the user's current position
        navigator.geolocation.getCurrentPosition(function (position) {
            const latitude = position.coords.latitude;
            const longitude = position.coords.longitude;

            // Save latitude and longitude in the cookie
            document.cookie = `latitude=${latitude}; path=/`;
            document.cookie = `longitude=${longitude}; path=/`;
            SetLocationName();
            var categoryId = $('#categoryDropdown').val();
            getPageCount(categoryId);

            // Perform additional actions or trigger a function (e.g., loadMorePosts())
            $('#post-container').empty();
            page = 1;
            loadMorePosts();
        }, function (error) {
            console.error('Error getting geolocation:', error);
        });
    } else {
        console.error('Geolocation is not supported by this browser.');
    }
});



function getPageCount(categoryId) {
    $.ajax({
        url: '/UserApi/GetPageQuestionPostCount',
        type: 'GET',
        data: { categoryId: categoryId },
        success: function (pagesCount) {
            // Handle the pagesCount value (you can use it as needed)
            Pages = pagesCount;
            console.log('Pages Count:', pagesCount);
        },
        error: function () {
            console.log('Error fetching Pages Count.');
            // Handle the error if needed
        }
    });
}