using CliWrap;
using CliWrap.Buffered;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Domaineer.WebApp.Services;

public class DogeService
{
    const string DOGE_PATH = "doge";

    public static async Task<bool> DoesDomainExist(string domain)
    {
        var result = await Cli.Wrap(DOGE_PATH)
            .WithArguments(["--json", domain])
            .ExecuteBufferedAsync();
        if (result.IsSuccess is false) return false;

        var response = JsonSerializer.Deserialize<DogeResponse>(result.StandardOutput);
        if (response is null) return false;

        return !response.Responses.Any(r => r.Error is not null && r.Error == "NXDomain");
    }
}


public record Response(
    [property: JsonPropertyName("error")] string? Error
);

public record DogeResponse(
    [property: JsonPropertyName("responses")] IReadOnlyList<Response> Responses
);

