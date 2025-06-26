using Newtonsoft.Json;
using TourPlanner.Domain.Model;

namespace TourPlanner.BusinessLayer.Services
{
    public class TourImportService
    {
        private ITourService _tourService;
        private TourLogService _tourLogService;

        public TourImportService(ITourService tourService, TourLogService tourLogService)
        {
            _tourService = tourService;
            _tourLogService = tourLogService;
        }

        public async Task ImportToursFromJson(string filePath = "../tourdata.json")
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("JSON file not found", filePath);

            var json = File.ReadAllText(filePath);
            var importedDtos = JsonConvert.DeserializeObject<List<TourImportDto>>(json);

            foreach (var dto in importedDtos)
            {
                // Map DTO to Domain Model
                if (!Enum.TryParse(dto.TransportType, out TransportType parsedTransport))
                {
                    //throw new InvalidDataException($"Invalid transport type: {dto.TransportType}");
                    continue; // Skip this tour if transport type is invalid
                }

                var tour = new Tour
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    From = dto.From,
                    To = dto.To,
                    TransportType = parsedTransport,
                    Distance = dto.Distance,
                    EstimatedTime = dto.EstimatedTime,
                    Image = dto.Image
                };

                // Check if the tour already exists
                var existingTour = (await _tourService.GetTours()).FirstOrDefault(t => t.Id == tour.Id);
                if (existingTour != null)
                {
                    _tourService.UpdateTour(tour);
                }
                else
                {
                    await _tourService.AddTour(tour);
                }
            }
        }
    }
}
