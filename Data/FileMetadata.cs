using Newtonsoft.Json;

namespace DocumentUploader.Data;

public record FileMetadata
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;
    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;
}