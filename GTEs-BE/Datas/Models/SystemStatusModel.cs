namespace GTEs_BE.Datas.Models
{
    public static class SystemStatusModel
    {
        public static bool IsUserOnBoard {  get; set; } = false;

        public static bool IsCarLocked { get; set; } = true;

        public static bool IsUserAuthenticated { get; set; } = false;
    }
}
