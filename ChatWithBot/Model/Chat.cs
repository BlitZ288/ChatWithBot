using ChatWithBot.Interface;
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

    class Chat
    {
        public string Name { get; set; }

        public List<User> Users = new List<User>();
      
        public List<Message> ListMessage = new List<Message>();

        public List<LogAction> LogActions = new List<LogAction>();

        public Dictionary<string, LogsUser> ChatLogUsers = new Dictionary<string, LogsUser>();

        public List<IBot> ChatBot = new List<IBot>();
        public Chat(User user, string name)
        {
            Name = name;
            Users.Add(user);
            ChatLogUsers.Add(user.Name, new LogsUser() { StartChat = DateTime.Now, StopChat = null });
        }
        public Chat()
        {
        }

        public void AddLogChat(Chat chat , string eventChat, User user)
        {
            chat.LogActions.Add(new LogAction(DateTime.Now, eventChat, user.Name));          
        }
       
    }
}
