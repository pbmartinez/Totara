using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text;
using System.Text.Json;

namespace WebApi.Helpers
{
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// Writes reports of health check in json format 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="healthReport"></param>
        /// <returns></returns>
        public static Task WriteJsonResponse(HttpContext httpContext, HealthReport healthReport)
        {            
            httpContext.Response.ContentType = "application/json; charset=utf-8";

            var options = new JsonWriterOptions { Indented = true };

            using var memoryStream = new MemoryStream();
            using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
            {
                jsonWriter.WriteStartObject();
                jsonWriter.WriteString("status", healthReport.Status.ToString());
                jsonWriter.WriteStartObject("results");

                foreach (var healthReportEntry in healthReport.Entries)
                {
                    jsonWriter.WriteStartObject(healthReportEntry.Key);
                    jsonWriter.WriteString("status",
                        healthReportEntry.Value.Status.ToString());
                    jsonWriter.WriteString("description",
                        healthReportEntry.Value.Description);
                    jsonWriter.WriteStartObject("data");

                    foreach (var item in healthReportEntry.Value.Data)
                    {
                        jsonWriter.WritePropertyName(item.Key);

                        JsonSerializer.Serialize(jsonWriter, item.Value,
                            item.Value?.GetType() ?? typeof(object));
                    }

                    jsonWriter.WriteEndObject();
                    jsonWriter.WriteEndObject();
                }

                jsonWriter.WriteEndObject();
                jsonWriter.WriteEndObject();
            }

            return httpContext.Response.WriteAsync(
                Encoding.UTF8.GetString(memoryStream.ToArray()));
        }

        public static IEndpointRouteBuilder MapDefaultHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHealthChecks("/health/live", new HealthCheckOptions()
            {
                Predicate = _ => false,
                ResponseWriter = WriteJsonResponse
            });
            endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
            {
                ResponseWriter = WriteJsonResponse
            });

            return endpoints;
        }
    }
}
