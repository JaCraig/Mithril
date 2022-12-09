namespace Mithril.Core.Abstractions.Services.Options
{
    /// <summary>
    /// IP filter options
    /// </summary>
    public class IPFilterOptions
    {
        /// <summary>
        /// Gets the policies.
        /// </summary>
        /// <value>The policies.</value>
        private Dictionary<string, IPFilterPolicy> Policies { get; } = new Dictionary<string, IPFilterPolicy>();

        /// <summary>
        /// Adds the default policy.
        /// </summary>
        /// <returns>The default policy.</returns>
        public IPFilterPolicy? AddDefaultPolicy()
        {
            return AddPolicy("DefaultPolicy");
        }

        /// <summary>
        /// Adds a policy with the name, or if it already exists returns it.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The IP filter policy specified.</returns>
        public IPFilterPolicy? AddPolicy(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            if (Policies.TryGetValue(name, out var policy))
                return policy;
            policy = new IPFilterPolicy(name);
            Policies.Add(name, policy);
            return policy;
        }

        /// <summary>
        /// Tries to get the policy specified.
        /// </summary>
        /// <param name="policyName">Name of the policy.</param>
        /// <param name="policy">The policy.</param>
        /// <returns>True if it is found, false otherwise.</returns>
        public bool TryGetPolicy(string policyName, out IPFilterPolicy? policy)
        {
            if (string.IsNullOrEmpty(policyName))
            {
                policy = null;
                return false;
            }
            return Policies.TryGetValue(policyName, out policy);
        }
    }
}