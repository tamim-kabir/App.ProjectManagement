using Project_Management.Data;
using Project_Management.Models;

namespace Project_Management.DataAccess
{
    public class InstituteRepo : BaseRepo<Institute, AuthDbContext>
    {

        public InstituteRepo(AuthDbContext authDbContext) : base(authDbContext)
        {

        }


    }
}
