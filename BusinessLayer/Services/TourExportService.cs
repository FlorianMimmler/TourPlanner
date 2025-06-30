using BusinessLayer.Interfaces;
using Newtonsoft.Json;

namespace BusinessLayer.Services
{
    public class TourExportService : ITourExportService
    {

        private ITourService _tourService;
        private ITourLogService _tourLogService;

        private readonly ILoggerWrapper _logger;

        public TourExportService(ITourService tourService, ITourLogService tourLogService, ILoggerWrapper logger)
        {
            _tourService = tourService;
            _tourLogService = tourLogService;
            _logger = logger;
        }


        public async Task ExportToursToJson(string filePath = "..")
        {
            try
            {
                var tours = await _tourService.GetTours();

                var json = JsonConvert.SerializeObject(tours, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath + "/tourdata.json", json);
            }
            catch (Exception ex)
            {
                _logger.Error("Error exporting tours to JSON: " + ex.Message);
            }
        }

    }
}
