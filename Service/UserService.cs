using System.Collections.Generic;
using System.Linq;
using NPinyin;
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
    }
}