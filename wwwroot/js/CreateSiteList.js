$(document).ready(function () {
    var currentPage = 1; // Initialize the current page

    function updatePagination(currentPage, totalPages) {
        var paginationHtml = '<ul class="pagination">';

        for (var i = 1; i <= totalPages; i++) {
            paginationHtml += '<li class="page-item' + (i === currentPage ? ' active' : '') + '">';
            paginationHtml += '<a class="page-link" href="#" data-page="' + i + '">' + i + '</a>';
            paginationHtml += '</li>';
        }

        paginationHtml += '</ul>';

        $('#pagination').html(paginationHtml);

        // Handle click events on pagination links
        $('#pagination').on('click', 'a.page-link', function (e) {
            e.preventDefault();
            var newPage = parseInt($(this).data('page'));
            if (newPage !== currentPage) {
                currentPage = newPage;
                loadData(currentPage); // Call the loadData function when a page link is clicked
            }
        });
    }

    // Define the loadData function to load data for a specific page
    function loadData(page) {
        // Get the search term from the input field

        $.ajax({
            url: '/SiteInstruction/GetData', // Make sure 'urlsite' is defined
            method: "GET",
            data: {
                name: name, // Make sure 'name' is defined
                page: page // Pass the current page
            },
            success: function (result) {
                console.log(result);

                $('#tableBody').empty();
                var pl = '';

                $.each(result.data, (k, v) => {
                    // Construct the table row HTML here
                    pl += `<tr>
                                <td>${v.ProjectName}</td>
                                <td>${v.CMPMINumber}</td>
                                <td>${v.AsecPMINumber}</td>
                                <td>${v.Remarks}</td>
                                <td>${v.Status}</td>
                                <td>${new Date(v.Date).toISOString().slice(0, 10)}</td>
                            </tr>`;
                });

                $('#tableBody').html(pl);
                updatePagination(result.pageCurrent, result.numSize);
            },
            error: function (xhr, status, error) {
                console.error("An error occurred while searching:", error);
                // You can display a more informative error message to the user here.
            }
        });
    }

    // Initial data load (you might want to call this when the page loads)
    loadData(currentPage);
});