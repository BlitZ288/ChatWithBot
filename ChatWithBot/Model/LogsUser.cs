using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model
{
    /// <summary>
    /// Логирует вступление в чат и выход из него
    /// </summary>
    [Serializable]
     class LogsUser
    {
       public DateTime StartChat { get; set; }
       public DateTime? StopChat { get; set; }
    }
}
