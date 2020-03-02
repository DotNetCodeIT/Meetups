using Food.API.Repositories;
using System.Threading.Tasks;

namespace Food.API.Services
{
    public interface ISeedDataService
    {
        Task Initialize(FoodDbContext context);
    }
}
