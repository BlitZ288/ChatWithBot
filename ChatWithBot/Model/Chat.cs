using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model
{
    [Serializable]

    public class Chat
    {
        public string Name { get; set; }

        public List<User> Users;
      
        public List<Message> ListMessage = new List<Message>();
        public Dictionary<string, LogsUser> ChatLogUsers = new Dictionary<string, LogsUser>();

        public Bot ChatBot { get; set; }

        public static List<Chat> Chats = new List<Chat>();
       
    }
}
