namespace API.Base.Core.Models
{
    public static class ValidationMessage
    {
        private const string ALREADYEXISTS = "{0}, alreay exists";
        public static string AlreadyExists(object param) => string.Format(ALREADYEXISTS, param);

        private const string NOTEMPTY = "{0}, cannot be empty";
        public static string NotEmpty(object param) => string.Format(NOTEMPTY, param);

        private const string NOTVALID = "{0} is not valid";
        public static string NotValid(object param) => string.Format(NOTVALID, param);
    }
}