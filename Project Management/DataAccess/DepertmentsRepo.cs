using Project_Management.Data;
using Project_Management.Models;

namespace Project_Management.DataAccess
{
    public class DepertmentsRepo : BaseRepo<Depertment, AuthDbContext>
    {
        public DepertmentsRepo(AuthDbContext authDbContext) : base(authDbContext)
        {

        }

    }
}

