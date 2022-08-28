using Bogus;
using Dictionary.Api.Domain.Models;
using Dictionary.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dictionary.Infrastructure.Persistence.Context
{
    internal class SeedData
    {
        private static List<User> GetUsers()
        {
            var result = new Faker<User>("tr")
           .RuleFor(s => s.Id, s => Guid.NewGuid())
           .RuleFor(s => s.CreateDate, s => s.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
           .RuleFor(s => s.FirstName, s => s.Person.FirstName)
           .RuleFor(s => s.LastName, s => s.Person.LastName)
           .RuleFor(s => s.EmailAddress, s => s.Internet.Email())
           .RuleFor(s => s.UserName, s => s.Internet.UserName())
           .RuleFor(s => s.Password, s => PasswordEncryptor.Encyrpt(s.Internet.Password()))
           .RuleFor(s => s.EmailConfirmed, s => s.PickRandom(true, false))
           .Generate(500);

            return result;
        }

        public async Task SeedAsync(IConfiguration configuration)
        {
            var dbContextBuilder = new DbContextOptionsBuilder();
            dbContextBuilder.UseSqlServer(configuration["DictionaryContextConnectionString"]);

            var context = new DictionaryContext(dbContextBuilder.Options);

            var users = GetUsers();
            var userIds = users.Select(s => s.Id);

            await context.Users.AddRangeAsync(users);

            var guids = Enumerable.Range(0, 150).Select(s => Guid.NewGuid()).ToList();
            int counter = 0;

            var entries = new Faker<Entry>("tr")
                .RuleFor(s => s.Id, s => guids[counter++])
                .RuleFor(s => s.CreateDate, s => s.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                .RuleFor(s => s.Subject, s => s.Lorem.Sentence(5, 5))
                .RuleFor(s => s.Content, s => s.Lorem.Paragraph(2))
                .RuleFor(s => s.CreatedById, s => s.PickRandom(userIds))
                .Generate(150);

            await context.Entries.AddRangeAsync(entries);

            var comments = new Faker<EntryComment>("tr")
                 .RuleFor(s => s.Id, s => Guid.NewGuid())
                 .RuleFor(s => s.CreateDate, s => s.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
                 .RuleFor(s => s.Content, s => s.Lorem.Paragraph(2))
                 .RuleFor(s => s.CreatedById, s => s.PickRandom(userIds))
                 .RuleFor(s => s.EntryId, s => s.PickRandom(guids))
                 .Generate(1000);

            await context.EntryComments.AddRangeAsync(comments);
            await context.SaveChangesAsync();
        }
    }
}
