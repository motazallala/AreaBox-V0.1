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