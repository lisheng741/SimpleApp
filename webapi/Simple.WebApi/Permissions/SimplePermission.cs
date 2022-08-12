namespace Simple.WebApi.Permissions;

public class SimplePermission
{
    public const string Default = "Simple";

    public class Account
    {
        public const string Default = "Simple:Account";
        public const string Insert = "Simple:Account:Insert";
    }
}
