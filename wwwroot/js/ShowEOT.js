$(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("/siteHub").build();
    connection.start().then(function () {
        console.log('Connected to siteHub');
        
        connection.on("LoadEOTData", function () {
            LoadPost();
        });
        LoadPost();

        function LoadPost() {
            console.log("Start na tayo");
            var pl = '';

            $.ajax({
                url: '/EOTClaim/GetData',
                method: 'GET',
               
                success: (result) => {
                    console.log(result);
                    $.each(result, (k, v) => {
                        pl += `<tr>                          
                        <td>${v.ProjectName}</td>
                       <td>${v.PMINumber}</td>
                       <td>${v.VONumber}</td>
                       <td>${v.PONumber}</td>
                       <td>${v.Remarks}</td>
                       <td>${v.Status}</td>
                      <td>${new Date(v.Date).toISOString().slice(0, 10)}</td>
                        <td>
                                <div id="CrudIcon" class="">
                                   
                                        
                                         <a href='../EOTClaim/Details?id=${v.EOTId}'>
                                        <i title="Details" class='far fa-file-alt'></i>
                                        </a>
                                        <a href='../EOTClaim/Edit?id=${v.EOTId}'onclick="return doClick(); >
                                        <i title="Update" class='fa-sharp fa-solid fa-file-pen'></i>
                                        </a>
                                        <a href='../EOTClaim/Delete?id=${v.EOTId}' onclick="return doClick(); >
                                        <i title="Delete" class='icon fa-regular fa-trash-can' ></i>
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
            $(document).on('click', 'a[href^="../EOTClaim/Delete?id="]', function (e) {
                return window.confirm('Are you sure you want to delete this file?');
            });
        }
        
    });
});
