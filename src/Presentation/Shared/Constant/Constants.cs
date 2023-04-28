using System.Net;

namespace BlazorEcommerce.Shared.Constant
{
    public static class Constants
    {
        public static string AdminEmail => "admin@admin.com";
        public static string AdminName => "Admin";
        public static string AdminRoleName => "Admin";
    }

    public static class Messages
    {
        public static string UnableToLogin => "Unable to login. Wrong Username or Password";
        public static string Success => "Success";
        public static string RegisteredSuccesfully => "Registered Successfully";
        public static string AddedSuccesfully => "Added Successfully";
        public static string AddedFailed => "Added Failed";
        public static string UpdatedSuccessfully => "Updated Successfully";
        public static string DeletedSuccessfully => "Deleted Successfully";
        public static string InvalidCredentials => "Invalid credentials endtered.";
        public static string DataFound => "Data found";
        public static string NoDataFound => "No Data found";
        public static string IssueWithData => "There is some issue with the data";
        public static string CheckCredentials => "Please check login credentials";
        public static string UserNameOrPasswordIsIncorrect => "Username or password is incorrect";
        public static string ConfirmYourEmail => "Please confirm your email";
        public static string PasswordDontMatchWithConfirmation => "Password doesn't match its confirmation";
        public static string RegisterSuccessfully => "Register successfuly please look at your mail box for account confirmation.";
        public static string NotFound => "{0} Not Found";
        public static string NotExist => "{0} does not exist";
        public static string AlreadyEmailConfirmed => "Already your email confirmed";
        public static string SuccessfullyEmailConfirmed => "Email confirmed successfully.You can login now";
        public static string RefreshTokenExpired => "Refresh Token Expired";
        public static string CurrentPasswordIsFalse => "Current Password is false";
        public static string IfEmailTrue => "If your email address is entered correctly, you will receive a link to reset your password.";
        public static string PasswordSuccessfullyReset => "Your password has been successfully reset.Your new password has been sent to your email address.We recommend that you change your password.";
        public static string ResetPasswordCodeInvalid => "Your Reset Password Code is invalid";
        public static string EmailSuccessfullyChangedConfirmYourEmail => "Email Successfully Changed.Please confirm your email";
        public static string TokenNotExpired => "Current token is not expired yet.";
        public static string PasswordChangedSuccess => "Password has been changed.";
        public static string IsRequired => "{0} is required";
        public static string AlreadyExists => "{0} already exist";
        public static string MaxCharLimit => "{0} is exceeding the limit of {1} characters.";

        public static string CartIte => "{0} is exceeding the limit of {1} characters.";
    }

    public static class HttpStatusCodes
    {
        //200
        public static int Accepted => (int)HttpStatusCode.Accepted;

        //400
        public static int BadRequest => (int)HttpStatusCode.BadRequest;

        //401
        public static int Unauthorized => (int)HttpStatusCode.Unauthorized;

        //403
        public static int Forbidden => (int)HttpStatusCode.Forbidden;

        //404
        public static int NotFound => (int)HttpStatusCode.NotFound;

        //500
        public static int InternalServerError => (int)HttpStatusCode.InternalServerError;
        
    }
}
