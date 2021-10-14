namespace WallPaper.Core.Entities
{
    public class WallPaperDbEntity : BaseEntity
    {
        public string ImageName { get; set; }
        public bool Favorite { get; set; }
    }
}
