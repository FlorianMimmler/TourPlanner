using Newtonsoft.Json;

namespace TourPlanner.BusinessLayer.Services
{
    public class TourExportService
    {

        private ITourService _tourService;
        private ITourLogService _tourLogService;

        public TourExportService(ITourService tourService, ITourLogService tourLogService)
        {
            _tourService = tourService;
            _tourLogService = tourLogService;
        }


        public async Task ExportToursToJson(string filePath = "..")
        {

            var tours = await _tourService.GetTours();

            var json = JsonConvert.SerializeObject(tours, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath + "/tourdata.json", json);
        }

    }
}
