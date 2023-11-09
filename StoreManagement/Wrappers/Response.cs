namespace StoreManagement.Wrappers;
public class Response<T>
{
    public Response()
    {
    }
    public Response(T data)
    {
        Succeeded = true;
        Message = string.Empty;
        Errors = null;
        Data = data;
    }

    public Response(string status, string message)
    {
        Succeeded = true;
        Message = message;
        Errors = null;
        Status = status;
        Data = default(T);
    }
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    public string[] Errors { get; set; }
    public string Message { get; set; }
    public string Status { get; set; }
}