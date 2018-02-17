using System.ComponentModel;

namespace aymk_library.Library.Util
{
    public enum aymkError
    {
        [Description("System Error")]
        GeneralError = 0,

        [Description("Item couldn't update")]
        UpdateError = 1,

        [Description("Item couldn't delete")]
        DeleteError = 2,

        [Description("Item couldn't add")]
        AddError = 3,

        [Description("Item couldn't get")]
        GetError = 4,

        [Description("Item list couldn't get")]
        GetListError = 5,

        [Description("Username or password is incorret")]
        UsernamePasswordWrong = 6,

        [Description("User not found")]
        UserNotFound = 7,

        [Description("User can not register")]
        RegisterError = 8,

        [Description("Email is already registered")]
        Register_Email_Exist = 9,

        [Description("Username is already registered")]
        Register_Username_Exist = 10,

        [Description("Account data is not valid")]
        Register_Not_Valid = 11,

    }

    public enum aymkMethodType
    {
        [Description("add")]
        Add,
        [Description("update")]
        Update,
        [Description("delete")]
        Delete,
        [Description("get")]
        Get,
        [Description("getbyid")]
        GetById,
        [Description("getlist")]
        Getlist,
        [Description("changestatus")]
        ChangeStatus,
        [Description("register")]
        Register,
        [Description("login")]
        Login,
        [Description("forgotpassword")]
        ForgotPassword,
        [Description("catalogs")]
        Catalogs,

    }

   
}