using BridgePatternExample.Models;
using Microsoft.EntityFrameworkCore;

namespace BridgePatternExample.Data
{
    public class BridgePatternExampleContext : DbContext
    {
        public BridgePatternExampleContext (DbContextOptions<BridgePatternExampleContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Car { get; set; } = default!;
    }
}
