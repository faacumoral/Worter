namespace Worter
{
    public class GetRequest
    {
        public string Url { get; set; }
        public bool CheckAuth { get; set; } = true;

        public GetRequest(string url)
        {
            Url = url;
        }

        public GetRequest(string url, bool checkauth) : this(url)
        {
            CheckAuth = checkauth;
        }
    }
}
