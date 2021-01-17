using System.ComponentModel.DataAnnotations;

namespace ReportService.ViewModels
{
    public class ReportDate
    {
        [Range(0, int.MaxValue)] public int Year { get; set; }

        [Range(1, 12)] public int Month { get; set; }
    }
}