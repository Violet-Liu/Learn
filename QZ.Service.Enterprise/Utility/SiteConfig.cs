using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace QZ.Service.Enterprise
{
    public class SiteConfig : ConfigurationSection
    {
        private static readonly ConfigurationProperty s_property = new ConfigurationProperty(string.Empty, typeof(SiteTheKeyValueCollection), null,
                                        ConfigurationPropertyOptions.IsDefaultCollection);

        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public SiteTheKeyValueCollection KeyValues
        {
            get
            {
                return (SiteTheKeyValueCollection)base[s_property];
            }
        }
    }



    public class SiteTheKeyValue : ConfigurationElement    // 集合中的每个元素
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return this["key"].ToString(); }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return this["value"].ToString(); }
            set { this["value"] = value; }
        }
    }

    public class TheKeyValue : ConfigurationElement    // 集合中的每个元素
    {
        [ConfigurationProperty("code", IsRequired = true)]
        public string Code
        {
            get { return this["code"].ToString(); }
            set { this["code"] = value; }
        }

        [ConfigurationProperty("msg", IsRequired = true)]
        public string Msg
        {
            get { return this["msg"].ToString(); }
            set { this["msg"] = value; }
        }
    }

    [ConfigurationCollection(typeof(TheKeyValue))]
    public class SiteTheKeyValueCollection : ConfigurationElementCollection        // 自定义一个集合
    {
        new public SiteTheKeyValue this[string name]
        {
            get { return (SiteTheKeyValue)base.BaseGet(name); }
        }
        // 下面二个方法中抽象类中必须要实现的。
        protected override ConfigurationElement CreateNewElement()
        {
            return new SiteTheKeyValue();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SiteTheKeyValue)element).Key;
        }
    }
}