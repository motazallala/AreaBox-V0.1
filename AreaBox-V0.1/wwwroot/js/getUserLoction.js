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

function SetLocationName() {
    $.ajax({
        url: '/UserApi/GetUserLocationByGeolocation',
        type: 'GET',
        success: function (response) {
            // Assuming the response has a property named 'country' and 'city'
            $('#countryText').val(response.country);
            $('#cityText').val(response.city);
        },
        error: function (error) {
            alert('Error fetching user location:', error);
        }
    });

}