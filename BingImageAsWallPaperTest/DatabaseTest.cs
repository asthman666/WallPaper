using BingImageAsWallPaper.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BingImageAsWallPaperTest
{
    public class DatabaseTest : IClassFixture<BaseTest>, IDisposable
    {
        WallPaperContext dbContext;
        public DatabaseTest(BaseTest fixture)
        {
            dbContext = fixture.serviceProvider.GetRequiredService<WallPaperContext>();
            cleanDatabase();
        }

        [Fact]
        public void CRUDTest()
        {
            // CREATE
            dbContext.Add(new WallPaperDbEntity { Favorite = true, ImageName = "test" });
            dbContext.SaveChanges();
            Assert.True(1 == dbContext.WallPaper.Count());

            // READ
            var image = dbContext.WallPaper.FirstOrDefault();
            Assert.Equal("test", image.ImageName);
            Assert.True(image.Favorite);

            // UPDATE
            image.ImageName = "test1";
            image.Favorite = false;
            dbContext.Update(image);
            var updateImage = dbContext.WallPaper.FirstOrDefault();
            Assert.Equal("test1", updateImage.ImageName);
            Assert.True(!updateImage.Favorite);

            // DELETE
            dbContext.Remove(updateImage);
            dbContext.SaveChanges();
            Assert.True(!dbContext.WallPaper.Any());
        }

        private void cleanDatabase()
        {
            var wallPapers = dbContext.WallPaper.ToList();
            foreach (var wallPaper in wallPapers)
            {
                dbContext.Remove(wallPaper);
            }
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            cleanDatabase();
        }
    }
}
