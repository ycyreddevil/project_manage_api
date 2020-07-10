using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using SqlSugar;

namespace project_manage_api.Service
{
    public class LoginService : SugarDBContext<Users>
    {
        private ICacheContext _cacheContext;
        private IHttpContextAccessor _httpContextAccessor;

        public LoginService(ICacheContext cacheContext, IHttpContextAccessor httpContextAccessor)
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
        public Response Login(string username, string password)
        {
            var result = new Response<string>();
            try
            {
                // 密码加密
                password = Md5.Encrypt(password);
                // 防sql注入
                username = Md5.avoidSqlInjection(username);

                var userInfo = SimpleDb.GetSingle(u => u.userName.Equals(username));

                if (userInfo == null)
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
                result.Result = currentSession.Token;
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

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        private string GetToken()
        {
            string token = _httpContextAccessor.HttpContext.Request.Query[Define.TOKEN_NAME];
            if (!string.IsNullOrEmpty(token)) return token;

            token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            if (!string.IsNullOrEmpty(token)) return token;

            var cookie = _httpContextAccessor.HttpContext.Request.Cookies[Define.TOKEN_NAME];
            return cookie ?? string.Empty;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
                return true;

            try
            {
                _cacheContext.Remove(token);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 通过token获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Dictionary<string, object> getInfo(string token)
        {
            var result = _cacheContext.Get<UserAuthSession>(token);

            // 获取用户角色
            var roleList = Db.Queryable<UserRole, Role>((ur, r) =>
                    new object[] {JoinType.Left, ur.RoleId == r.Id}).Where(ur => ur.UserId == result.UserId)
                .Select((ur, r) => new {r.Name}).ToList();

            // 获取用户头像
            var avatar = SimpleDb.GetSingle(u => u.userId == result.UserId).avatar;

            return new Dictionary<string, object>
            {
                {"name", result.UserName},
                {"userId", result.UserId},
                {"wechatUserId", result.WechatUserId},
                {"avatar", avatar},
                {"role", roleList}
            };
        }

        /// <summary>
        /// 获取用户可访问的菜单
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IEnumerable<TreeItem<Module>> getModulesTree(string token)
        {
            var result = _cacheContext.Get<UserAuthSession>(token);
            var moduleList = Db.Queryable<UserRole, RoleModule, Module>((ur, rm, m) =>
                    new object[] {JoinType.Left, ur.RoleId == rm.RoleId && rm.ModuleId == m.Id})
                .Where(ur => ur.UserId == result.UserId).Select<Module>().ToList();
            var moduleTree = moduleList.GenerateTree(u => u.Id, u => u.ParentId);
            return moduleTree;
        }
    }
}