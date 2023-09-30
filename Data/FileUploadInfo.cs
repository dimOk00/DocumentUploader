namespace DocumentUploader.Data;

public record FileUploadInfo
{
    public string Name { get; set; } = string.Empty;
    public long SizeInKb { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}