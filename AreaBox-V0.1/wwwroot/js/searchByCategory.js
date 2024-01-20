$("#ApplyBtn").off('click').on('click', function () {
    var categoryId = $('#categoryDropdown').val();

    // Reset scroll to the default position
    window.scrollTo(0, 0);

    getPageCount(categoryId);

    // Perform additional actions or trigger a function (e.g., loadMorePosts())
    $('#post-container').empty();
    page = 1;
    loadMorePosts();
});
