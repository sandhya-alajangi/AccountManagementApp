namespace AccountManagementApp.Model.Enumerators
{
    /// <summary>
    /// Defines Delimiter Types
    /// </summary>
    public enum Delimiter : byte
    {
        NotSet = 0,
        Comma = 1,
        Semicolon = 2,
        Tab = 3,
        VerticalBar = 4,
        LineBreak = 5,
        CarriageReturn = 6,
        LineFeed = 7,
        None = 8
    }
}
