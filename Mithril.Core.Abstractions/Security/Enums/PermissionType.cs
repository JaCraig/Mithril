namespace Mithril.Core.Abstractions.Security.Enums
{
    /// <summary>
    /// Permission type
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// All claims must match in order to be true
        /// </summary>
        All,

        /// <summary>
        /// Any claims can match in order to be true
        /// </summary>
        Any
    }
}