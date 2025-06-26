using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TourPlanner.BusinessLayer.Model;

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
