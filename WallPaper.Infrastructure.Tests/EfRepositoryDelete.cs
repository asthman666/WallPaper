using System;
using System.Threading.Tasks;
using WallPaper.Core.Entities;
using Xunit;

namespace WallPaper.Infrastructure.Tests
{
    public class EfRepositoryDelete : BaseEfRepoTestFixture
    {
        [Fact]
        public async Task DeletesItemAfterAddingIt()
        {
            // add an item
            var repository = GetRepository();
            var initialTitle = Guid.NewGuid().ToString();
            var item = new WallPaperDbEntityBuilder().Title(initialTitle).Build();
            await repository.AddAsync(item);

            // delete the item
            await repository.DeleteAsync(item);

            // verify it's no longer there
            Assert.DoesNotContain(await repository.ListAsync<WallPaperDbEntity>(),
                i => i.ImageName == initialTitle);
        }
    }
}
