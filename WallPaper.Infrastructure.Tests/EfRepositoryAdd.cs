using System.Linq;
using System.Threading.Tasks;
using WallPaper.Core.Entities;
using Xunit;

namespace WallPaper.Infrastructure.Tests
{
    public class EfRepositoryAdd : BaseEfRepoTestFixture
    {
        [Fact]
        public async Task AddsEntity()
        {
            var repository = GetRepository();
            var item = new WallPaperDbEntityBuilder().WithDefaultValues().Build();
            await repository.AddAsync(item);
            var newItem = (await repository.ListAsync<WallPaperDbEntity>())
                            .FirstOrDefault();
            Assert.Equal(item, newItem);
            Assert.False(string.IsNullOrEmpty(newItem.ImageName));
        }
    }
}
