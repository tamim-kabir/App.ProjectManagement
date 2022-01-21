using Project_Management.Data;
using Project_Management.Models;

namespace Project_Management.DataAccess
{
    public class ItemsRepo : BaseRepo<Items, AuthDbContext>
    {
        public ItemsRepo(AuthDbContext authDbContext) : base(authDbContext)
        {

        }

    }
}
