namespace ApiRestful.Business
{
    public class FindUser
    {
        public int? Id { get; set; }
        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = Utils.Utils.Instance.GenerateMd5(value?.Trim());
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value?.ToUpper().Trim(); }
        }
    }
}
