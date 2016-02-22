namespace Domain.Shared
{
    /// <summary>
    /// An entity, as explained in the DDD book.
    /// </summary>  
    public interface IEntity<in T> : IDomainModel where T : IDomainModel
    {
        /// <summary>
        /// Entities compare by identity, not by attributes.
        /// </summary>
        /// <param name="other">The other entity.</param>
        /// <returns>true if the identities are the same, regardles of other attributes.</returns>
        bool SameIdentityAs(T other);
        int Id { get; }
    }
}