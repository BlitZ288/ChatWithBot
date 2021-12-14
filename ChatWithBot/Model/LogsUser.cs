using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model
{
    [Serializable]
    public class LogsUser
    {
       public DateTime StartChat { get; set; }
       public DateTime? StopChat { get; set; }
    }
}
