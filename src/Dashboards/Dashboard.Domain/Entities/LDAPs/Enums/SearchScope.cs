namespace Dashboard.Domain.Entities.LDAPs.Enums;

public enum SearchScope
{
    /// <summary>Search only the specified base object. The value is equal to 0.</summary>
    Base = 0,

    /// <summary>Search the child objects of the base object, but not the base object itself. The value is equal to 1.</summary>
    OneLevel = 1,

    /// <summary>Search the base object and all child objects. The value is equal to 2.</summary>
    Subtree = 2,
}