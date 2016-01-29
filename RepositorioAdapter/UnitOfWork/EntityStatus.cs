namespace RepositorioAdapter.UnitOfWork
{
    public enum EntityStatus : int
    {
        Added,
        Deleted,
        Detached,
        Modified,
        Unchanged
    }
}