﻿function GetFileSizeNameAndType() {
    var fi = document.getElementById('file'); // GET THE FILE INPUT AS VARIABLE.

    var totalFileSize = 0;

    // VALIDATE OR CHECK IF ANY FILE IS SELECTED.
    if (fi.files.length > 0) {
        // RUN A LOOP TO CHECK EACH SELECTED FILE.
        for (var i = 0; i <= fi.files.length - 1; i++) {
            //ACCESS THE SIZE PROPERTY OF THE ITEM OBJECT IN FILES COLLECTION. IN THIS WAY ALSO GET OTHER PROPERTIES LIKE FILENAME AND FILETYPE
            var fsize = fi.files.item(i).size;
            totalFileSize = totalFileSize + fsize;
            document.getElementById('fp').innerHTML =
                document.getElementById('fp').innerHTML
                +
                '<br /> ' + 'File Name: <b>' + fi.files.item(i).name
                +
                '</b>, Size: <b>' + Math.round((fsize / 1024)) //DEFAULT SIZE IS IN BYTES SO WE DIVIDING BY 1024 TO CONVERT IT IN KB
                +
                '</b> KB /n';/* and File Type is <b>' + fi.files.item(i).type + "</b>."*/
        }
    }
    document.getElementById('divTotalSize').innerHTML = "Total File(s) Size: <b>" + Math.round(totalFileSize / 1024) + "</b> KB";
}