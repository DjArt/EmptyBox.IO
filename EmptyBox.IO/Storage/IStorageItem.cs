namespace EmptyBox.IO.Storage
{
    public interface IStorageItem
    {
        IStorageItem Path { get; }
        string Name { get; }
    }
}
