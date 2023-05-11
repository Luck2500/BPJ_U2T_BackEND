using BPJ_U2T.DTOS.Report;

namespace BPJ_U2T.Interfaces
{
    public interface IReportService
    {
        Task<SalesStatisticsDTO> SalesStatistics();
    }
}
