using Common.Domain;

namespace BlogModule.Domain
{
    public class Blog : BaseEntity
    { 
        #region Constractors
        public Blog()
        {

        }
        #endregion

        #region Properties
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Descriptions { get; set; }
        public string Author { get; set; }
        public string Slug { get; set; }
        public string DesktopImage { get; set; }
        public string MobileImage { get; set; }
        #endregion
        #region ForenKeies
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        #endregion
    }
}
