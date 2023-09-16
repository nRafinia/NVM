namespace Agent.UI.Helpers;

public static class FileTypeIcon
{
    public static string Get(string mimeType)
    {
        if (string.IsNullOrEmpty(mimeType))
        {
            return "";
        }
        
        if (mimeType=="Directory")
        {
            return "fa-folder";
        }

        var iconMapping = new Dictionary<string, string>()
        {
            {"image", "fa-file-image"},
            {"audio", "fa-file-audio"},
            {"video", "fa-file-video"},
            {"application/pdf", "fa-file-pdf"},
            {"application/msword", "fa-file-word"},
            {"application/vnd.ms-word", "fa-file-word"},
            {"application/vnd.oasis.opendocument.text", "fa-file-word"},
            {"application/vnd.openxmlformats-officedocument.wordprocessingml","fa-file-word"},
            {"application/vnd.ms-excel", "fa-file-excel"},
            {"application/vnd.openxmlformats-officedocument.spreadsheetml","fa-file-excel"},
            {"application/vnd.oasis.opendocument.spreadsheet", "fa-file-excel"},
            {"application/vnd.ms-powerpoint", "fa-file-powerpoint"},
            {"application/vnd.openxmlformats-officedocument.presentationml","fa-file-powerpoint"},
            {"application/vnd.oasis.opendocument.presentation", "fa-file-powerpoint"},
            {"text/plain", "fa-file-text"},
            {"text/html", "fa-file-code"},
            {"application/json", "fa-file-code"},
            {"application/gzip", "fa-file-archive"},
            {"application/zip", "fa-file-archive"}
        };

        return iconMapping.ContainsKey(mimeType) ? iconMapping[mimeType] : "fa-file";
    }
}