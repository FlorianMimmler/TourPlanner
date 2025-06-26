
namespace DAL.Queries
{
    public abstract class DbQueryBase
    {
        protected readonly TourPlannerDbContextFactory ContextFactory;

        protected DbQueryBase(TourPlannerDbContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
        }
    }
}
