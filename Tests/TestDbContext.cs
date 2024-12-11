using LearningPlatformBackend.Data;
using Microsoft.EntityFrameworkCore;

public static class TestDbContextHelper
{
    public static ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        return context;
    }
}
