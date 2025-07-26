using Microsoft.EntityFrameworkCore;

namespace Passeio.Data.Context
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options){}
    }
}
