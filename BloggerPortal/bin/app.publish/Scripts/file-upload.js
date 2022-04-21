

function fileUpload() {
    /*    $('#btnUpload').click(function () {*/

    // Checking whether FormData is available in browser  
    if (window.FormData !== undefined) {

        var fileUpload = $("#FileUpload1").get(0);
        var files = fileUpload.files;

        // Create FormData object  
        var fileData = new FormData();

        // Looping over all files and add it to FormData object  
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        if (files.length > 0) {
            // Adding one more key to FormData object  
            fileData.append('username', 'manas');

            $.ajax({
                url: '/FileUpload/UploadFiles',
                type: "POST",
                async:false,
                contentType: false, // Not to set any content header  
                processData: false, // Not to process data  
                data: fileData,
                success: function (result) {      
                    $('#MediaFileId').val(result.FileId);
                    return result;
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });
        }
        else {
            return "nofiles";
        }
    } else {
        return "nofiles";
    }
    /*});*/

    //Dropzone.autoDiscover = false;
    //$(fileUploadId).dropzone({
    //    url: "/FileUpload/Index",
    //    maxFiles: 1,
    //    acceptedFiles: acceptedformats,
    //    init: function () {
    //        this.on("sending", function (file, xhr, formData) {
    //            formData.append("id", id);
    //          /*  formData.append("mode", mode)*/;
    //        });

    //        this.on('success', function (file, response) {               
    //            if (this.files.length >= 1) {
    //               // this.removeFile(this.files[0]);
    //            }
    //            if (response != null) {
    //                $('#MediaFileId').val(response.FileId);
    //            }    
    //        });

    //        this.on("maxfilesexceeded", function (file) {
    //            this.removeAllFiles();
    //            this.addFile(file);
    //        });      
    //    }
    //});
}

function appendProductImageToList(response) {
    let htmlValue =
        '<div class="gallery" data-id="' + response.FileId + '">\
                <img src="../Content/MediaFiles/'+ response.FileName + '">\
                <div class="corner-icon">\
                    <input type="checkbox" title="Set as Home Image" onclick="setAsHomeImage(this);">\
                    <a href="#" onclick="removeImage(this);"><span class="close-icon" title="Remove the image"><i class="fa fa-minus-circle"></i></span></a>\
                </div>\
                </div>';
    $('#product-image-list').append(htmlValue);
}


function removeMediaFile(mediaFileId, mode) {
    $.post(
        "/FileUpload/RemoveFile",
        { mediaFileId: mediaFileId, mode: mode },
        function (data) {
            toastr.success('success', 'Selcted file has been deleted...');
        });
}