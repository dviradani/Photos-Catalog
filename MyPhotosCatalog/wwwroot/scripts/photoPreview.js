document.querySelector("#fileInput").addEventListener("change", function () {
    const file = this.files[0];
    const reader = new FileReader();

    reader.onload = function (e) {
        document.querySelector("#preview").src = e.target.result;
    };

    reader.readAsDataURL(file);
});
