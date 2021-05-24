namespace RestAPI101.Domain.Enums
{
    public enum TodoIncludeMode : byte
    {
        All,
        Done,
        Undone
    }

    public enum TodoFilterLabelsMode : byte
    {
        Union,
        Intersection
    }
}
