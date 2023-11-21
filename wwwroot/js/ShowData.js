$(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("/siteHub").build();
    connection.start().then(function () {
        console.log('Connected to siteHub');
        
        connection.on("LoadData", function () {
            LoadPost();
        });
        LoadPost();

        function LoadPost() {
            console.log("Start na tayo");
            var pl = '';

            $.ajax({
                url: '/PotentialClaim/GetData',
                method: 'GET',
               
                success: (result) => {
                    console.log(result);
                    $.each(result, (k, v) => {
                        pl += `<tr>                          
                        <td>${v.ProjectName}</td>
                       <td>${v.CVINumber}</td>
                       <td>${v.AsecPMINumber}</td>
                       <td>${v.Remarks}</td>
                       <td>${v.Status}</td>
                       <td>${new Date(v.Date).toISOString().slice(0, 10)}</td>
                        <td>
                                <div id="CrudIcon" class="">
                                   
                                        
                                         <a href='../PotentialClaim/Details?id=${v.PotId}'>
                                        <i title="Details" class='far fa-file-alt'></i>
                                        </a>
                                        <a href='../PotentialClaim/Edit?id=${v.PotId}' onclick="return doClick();>
                                        <i title="Update" class='fa-sharp fa-solid fa-file-pen' ></i>
                                        </a>
                                        <a href='../PotentialClaim/Delete?id=${v.PotId}' onclick="return doClick();>
                                        <i title="Delete" class='icon fa-regular fa-trash-can'></i>
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
            $(document).on('click', 'a[href^="../PotentialClaim/Delete?id="]', function (e) {
                return window.confirm('Are you sure you want to delete this file?');
            });
        }
        
    });
});
