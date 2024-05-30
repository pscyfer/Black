using Common.Domain;

namespace BlogModule.Domain
{
    public class Category : BaseEntity
    {
        #region Ctor
        public Category()
        {

        }
        #endregion

        #region Props
        public int Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string DesktopImage { get; set; }
        public string MobileImage { get; set; }
        #endregion

        #region Relations

        #endregion
        #region Methods
        #endregion
    }
}
