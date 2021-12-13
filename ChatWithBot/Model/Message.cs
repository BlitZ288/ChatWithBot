using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model
{
    [Serializable]
    public class Message
    {
       public int IdMessage { get; set; }
        public string Content { get; set; }
        public DateTime dateTime { get; set; }
        public User user = new User();
    }
}
