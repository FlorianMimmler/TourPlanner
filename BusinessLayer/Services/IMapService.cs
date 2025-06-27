
namespace BusinessLayer.Services
{
    public interface IMapService
    {
        Task<string> GetRouteGeoJson(string start, string end);

    }
}