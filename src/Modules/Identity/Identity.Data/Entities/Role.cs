using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;
using Common.Domain;

namespace Identity.Data.Entities
{
    public sealed class Role : IdentityRole<Guid>
    {
        #region Constructor
        public Role()
        {
            Id = SequentialGuidGenerator.GenerateNewGuid();
        }

        public Role(string name) : base(name)
        {

        }

        public Role(string name, string description) : base(name)
        {
            Description = description;
        }
        #endregion

        #region Props

        public string Description { get; set; }

        /////// <summary>
        /////// لیست دسترسی های گروه کاربری
        /////// </summary>
        //public string Permissions { get; set; }

        ///// <summary>
        /////ساختار ایکس ام ال لیست دسترسی های گروه کاربری
        ///// </summary>
        //public XElement XmlPermission
        //{
        //    get =>  XElement.Parse(Permissions);
        //    set => Permissions = value.ToString();
        //}

        #endregion Props

        #region Relations
        public ICollection<RoleClaim> Claims { get; set; }
        public ICollection<UserRole> Users { get; set; }

        #endregion
    }
}
