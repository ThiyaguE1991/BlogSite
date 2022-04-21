



function getDeleteString(data) {
    return "<a href='#' class='float-right' onclick=\"DeleteRecord('" + data.BlogId + "')\"><i class='fa fa-trash' style='margin-left:5px;'></i></a> ";
}

function getEditString(data) {
    return "<a href='#' class='float-right' onclick =\"EditRecord('" + data.BlogId + "')\"><i class='fa fa-edit'></i></a>";
}

function getReplyString(data) {
    return "<a href='#' class='float-right' onclick =\"ReplyRecord('" + data.CommentId + "')\" ><i class='fa fa-reply'></i></a>";
}

function DeleteRecord(id) {
    openDeleteDialog(id);
    // alert('deleted ' + id);
}

function EditRecord(id) {
    openAddorEditPopup(id);
    // alert('Edited ' + id);
}

function ReplyRecord(id) {
    openReplyPopup(id);
}

function OpenCommentPage(id) {
    location.href = '/Comment/Index?blogId=' + id;
}

function getCommentLinkString(data) {
    return "<a href='#' class='float-right' style='margin-left:3px;margin-right:3px;' onclick =\"OpenCommentPage('" + data.BlogId + "')\"><i class='fa fa-envelope'></i></a>";
}