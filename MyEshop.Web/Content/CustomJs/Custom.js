// ثبت نظر
function SuccessComment() {
    $("#Comment").val("");
    $("#ParentId").val("");
    swal({
        title: "ثبت نظر",
        text: "نظر شما با موفقیت ثبت شد !",
        icon: "success"
    });
}

// ثبت پاسخ به نظر
function replyComment(commentId) {
    $("#ParentId").val(commentId);
}