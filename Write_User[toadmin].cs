using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
class Class1
{
    public static bool CreateLocalWindowsAccount(string username, string password, string displayName, string description, bool canChangePwd, bool pwdExpires)
    {
        try
        {
            PrincipalContext context = new PrincipalContext(ContextType.Machine);
            UserPrincipal user = new UserPrincipal(context);
            user.SetPassword(password);
            user.DisplayName = displayName;
            user.Name = username;
            user.Description = description;
            user.UserCannotChangePassword = canChangePwd;
            user.PasswordNeverExpires = pwdExpires;
            user.Save();
            //now add user to "Users" group so it displays in Control Panel
            GroupPrincipal group = GroupPrincipal.FindByIdentity(context, "Administrators");
            group.Members.Add(user);
            group.Save();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error creating account: {0}", ex.Message);
            return false;
        }

    }

    static void Main(string[] args)
    {
        string username = "tester12";
        string password = "whodunit";
        string displayName = "tester12";
        string description = "testacct";
        CreateLocalWindowsAccount(username,password,displayName,description,true,true);
    }
}