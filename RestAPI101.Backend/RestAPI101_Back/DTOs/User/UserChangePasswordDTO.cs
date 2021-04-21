namespace RestAPI101_Back.DTOs
{
    public class UserChangePasswordDTO
    {
        public string OldPassword { get; }
        public string NewPassword { get; }

        public UserChangePasswordDTO(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }
}
