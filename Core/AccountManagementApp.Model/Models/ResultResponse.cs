namespace AccountManagementApp.Model.Models
{
    public class ResultResponse : ResultCounter
    {
        public bool Success { get; set; }

        public string ErrorCode { get; set; }

        public string Error { get; set; }

        
    }
}
