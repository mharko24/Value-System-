//$(() => {
//    var urlsite = '/SiteInstruction/GetData'
//    var currentPage = 1; // Initialize the current page
//    //var totalPages = 3;
//    loadData(currentPage);
//    $("#btnSearch").click(function (e) {
//        currentPage = 1; // Reset the current page when searching
//        loadData(currentPage); // Load data for the first page when searching
//        urlsite = '/SiteInstruction/Search'
//    });
//    var connection = new signalR.HubConnectionBuilder().withUrl("/siteHub").build();
//    connection.start().then(function () {
//        console.log('Connected to siteHub');

//        connection.on("LoadSiteData", function () {
//            loadData(page);
//        });

//        loadData(page);

//        function loadData(page) {
//            var name = $("#txtSearch").val(); // Get the search term from the input field

//            $.ajax({
//                url: urlsite,
//                method: "GET",
//                data: {
//                    name: name,
//                    page: page // Pass the current page
//                },
//                success: function (result) {
//                    console.log(result);
//                    //$('#tableBody').empty();
//                    if (result.length === 0) {
//                        // No results, display "No file match"
//                        $('#tableBody').html('<tr><td colspan="7">No file match</td></tr>');
//                    } else {
//                        $('#tableBody').empty();
//                        var pl = '';
//                        //<td>${new Date(v.Date).toISOString().slice(0, 10)}</td>
//                        $.each(result.data, (k, v) => {
//                            // Construct the table row HTML here
//                            pl += `<tr>
//                                    <td>${v.ProjectName}</td>
//                                    <td>${v.CMPMINumber}</td>
//                                    <td>${v.AsecPMINumber}</td>
//                                    <td>${v.Remarks}</td>
//                                    <td>${v.Status}</td>
//                                      <td>${v.Date}</td>  
//                                    <td>
//                                        <div id="CrudIcon" class="mt-2 fa-xs fa-sm fa-lg">
//                                            <a href='../SiteInstruction/Details?id=${v.CMSiteId}'>
//                                                <i title="Details" class='far fa-file-alt'></i>
//                                            </a>
//                                            <a href='../SiteInstruction/Edit?id=${v.CMSiteId}' onclick="return doClick();">
//                                                <i title="Update" class='fa-sharp fa-solid fa-file-pen'></i>
//                                            </a>
//                                            <a id="GoDelete" href='../SiteInstruction/DeleteSite?id=${v.CMSiteId}' onclick="return doClick();">
//                                                <i id="GoDelete" title="Delete" class='icon fa-regular fa-trash-can'></i>
//                                            </a>
//                                        </div>
//                                    </td>
//                                </tr>`;
//                        });

//                        $('#tableBody').html(pl);
//                        updatePagination(result.pageCurrent, result.numSize);
//                    }
//                },

//                error: function () {
//                    alert("An error occurred while searching.");
//                }
//            });
//        }
//        $(document).on('click', 'a[href^="../SiteInstruction/DeleteSite?id="]', function (e) {
//            /*     $(document).on('click', 'GoDelete', function (e) {*/
//            return window.confirm('Are you sure you want to delete this file?');
//        })
//    });
//    $(document).ready(function () {

//        //if (!userHasPermission) {
//        //    $("#GoDelete").prop("disabled", true); // Disable the button
//        //    // OR
//        //    $("#GoDelete").hide(); // Hide the button
//        //}
//    });
//    function updatePagination(currentPage, totalPages) {
//        var paginationHtml = '<ul class="pagination">';

//        for (var i = 1; i <= totalPages; i++) {
//            paginationHtml += '<li class="page-item' + (i === currentPage ? ' active' : '') + '">';
//            paginationHtml += '<a class="page-link" href="#" data-page="' + i + '">' + i + '</a>';
//            paginationHtml += '</li>';
//        }

//        paginationHtml += '</ul>';

//        $('#pagination').html(paginationHtml);

//        // Handle click events on pagination links
//        $('#pagination').on('click', 'a.page-link', function (e) {
//            e.preventDefault();
//            var newPage = parseInt($(this).data('page'));
//            if (newPage !== currentPage) {
//                currentPage = newPage;
//                loadData(currentPage); // Call the loadData function when a page link is clicked
//            }
//        });
//    }

//});
