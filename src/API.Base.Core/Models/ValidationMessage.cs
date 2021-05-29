namespace API.Base.Core.Models
{
    public static class ValidationMessage
    {
        private const string ALREADYEXISTS = "{0}, alreay exists";

        private const string NOTEMPTY = "{0}, cannot be empty";

        private const string NOTVALID = "{0} is not valid";
        public static string AlreadyExists(object param) => string.Format(ALREADYEXISTS, param);
        public static string NotEmpty(object param) => string.Format(NOTEMPTY, param);
        public static string NotValid(object param) => string.Format(NOTVALID, param);
    }
}