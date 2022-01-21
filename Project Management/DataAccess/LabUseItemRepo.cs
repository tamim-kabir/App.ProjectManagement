using Project_Management.Data;
using Project_Management.Models;

namespace Project_Management.DataAccess
{
    public class LabUseItemRepo : BaseRepo<LabUseItem, AuthDbContext>
    {
        public LabUseItemRepo(AuthDbContext authDbContext) : base(authDbContext)
        {

        }

    }
}
