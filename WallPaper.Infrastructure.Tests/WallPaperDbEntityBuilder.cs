using WallPaper.Core.Entities;

namespace WallPaper.Infrastructure.Tests
{
    public class WallPaperDbEntityBuilder
    {
        private WallPaperDbEntity apiImage = new WallPaperDbEntity();

        public WallPaperDbEntityBuilder Title(string imageName)
        {
            apiImage.ImageName = imageName;
            return this;
        }

        public WallPaperDbEntityBuilder Favorite(bool favorite)
        {
            apiImage.Favorite = favorite;
            return this;
        }

        public WallPaperDbEntityBuilder WithDefaultValues()
        {
            apiImage = new WallPaperDbEntity() { ImageName = "Test Item", Favorite = true };
            return this;
        }

        public WallPaperDbEntity Build() => apiImage;
    }
}
