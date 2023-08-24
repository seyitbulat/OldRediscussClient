namespace RediscussMvcUI.Models
{
	public class ResponseBody<T>
	{
        public int StatusCode { get; set; }
        public List<string> ErrorMessages { get; set; }
        public T Data { get; set; }
    }
}
