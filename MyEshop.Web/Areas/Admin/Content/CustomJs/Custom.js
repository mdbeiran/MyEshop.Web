// Users
function DeleteUser(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/DeleteUser/" + id, function (res) {
            location.reload();
        });
    });
}

function ReturnUser(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageUsers/ReturnUser/" + id, function (res) {
            location.reload();
        });
    });
}


// Products
function DeleteProductGroup(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/DeleteGroup/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}

function ReturnProductGroup(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/ReturnGroup/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}

function DeleteProductBrand(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/DeleteBrand/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}

function DeleteProductGallery(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/DeleteProductGallery/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}

function ReturnProductBrand(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/ReturnBrand/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}

function DeleteProductFeature(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/DeleteFeature/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}

function ReturnProductFeature(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/ReturnFeature/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}

function DeleteProductSelectedFeature(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/DeleteProductFeature/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}

function ReturnProductSelectedFeature(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/ReturnProductFeature/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}


function DeleteProduct(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/DeleteProduct/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}

function ReturnProduct(id) {
    $(document).on('confirmation', '.remodal', function () {
        $.get("/Admin/ManageProducts/ReturnProduct/" + id, function (res) {
            //if (statusText === "success") {
            //    location.reload();
            //}
            location.reload();
        });
    });
}


// پیش نمایش عکس محصول قبل از اپلود روی سرور
$("#ProductImageInput").change(function () {
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $('#ProductImage').attr('src', e.target.result);
        };
        reader.readAsDataURL(this.files[0]);
    }
});