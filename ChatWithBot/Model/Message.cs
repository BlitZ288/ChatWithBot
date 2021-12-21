using System;
using System.Linq;

namespace ChatWithBot.Model
{
    [Serializable]
     class Message
    {
        public int IdMessage { get; set; }
        public string Content { get; set; }
        public DateTime dateTime { get; set; }
        public string OutUser { get; set; }
        public User User;

        public Message(string content, string nameSend, Chat chat, User user)
        {
            var Senduser = chat.Users.FirstOrDefault(c => c.Name.Equals(nameSend));
            var SendBot = chat.ChatBot.FirstOrDefault(c => c.NameBot.Equals(nameSend));

            if (Senduser != null || SendBot != null)
            {
                if (chat.ListMessage.Any())
                {
                    IdMessage = chat.ListMessage[^1].IdMessage + 1;
                }
                else
                {
                    IdMessage = 0;
                }
                Content = content;
                dateTime = DateTime.Now;
                OutUser = Senduser == null ? SendBot.NameBot : Senduser.Name;
                User = user;
            }
            else
            {
                throw new ArgumentNullException("Этого пользователя нет в чате");   
            } 
        }
        public Message()
        {

        }
    }
}
