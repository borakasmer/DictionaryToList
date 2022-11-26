using BlogDictionaryToList.Enums;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogDictionaryToList.Model
{
    public class Email
    {
        public string EmailContent { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public SenderType SenderType { get; set; }
    }
}
