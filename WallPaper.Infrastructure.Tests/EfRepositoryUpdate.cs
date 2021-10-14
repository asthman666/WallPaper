using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WallPaper.Core.Entities;
using Xunit;

namespace WallPaper.Infrastructure.Tests
{
    public class EfRepositoryUpdate : BaseEfRepoTestFixture
    {
        [Fact]
        public async Task UpdatesItemAfterAddingIt()
        {
            // add an item
            var repository = GetRepository();
            var initialTitle = Guid.NewGuid().ToString();
            var item = new WallPaperDbEntityBuilder().Title(initialTitle).Build();
            await repository.AddAsync(item);

            // detach the item so we get a different instance when using ListAsync to fetch this item from the database
            _dbContext.Entry(item).State = EntityState.Detached;

            // fetch the item and update its title
            var newItem = (await repository.ListAsync<WallPaperDbEntity>())
                    .FirstOrDefault(i => i.ImageName == initialTitle);
            Assert.NotNull(newItem);
            Assert.NotSame(item, newItem);

            var newTitle = Guid.NewGuid().ToString();
            newItem.ImageName = newTitle;

            // Update the item
            await repository.UpdateAsync(newItem);
            var updatedItem = (await repository.ListAsync<WallPaperDbEntity>())
                .FirstOrDefault(i => i.ImageName == newTitle);

            Assert.NotNull(updatedItem);
            Assert.NotEqual(item.ImageName, updatedItem.ImageName);
        }
    }
}
