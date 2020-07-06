namespace project_manage_api.Infrastructure
{
    public class Response
    {
        public string Message { get; set; }
        
        public int Code { get; set; }

        public Response()
        {
            Message = "success";
            Code = 200;
        }
    }

    public class Response<T> : Response
    {
        /// <summary>
        /// 回传的结果
        /// </summary>
        public T Result { get; set; }
    }
}