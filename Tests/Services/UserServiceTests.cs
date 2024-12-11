using LearningPlatformBackend.Services;
using LearningPlatformBackend.Entities;
using Xunit;
using FluentAssertions;
using LearningPlatformBackend.Repositories;

public class UserServiceTests
{
    [Fact]
    public async Task GetAllUsersAsync_ReturnsUsers()
    {
        // Arrange
        var context = TestDbContextHelper.GetInMemoryDbContext();
        context.Users.AddRange(
            new User { Id = 1, Username = "testuser1" },
            new User { Id = 2, Username = "testuser2" }
        );
        await context.SaveChangesAsync();

        var userRepository = new GenericRepository<User>(context);
        var userService = new UserService(userRepository);

        // Act
        var users = await userService.GetAllUsersAsync();

        // Assert
        users.Should().HaveCount(2);
        users.First().Username.Should().Be("testuser1");
    }
}
