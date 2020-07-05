namespace Worter.Models
{
    public class APIRequest<T>
    {
        public string Url { get; set; }
        public T Parameter { get; set; }
        public bool CheckAuth { get; set; } = true;

        public APIRequest(string url)
        {
            Url = url;
        }

        public APIRequest(string url, T parameter) : this(url)
        {
            Parameter = parameter;
        }

        public APIRequest(string url, bool checkauth) : this(url)
        {
            CheckAuth = checkauth;
        }

        public APIRequest(string url, T parameter, bool checkAuth):this(url, parameter)
        {
            CheckAuth = checkAuth;
        }
    }
}
