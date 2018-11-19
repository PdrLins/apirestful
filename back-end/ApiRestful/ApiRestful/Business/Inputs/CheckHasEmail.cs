namespace DesafioPitang.Business
{
    public class CheckHasEmail
    {
        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value?.ToUpper().Trim(); }
        }

    }
}
