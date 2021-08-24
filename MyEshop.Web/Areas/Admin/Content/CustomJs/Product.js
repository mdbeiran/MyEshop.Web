$(document).ready(function () {
    //$("#MainGroupId").change(function () {
    //    $("#SubGroupId").empty();
    //    $.ajax({
    //        type: 'Get',
    //        url: '/Admin/ManageProducts/GetSubGroups/',
    //        dataType: 'json',
    //        data: { id: $("#MainGroupId").val() },
    //        success: function (subs) {
    //            $.each(subs, function (i, sub) {
    //                $("#SubGroupId").append('<option value="' + sub.Value + '">' + sub.Text + '</option>');
    //            });
    //        },
    //        error: function (ex) {
    //            alert('Failed to retrieve states.' + ex);
    //        }
    //    });
    //    return false;
    //});

    $('#Tags').tagsinput({
        tagClass: 'big'
    });
    $('#Tags').tagsinput({
        onTagExists: function (item, $tag) {
            $tag.hide.fadeIn();
        }
    });

});