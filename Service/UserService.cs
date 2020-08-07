using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using NPinyin;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using SqlSugar;

namespace project_manage_api.Service
{
    public class UserService : SugarDBContext<Users>
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ICacheContext _cacheContext;
        private UserAuthSession user;
        
        public UserService(IHttpContextAccessor httpContextAccessor, ICacheContext cacheContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _cacheContext = cacheContext;

            string token = _httpContextAccessor.HttpContext.Request.Headers[Define.TOKEN_NAME];
            user = _cacheContext.Get<UserAuthSession>(token);
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Users> findUsers(string key)
        {
            var list = SimpleDb.AsQueryable().Where(u => u.userName.Contains(key)).ToList();

            if (list.Count != 0)
                return list;
            // 如果不是字符串包含 则按拼音首字母或者全拼来进行搜索
            var totalList = SimpleDb.AsQueryable().ToList();

            list.AddRange(totalList.Where(user =>
                Pinyin.GetInitials(user.userName).ToLower().StartsWith(key) ||
                Pinyin.GetPinyin(user.userName).Replace(" ", "").StartsWith(key)));

            return list;
        }

        /// <summary>
        /// 找到登录用户的所有上级
        /// </summary>
        /// <returns></returns>
        public List<Users> findApproverList()
        {
            var userDepartmentPostList = Db.Queryable<UserDepartmentPost>().Where(u => u.userId == user.UserId).ToList();

            var result = new List<Users>();

            foreach (var temp in userDepartmentPostList)
            {
                if (temp.isHead == 1)
                {
                    // 则找到上级部门的负责人
                    var list = Db.Queryable<Department, UserDepartmentPost, Users>((d, udp, u) => new object[]
                    {
                        JoinType.Left, d.parentId == udp.departmentId, JoinType.Left, udp.userId == u.userId
                    }).Where((d, udp, u) => d.Id == temp.departmentId && udp.isHead == 1).Select<Users>().ToList();
                    result.AddRange(list);
                }
                else
                {
                    // 则找本部门的负责人
                    var selfDepId = temp.departmentId;
                    var list = Db.Queryable<UserDepartmentPost, Users>((udp, u) => new object[]
                    {
                        JoinType.Left, udp.userId == u.userId
                    }).Where((udp,u) => udp.departmentId == selfDepId && udp.isHead == 1).Select<Users>().ToList();
                    result.AddRange(list);
                }
            }
            
            return result;
        }
    }
}