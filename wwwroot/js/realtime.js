$(() => {
    $(document).ready(function () {
       /* fetchDataFromServer();*/
        // Initialize the SignalR connection
        var connection = new signalR.HubConnectionBuilder().withUrl("/siteHub").build();

        // Start the SignalR connection
        connection.start().then(function () {
            console.log('Connected to siteHub');
        }).catch(function (err) {
            return console.error(err.toString());
        });

        // Handle the "ReceiveSite" event from SignalR
        connection.on("ReceiveSite", function (site) {
            console.log('Received a new site:', site);
         /*   fetchDataFromServer();*/
        });

        // Fetch data from the server when the page loads
       /* fetchDataFromServer();*/

        // Handle the click event for sending a new site
        $('#btnSendMessage').click(function (e) {
            var site = {
                projectname: $("#txtProjectName").val(),
                pmi: $("#txtPMI").val(),
                asec: $("#txtAsec").val(),
                remarks: $("#txtRemarks").val(),
                status: $("#txtStatus").val(),
                date: $("#txtDate").val()
            };

            // Call the SignalR method to create a new site
            connection.invoke("Create", site).then(function () {
                console.log('Site created successfully');
            }).catch(function (err) {
                console.error('Error creating site:', err.toString());
            });

            // Clear input fields
            $("#txtProjectName").val('');
            $("#txtPMI").val('');
            $("#txtAsec").val('');
            $("#txtRemarks").val('');
            $("#txtStatus").val('');
            $("#txtDate").val('');

            e.preventDefault();
        });
    });
});

//function fetchDataFromServer() {
//    var tr;
//    $.ajax({
//        url: '/SiteInstruction/GetSiteList', // Replace with the actual API endpoint URL
//        method: 'GET',
//        dataType: 'json',
//        success: function (data) {
//            // Assuming data is an array of objects with properties like projectname, pmi, etc.
//            $("#tableBody").empty(); // Clear existing rows

//            // Loop through the data and add rows to the table
//            $.each(data, function (index, site) {
//                tr += `<tr>
//                    <td>${site.projectname}</td>
//                    <td>${site.pmi}</td>
//                    <td>${site.asec}</td>
//                    <td>${site.remarks}</td>
//                    <td>${site.status}</td>
//                    <td>${site.date}</td>
//                </tr>`;
//            });
//            $("#tableBody").html(tr);
//        },
//        error: function (error) {
//            console.error("Error fetching data: ", error);
//        }
//    });
//}
