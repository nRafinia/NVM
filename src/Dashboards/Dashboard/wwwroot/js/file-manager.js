function CreateFolder() {
    let value = $("#txtFolderName").val();
    if (!value) {
        return;
    }

    callCallback(value);
}