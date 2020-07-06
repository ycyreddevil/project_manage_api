﻿using System;

namespace project_manage_api.Infrastructure
{
    [Serializable]
    public class UserAuthSession
    {
        /// <summary>
        /// 用户token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户userId
        /// </summary>
        public int UserId { get; set; }
        
        /// <summary>
        /// 用户wechatUserId
        /// </summary>
        public string WechatUserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        
        public DateTime CreateTime { get; set; }
    }
}