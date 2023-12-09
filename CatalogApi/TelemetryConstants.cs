using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace CatalogApi;

public class TelemetryConstants
{
    public const string ServiceName = "catalogApi";
    public readonly static ActivitySource ActivitySource = new(ServiceName);
    public readonly static Meter Meter = new(ServiceName);
}