using Newtonsoft.Json;

namespace TourPlanner.BusinessLayer.Services
{
    public class TourOutputService
    {

        private ITourService _tourService;
        private TourLogService _tourLogService;

        public TourOutputService(ITourService tourService, TourLogService tourLogService)
        {
            _tourService = tourService;
            _tourLogService = tourLogService;
        }


        public void ExportToursToJson(string filePath = "..")
        {

            var tours = _tourService.GetTours();

            var json = JsonConvert.SerializeObject(tours, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath + "/tourdata.json", json);
        }

    }
}
