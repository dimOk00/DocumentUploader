using Newtonsoft.Json;

namespace DocumentUploader.Data;

public record File : FileMetadata
{
    public string Name { get; set; } = string.Empty;
    public long SizeInKb { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Link { get; set; } = string.Empty;
}