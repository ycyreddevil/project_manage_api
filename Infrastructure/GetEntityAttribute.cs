﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Infrastructure
{
    /// <summary>
    /// 获取实体类中的属性名和注解
    /// </summary>
    public static class GetEntityAttribute
    {

        public static List<KeyDescription> getEntityAttribute<T>(T t)
        {
            var result = new List<KeyDescription>();

            if (t == null)
                return null;

            var properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            if (properties == null || properties.Length <= 0)
                return null;

            foreach (var item in properties)
            {
                var itemName = item.Name;
                if (itemName == "Id")
                    continue;
                var desc_attribute = Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute));
                var desc = desc_attribute == null ? "" : ((DescriptionAttribute)desc_attribute).Description;
                
                var browsable_attribute = Attribute.GetCustomAttribute(item, typeof(BrowsableAttribute));
                var browsable = browsable_attribute == null ? true : ((BrowsableAttribute)browsable_attribute).Browsable;
                
                if (!browsable)
                    continue;
                
                var temp = new KeyDescription
                {
                    Key = itemName,
                    Description = desc,
                    Browsable = browsable
                };

                result.Add(temp);
            }

            return result;
        }


        public static List<KeyDescription> SetNoShowingForNullDescreption(List<KeyDescription> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (string.IsNullOrEmpty(list[i].Description))
                    list[i].Browsable = false;
                else
                    list[i].Browsable = true;
            }
            return list;
        }
    }

    
}
