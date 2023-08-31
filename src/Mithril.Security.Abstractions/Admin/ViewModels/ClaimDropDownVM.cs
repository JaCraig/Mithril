using Mithril.Admin.Abstractions.DataEditor.Attributes;
using Mithril.API.Abstractions.Admin.DropDowns;
using Mithril.Data.Abstractions.Interfaces;

namespace Mithril.Security.Abstractions.Admin.ViewModels
{
    /// <summary>
    /// Claim Drop Down VM
    /// </summary>
    public class ClaimDropDownVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimDropDownVM"/> class.
        /// </summary>
        public ClaimDropDownVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimDropDownVM"/> class.
        /// </summary>
        /// <param name="claim">The claim.</param>
        public ClaimDropDownVM(IUserClaim claim)
        {
            Claim = claim?.ID ?? 0;
        }

        /// <summary>
        /// Gets or sets the claim.
        /// </summary>
        /// <value>The claim.</value>
        [Order(1)]
        [Select(typeof(UserClaimDropDown))]
        public long Claim { get; set; }
    }
}