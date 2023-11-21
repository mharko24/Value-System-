$(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("/siteHub").build();
    connection.start().then(function () {
        console.log('Connected to siteHub');

        connection.on("LoadSiteData", function () {
            LoadSite();
        });

        LoadSite();

        function LoadSite() {
           
            console.log("Start na tayo");
            var pl = '';

            $.ajax({
                url: '/SiteInstruction/GetData',
                method: 'GET',

                success: (result) => {
                    
                    console.log(result);
                    $.each(result, (k, v) => {
                        //v.Date = Date
                        //$("#CrudIcon").hide();
                        pl += `<tr>                          
                        <td>${v.ProjectName}</td>
                       <td>${v.CMPMINumber}</td>
                       <td>${v.AsecPMINumber}</td>
                       <td>${v.Remarks}</td>
                       <td>${v.Status}</td>
                       <td>${new Date(v.Date).toISOString().slice(0, 10)}</td>


                       
                        <td>
                                <div id="CrudIcon" class="mt-2 fa-xs fa-sm fa-lg">
                                         <a href='../SiteInstruction/Details?id=${v.CMSiteId}'>
                                        <i title="Details" class='far fa-file-alt'></i>
                                        </a>
                                        <a href='../SiteInstruction/Edit?id=${v.CMSiteId}' onclick="return doClick(); >
                                        <i title="Update" class='fa-sharp fa-solid fa-file-pen'></i>
                                        </a>
                                       
                                           <a id="GoDelete" href='../SiteInstruction/DeleteSite?id=${v.CMSiteId}' onclick="return doClick(); >
                                        <i id="GoDelete" title="Delete" class='icon fa-regular fa-trash-can'></i>
                                        </a
                                        
                                </div>
                            </td>
                        </tr>`;
                    });

                    $("#tableBody").html(pl);
                },
                error: (error) => {
                    console.error("Error fetching data: ", error);
                    // Handle errors gracefully, e.g., display an error message to the user.
                }
            });
           
            
            
              $(document).on('click', 'a[href^="../SiteInstruction/DeleteSite?id="]', function (e) {
       /*     $(document).on('click', 'GoDelete', function (e) {*/
                return window.confirm('Are you sure you want to delete this file?');
            });
        }
    });
    $(document).ready(function () {
        // Assuming you have jQuery loaded

        // Disable or hide the button based on the user's role
        if (!userHasPermission) {
            $("#GoDelete").prop("disabled", true); // Disable the button
            // OR
            $("#GoDelete").hide(); // Hide the button
        }
    });

});
