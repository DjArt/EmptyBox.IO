namespace EmptyBox.IO.Access
{
    public enum AccessStatus : byte
    {
        Success,
        DeniedBySystem,
        DeniedByUser,
        NotSupported,
        NotAvailable,
        UnknownError
    }
}
