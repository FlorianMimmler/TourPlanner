using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Domain.Model
{
    public class TransportTypeHelper
    {

        public static TransportType StringToTransportType(string transportTypeString)
        {
            return transportTypeString switch
            {
                "Bike" => TransportType.Bike,
                "Hiking" => TransportType.Hiking,
                "Running" => TransportType.Running,
                "Vacation" => TransportType.Vacation,
                _ => TransportType.Unknown
            };
        }

        public static string TransportTypeToString(TransportType transportType)
        {
            return transportType switch
            {
                TransportType.Bike => "Bike",
                TransportType.Hiking => "Hiking",
                TransportType.Running => "Running",
                TransportType.Vacation => "Vacation",
                _ => "Unknown"
            };
        }
    }

    public enum TransportType
    {
        Bike,
        Hiking,
        Running,
        Vacation,
        Unknown
    }
}
