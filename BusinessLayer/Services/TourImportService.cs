using BusinessLayer.Interfaces;
using Newtonsoft.Json;
using TourPlanner.Domain.Model;

namespace BusinessLayer.Services
{
    public class TourImportService
    {
        private ITourService _tourService;
        private ITourLogService _tourLogService;

        private readonly ILoggerWrapper _logger;

        public TourImportService(ITourService tourService, ITourLogService tourLogService, ILoggerWrapper logger)
        {
            _tourService = tourService;
            _tourLogService = tourLogService;
            _logger = logger;
        }

        public async Task ImportToursFromJson(string filePath = "../tourdata.json")
        {
            if (!File.Exists(filePath))
            {
                _logger.Error($"JSON file not found at {filePath}. Import tours aborted.");
                //throw new FileNotFoundException("JSON file not found", filePath);
                //TODO: Return something to handle in UI
                return;
            }

            List<TourImportDto> importedDtos = [];
            try
            {
                var json = File.ReadAllText(filePath);
                importedDtos = JsonConvert.DeserializeObject<List<TourImportDto>>(json) ?? [];
            } catch (Exception ex)
            {
                _logger.Error($"Failed to import tours from JSON at '{filePath}'. Exception: {ex}");
                //throw new InvalidDataException("Error reading or deserializing JSON file", ex);
                return; // Exit if there's an error reading the file
            }

            var skippedTours = new List<string>();

            foreach (var dto in importedDtos)
            {
                // Map DTO to Domain Model
                if (!Enum.TryParse(dto.TransportType, out TransportType parsedTransport))
                {
                    //throw new InvalidDataException($"Invalid transport type: {dto.TransportType}");
                    skippedTours.Add($"Tour ID: {dto.Id}, Name: {dto.Name} - Invalid transport type: {dto.TransportType}");

                    //_logger.Warn($"Invalid transport type: {dto.TransportType} in tour with ID {dto.Id}. Skipping import of this tour.");
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
                    await _tourService.UpdateTour(tour);
                }
                else
                {
                    await _tourService.AddTour(tour);
                }
            }

            if (skippedTours.Count > 0)
            {
                _logger.Warn($"Skipped {skippedTours.Count} tours due to invalid data:\n" + string.Join("\n", skippedTours));
            }
        }
    }
}
