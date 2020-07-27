using System.Collections.Generic;
using project_manage_api.Infrastructure;
using project_manage_api.Model;
using SqlSugar;

namespace project_manage_api.Service
{
    public class UserService : SugarDBContext<Users>
    {
        public UserService()
        {
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<Users> findUsers(string key)
        {
            return SimpleDb.AsQueryable().Where(u => u.userName.Contains(key) || getPinYinFirstLetter(u.userName).Contains(key)).ToList();
        }
    }
}