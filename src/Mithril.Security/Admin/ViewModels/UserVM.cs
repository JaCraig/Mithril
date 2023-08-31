using BigBook;
using Mithril.Admin.Abstractions.BaseClasses;
using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.Data.Abstractions.Services;
using Mithril.Data.Models.General;
using Mithril.Security.Abstractions.Admin.ViewModels;
using Mithril.Security.Admin.DropDowns;
using Mithril.Security.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Mithril.Security.Admin.ViewModels
{
    /// <summary>
    /// User view model
    /// </summary>
    /// <seealso cref="EntityBaseClass&lt;User&gt;" />
    public class UserVM : EntityBaseClass<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserVM"/> class.
        /// </summary>
        public UserVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserVM" /> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="full">if set to <c>true</c> [full].</param>
        /// <param name="contactInfoType">Type of the contact information.</param>
        public UserVM(User model, bool full, LookUpType? contactInfoType) : base(model)
        {
            if (model is null)
                return;
            FirstName = model.FirstName;
            LastName = model.LastName;
            MiddleName = model.MiddleName;
            NickName = model.NickName;
            Prefix = model.Prefix;
            Suffix = model.Suffix;
            Title = model.Title;
            UserName = model.UserName;
            if (!full)
                return;
            Claims = model.Claims.ToList(x => new ClaimDropDownVM(x));
            ContactInformation = model.ContactInformation.ToList(x => new ContactInformationVM(x, contactInfoType));
            Tenant = model.TenantID;
        }

        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        /// <value>
        /// The claims.
        /// </value>
        [Order(11)]
        public List<ClaimDropDownVM> Claims { get; set; } = new List<ClaimDropDownVM>();

        /// <summary>
        /// Gets or sets the contact information.
        /// </summary>
        /// <value>
        /// The contact information.
        /// </value>
        [Order(10)]
        public List<ContactInformationVM> ContactInformation { get; set; } = new List<ContactInformationVM>();

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Order(3)]
        public string? FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        [Required]
        [MaxLength(100)]
        [MinLength(1)]
        [Order(5)]
        public string? LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        [MaxLength(100)]
        [Order(4)]
        public string? MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the nick name.
        /// </summary>
        /// <value>The nick name.</value>
        [MaxLength(100)]
        [Order(7)]
        public string? NickName { get; set; }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        /// <value>The prefix.</value>
        [MaxLength(30)]
        [Order(2)]
        public string? Prefix { get; set; }

        /// <summary>
        /// Gets or sets the suffix.
        /// </summary>
        /// <value>The suffix.</value>
        [MaxLength(30)]
        [Order(6)]
        public string? Suffix { get; set; }

        /// <summary>
        /// Gets or sets the tenant.
        /// </summary>
        /// <value>
        /// The tenant.
        /// </value>
        [Select(typeof(TenantDropDown))]
        [DoNotList]
        [Order(9)]
        public long Tenant { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [MaxLength(100)]
        [Order(8)]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        [Required]
        [MaxLength(100)]
        [Order(1)]
        public string? UserName { get; set; }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="dataService">The data service.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>
        /// The async task.
        /// </returns>
        public override Task<User?> SaveAsync(long id, IDataService? dataService, ClaimsPrincipal? currentUser)
        {
            var Model = User.Load(id, dataService) ?? new User(UserName, FirstName, LastName, null);
            return Task.FromResult<User?>(null);
        }
    }
}