using System;
using Microsoft.AspNetCore.Http;
using project_manage_api.Infrastructure;
using project_manage_api.Model;

namespace project_manage_api.Service
{
    public class LoginService : SugarDBContext<Users>
    {
        private ICacheContext _cacheContext;
        private IHttpContextAccessor _httpContextAccessor;

        public LoginService(ICacheContext cacheContext,IHttpContextAccessor httpContextAccessor)
        {
            _cacheContext = cacheContext;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 用户名密码登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Response login(string username, string password)
        {
            var result = new Response();
            try
            {
                // 密码加密
                password = Md5.Encrypt(password);
                // 防sql注入
                username = Md5.avoidSqlInjection(username);

                var userInfo = SimpleDb.GetSingle(u => u.userName.Equals(username));

                if (username == null)
                    throw new Exception("用户不存在");
                if (!Md5.Encrypt(userInfo.passWord).Equals(password))
                    throw new Exception("密码不正确");

                var currentSession = new UserAuthSession
                {
                    UserId = userInfo.userId,
                    WechatUserId = userInfo.wechatUserId,
                    UserName = userInfo.userName,
                    Token = Guid.NewGuid().ToString().GetHashCode().ToString("x"),
                    CreateTime = DateTime.Now
                };

                _cacheContext.Set(currentSession.Token, currentSession, DateTime.Now.AddDays(10));
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.Message;
            }
            
            return result;
        }

        /// <summary>
        /// 检查token是否有效
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool CheckToken(string token = "")
        {
            if (string.IsNullOrEmpty(token))
                token = GetToken();

            if (string.IsNullOrEmpty(token))
                return false;
            
            try
            {
                var result = _cacheContext.Get<UserAuthSession>(token) != null;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private string GetToken()
        {
            string token = _httpContextAccessor.HttpContext.Request.Query[Define.TOKEN_NAME];
            if (!string.IsNullOrEmpty(token)) return token;

            token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            if (!string.IsNullOrEmpty(token)) return token;

            var cookie = _httpContextAccessor.HttpContext.Request.Cookies[Define.TOKEN_NAME];
            return cookie ?? string.Empty;
        }
    }
}