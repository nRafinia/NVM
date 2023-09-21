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
            {"image/png", "fa-file-image"},
            {"image/x-icon", "fa-file-image"},
            {"image/jpeg", "fa-file-image"},
            {"image/bmp", "fa-file-image"},
            {"image/avif", "fa-file-image"},
            {"image/gif", "fa-file-image"},
            {"image/tiff", "fa-file-image"},
            {"image/webp", "fa-file-image"},
            
            {"audio", "fa-file-audio"},
            {"audio/aac", "fa-file-audio"},
            {"application/x-cdf", "fa-file-audio"},
            {"audio/midi", "fa-file-audio"},
            {"audio/x-midi", "fa-file-audio"},
            {"audio/mpeg", "fa-file-audio"},
            {"audio/ogg", "fa-file-audio"},
            {"audio/opus", "fa-file-audio"},
            {"audio/wav", "fa-file-audio"},
            {"audio/webm", "fa-file-audio"},
            
            {"video", "fa-file-video"},
            {"video/x-msvideo", "fa-file-video"},
            {"video/mp4", "fa-file-video"},
            {"video/mpeg", "fa-file-video"},
            {"video/ogg", "fa-file-video"},
            {"video/mp2t", "fa-file-video"},
            {"video/webm", "fa-file-video"},
            
            {"application/pdf", "fa-file-pdf"},
            {"application/java-archive", "fa-java"},
            
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
            {"text/csv", "fa-file-csv"},
            {"text/css", "fa-file-text"},
            
            {"application/json", "fa-file-code"},
            {"application/ld+json", "fa-file-code"},
            {"application/x-sh", "fa-file-code"},
            {"application/xhtml+xml", "fa-file-code"},
            {"application/html", "fa-file-code"},
            {"application/x-httpd-php", "fa-file-code"},
            {"text/javascript", "fa-file-code"},
            {"application/x-javascript", "fa-file-code"},
            {"application/x-csh", "fa-file-code"},
            
            {"application/gzip", "fa-file-archive"},
            {"application/x-bzip", "fa-file-archive"},
            {"application/x-bzip2", "fa-file-archive"},
            {"application/x-freearc", "fa-file-archive"},
            {"application/x-tar", "fa-file-archive"},
            {"application/vnd.rar", "fa-file-archive"},
            {"application/x-7z-compressed", "fa-file-archive"},
            {"application/zip", "fa-file-zip"}
        };

        return iconMapping.ContainsKey(mimeType) ? iconMapping[mimeType] : "fa-file";
    }
}