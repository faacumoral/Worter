namespace Worter
{
    public class PostRequest<T>
    {
        public string Url { get; set; }
        public T Parameter { get; set; }
        public bool CheckAuth { get; set; } = true;

        public PostRequest(string url)
        {
            Url = url;
        }

        public PostRequest(string url, T parameter) : this(url)
        {
            Parameter = parameter;
        }

        public PostRequest(string url, bool checkauth) : this(url)
        {
            CheckAuth = checkauth;
        }

        public PostRequest(string url, T parameter, bool checkAuth):this(url, parameter)
        {
            CheckAuth = checkAuth;
        }
    }
}
