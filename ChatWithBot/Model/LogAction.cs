using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model
{
    /// <summary>
    /// Логирует действия в чате 
    /// </summary>
    [Serializable]
    class LogAction
    {
       private DateTime FixLog { get; set; }
       private string Content { get; set; }
       private string NameUser { get; set; }
        public LogAction(DateTime fixLog, string content,string nameUser)
        {
            FixLog = fixLog;
            Content = content;
            NameUser = nameUser;
        }
    }
}
