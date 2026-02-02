using Application.Interfaces;
using Domain;

namespace Infrastructure.Reports
{
    public class ReportService<T> : IReportService<T> where T : BaseEntity
    {
        public async Task<MemoryStream> GetCsvReport(List<T> records)
        {
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            var properties = typeof(T).GetProperties();

            // Write CSV header
            await writer.WriteLineAsync(string.Join(",", properties.Select(p => p.Name)));

            // Write CSV rows
            foreach (var record in records)
            {
                var values = properties.Select(p => p.GetValue(record)?.ToString()?.Replace(",", " ") ?? string.Empty);
                await writer.WriteLineAsync(string.Join(",", values));
            }

            await writer.FlushAsync();
            memoryStream.Position = 0; // Reset stream position for reading
            return memoryStream;
        }
    }
}

