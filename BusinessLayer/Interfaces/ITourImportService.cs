namespace BusinessLayer.Interfaces
{
    public interface ITourImportService
    {
        Task ImportToursFromJson(string filePath = "../tourdata.json");
    }
}