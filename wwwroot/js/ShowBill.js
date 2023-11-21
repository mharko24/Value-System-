$(document).ready(function () {
    loadData(currentPage);
    var connection = new signalR.HubConnectionBuilder().withUrl("/siteHub").build();
    var urlsite = '/BillingAccomplishment/GetData'
    var extraData = false;
    var currentPage = 1; // Initialize the current page
    var checkSearch = false;
    //var totalPages = 3;
    connection.start().then(function () {
        connection.on("LoadBillData", function () {
            loadData(currentPage);
        });
        loadData(currentPage);
    }).catch(function (error) {
        loadData(currentPage);
        // This catch block will handle errors that occur during connection start.
        console.error("Connection start failed:", error);

    });
    loadData(currentPage);

    //$("#txtSearch").keyup("input", function () {
    //    var searchText = $(this).val().trim(); 

    //    if (searchText === "") {
    //        currentPage = 1; 
    //        loadData(currentPage);
    //    } else {

    //        urlsite = '/BillingAccomplishment/Search';
    //        loadData(currentPage); 
    //    }
    //});

    $("#btnSearch").click(function (e) {
        currentPage = 1; // Reset the current page when searching
        urlsite = '/BillingAccomplishment/Search'
        loadData(currentPage); // Load data for the first page when searching
         checkSearch = true;

    });
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
        console.log(extraData)
        var name = $("#txtSearch").val(); // Get the search term from the input field

        $.ajax({
            url: urlsite,
            method: "GET",
            data: {
                name: name,
                page: page // Pass the current page
            },
            success: function (result) {
                //if (result.data == null) {
                //    $(document).ready(function () {
                //        alert("No Record Found");
                //    });
                //    $('#tableBody').html('<tr><td colspan="7">No file match</td></tr>');
                //}
                if (checkSearch = true) {
                    if (result.data == 0) {
                        $(document).ready(function () {
                            alert("No Record Found");
                        });
                        urlsite = "/BillingAccomplishment/GetData";
                        loadData(currentPage);
                    }
                }
                
                console.log(result);
                //$('#tableBody').empty();
                if (result.length === 0) {
                    // No results, display "No file match"
                    $('#tableBody').html('<tr><td colspan="7">No file match</td></tr>');
                } else {
                    $('#tableBody').empty();
                    var pl = '';

                    $.each(result.data, (k, v) => {
                        //<td>${ new Date(v.CoverPeriod).toISOString().slice(0, 10) } </td>
                        // Construct the table row HTML here
                        pl += `<tr>
    @if (User.IsInRole("Admin") || User.IsInRole("Cost") || User.IsInRole("Superindent/Manager") || User.IsInRole("Engineering") || User.IsInRole("Audit") || User.IsInRole("LYE") || User.IsInRole("MTYE"))
    {
                                                        <td>${v.BillingNo}</td>

                                                                        <td>${v.CoverPeriod}</td>
                                                                <td>${v.PONumber}</td>
                                                                <td>${v.ContractType}</td>
                                                                <td>${v.NetAmount}</td>
                                                                        <td>${v.Status}</td>
                                                        
    }





                                                    <td>


            <div id="CrudIcon" class="mt-2 fa-xs fa-sm fa-lg">
    @if (User.IsInRole("Admin") || User.IsInRole("Cost") || User.IsInRole("Superindent/Manager") || User.IsInRole("Engineering"))
    {
                        <a href='../SiteInstruction/Details?id=${v.CMSiteId}'>
                            <i title="Details" class='far fa-file-alt'></i>
                        </a>

                        <a href='../SiteInstruction/Edit?id=${v.CMSiteId}' >
                            <i title="Update" class='fa-sharp fa-solid fa-file-pen'></i>
                        </a>

        @if (User.IsInRole("Admin"))
        {
                                    <a  href='../SiteInstruction/Delete?id=${v.CMSiteId}' onclick="return doClick();">

                                            <i title="Delete" class='icon fa-regular fa-trash-can'></i>
                                        </a>
        }

    }

            </div>

                                                    </td>
                                        </tr>`;
                    });

                    $('#tableBody').html(pl);
                    updatePagination(result.pageCurrent, result.numSize);
                }
            },
            error: function () {
                loadData(currentPage);
                console.log("An error occurred while searching.");
            }
        });

    }
    $(document).on('click', 'a[href^="../SiteInstruction/Delete?id="]', function (e) {
        //$(document).on('click', 'GoDelete', function (e) {
        return window.confirm('Are you sure you want to delete this file?');
    });

});