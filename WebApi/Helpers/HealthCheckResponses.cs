using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace WebApi.Helpers
{
    public static class HealthCheckResponses
    {
        public static Task WriteJsonResponse(HttpContext httpContext, HealthReport healthReport)
        {
            httpContext.Response.ContentType = "application/json; charset=utf-8";
            var options = new JsonWriterOptions { Indented = true };
            using var writer = new Utf8JsonWriter(httpContext.Response.BodyWriter, options);
            writer.WriteStartObject();
            writer.WriteString("status", healthReport.Status.ToString());

            if (healthReport.Entries.Count > 0)
            {
                writer.WriteStartArray("results");

                foreach (var (key, value) in healthReport.Entries)
                {
                    writer.WriteStartObject();
                    writer.WriteString("key", key);
                    writer.WriteString("status", value.Status.ToString());
                    writer.WriteString("description", value.Description);
                    writer.WriteStartArray("data");
                    foreach (var (dataKey, dataValue) in value.Data.Where(d => d.Value is object))
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName(dataKey);
                        JsonSerializer.Serialize(writer, dataValue, dataValue.GetType());
                        writer.WriteEndObject();
                    }
                    writer.WriteEndArray();
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }

            writer.WriteEndObject();

            return Task.CompletedTask;
        }
    }
}
