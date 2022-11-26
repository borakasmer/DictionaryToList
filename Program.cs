using BlogDictionaryToList;
using BlogDictionaryToList.Enums;
using BlogDictionaryToList.Model;

List<Dictionary<string, object>> dictList = new List<Dictionary<string, object>>();
List<Email> Recipients = new List<Email>();
Recipients.Add(new Email() { EmailContent = "Test Email", SenderType = SenderType.To });
Recipients.Add(new Email() { EmailContent = "Test Email 2", SenderType = SenderType.Cc });

for (var i = 0; i < 10000; i++)
{
    Dictionary<string, object> dict = new Dictionary<string, object>();
    var uniqueKey = $"bora@borakasmer.com{i}" + "|" + 2 + "|" + 653 + i;
    dict.Add("uniqueKey", uniqueKey);
    dict.Add("Email", $"bora.kasmer@keepnetlabs.com{i}");  
    dict.Add("MailId", 653 + i);
    dict.Add("MailType", MailType.SentItems);
    dict.Add("FolderName", $"Bora's Folder{i}");
    dict.Add("From", $"Bora{i}");
    dict.Add("Subject", $"Test Converter{i}");
    dict.Add("SenderName", $"Bora{i}");    
    dict.Add("PostTime", DateTime.Now);
    dict.Add("Recipient", Recipients);
    if (dictList.Any(dic => dic["uniqueKey"].ToString() == uniqueKey))
    {
        var dic = dictList.First(dic => dic["uniqueKey"].ToString() == uniqueKey);
        dictList.Remove(dic);
    }
    dictList.Add(dict);
}

var columnMatchTable = new Dictionary<string, string>() {
                                    {"Subject", "Konu" }
                                };
for (var s = 0; s < 5; s++)
{
    var list = dictList.DictionaryToList<SentMail>(columnMatchTable);    
}

int i2 = 0;
