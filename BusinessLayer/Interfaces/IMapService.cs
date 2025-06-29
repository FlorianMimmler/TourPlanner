namespace BusinessLayer.Interfaces
{
    public interface IMapService
    {
        Task<string> GetRouteGeoJson(string start, string end);

    }
}