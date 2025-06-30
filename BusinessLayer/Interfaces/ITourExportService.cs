namespace BusinessLayer.Interfaces
{
    public interface ITourExportService
    {
        Task ExportToursToJson(string filePath = "..");
    }
}