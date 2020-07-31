﻿using System.Collections.Generic;

namespace project_manage_api.Model
{
    public class TreeItem<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Children { get; set; }
    }
    
    public class TreeItem
    {
        public int id { get; set; }
        
        public string label { get; set; }

        public string chargeUserName { get; set; }
        
        public string status { get; set; }

        public IEnumerable<TreeItem> children { get; set; }
    }
}