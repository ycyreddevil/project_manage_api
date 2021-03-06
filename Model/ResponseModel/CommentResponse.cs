﻿using System.Collections.Generic;

namespace project_manage_api.Model
{
    public class CommentResponse
    {
        public int id { get; set; }
        public CommentUserResponse commentUser { get; set; }
        public CommentUserResponse targetUser { get; set; }
        public string content { get; set; }
        public string createDate { get; set; }
        public List<CommentResponse> childrenList { get; set; }
    }

    public class CommentUserResponse
    {
        public int id { get; set; }
        public string nickName { get; set; }
        public string avatar { get; set; }
    }
}