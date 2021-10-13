using BingImageAsWallPaper.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
