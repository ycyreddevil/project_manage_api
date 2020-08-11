using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_manage_api.Infrastructure;

namespace project_manage_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private static IHostingEnvironment _hostingEnvironment;

        public FileUploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="formFile">form表单文件流信息</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Response<string> fileUpload([FromForm]IFormFile formFile)
        {
            var currentDate = DateTime.Now;
            var webRootPath = _hostingEnvironment.WebRootPath;

            try
            {
                var filePath = $"/UploadFile/{currentDate:yyyyMMdd}/";

                //创建每日存储文件夹
                if (!Directory.Exists(webRootPath + filePath))
                    Directory.CreateDirectory(webRootPath + filePath);

                if (formFile != null)
                {
                    //文件后缀
                    var fileExtension = Path.GetExtension(formFile.FileName);//获取文件格式，拓展名

                    //判断文件大小
                    var fileSize = formFile.Length;

                    if (fileSize > 1024 * 1024 * 10) //最大限制10M
                        return new Response<string> { Code = 500, Message = "上传的文件不能大于10M！" };

                    //保存的文件名称(以名称和保存时间命名)
                    var saveName = formFile.FileName.Substring(0, formFile.FileName.LastIndexOf('.')) + "_" + currentDate.ToString("HHmmss") + fileExtension;

                    //文件保存
                    using (var fs = System.IO.File.Create(webRootPath + filePath + saveName))
                    {
                        formFile.CopyTo(fs);
                        fs.Flush();
                    }

                    //完整的文件路径
                    var completeFilePath = Path.Combine(filePath, saveName);

                    return new Response<string> { Code = 200, Message = "上传成功！", Result = completeFilePath};
                }
                else
                {
                    return new Response<string> { Code = 500, Message = "上传失败，未检测上传的文件信息！" };
                }

            }
            catch (Exception ex)
            {
                return new Response<string> { Code = 500, Message = ex.Message };
            }
        }
    }
}