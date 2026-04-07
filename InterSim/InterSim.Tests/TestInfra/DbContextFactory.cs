using InterSim.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InterSim.Tests.TestInfra;

public static class DbContextFactory
{
    public static AppDbContext CreateInMemory(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new AppDbContext(options);
    }
}