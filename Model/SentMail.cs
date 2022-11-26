using BlogDictionaryToList.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDictionaryToList.Model
{
    public  class SentMail
    {
        public string Email { get; set; }
        public string MailId { get; set; }
        public int MailType { get; set; }
        public string FolderName { get; set; }
        public string From { get; set; }
        public string Konu { get; set; }
        public string SenderName { get; set; }        
        public DateTime PostTime { get; set; }
    
        [JsonData]
        public string Recipient { get; set; }
    }
}
