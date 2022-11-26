using System;
using System.Collections.Generic;
using System.Text;

namespace BlogDictionaryToList.CustomAttribute
{
    [AttributeUsage(AttributeTargets.All)]
    public class JsonData : Attribute
    {
        public JsonData()
        {
        }
    }
}
