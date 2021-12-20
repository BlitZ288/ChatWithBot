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
        public User user;

        public static Message CreatMessage(string content, string nameSend,Chat chat,User user)
        {
            Message message = new Message();       
            var Senduser = chat.Users.FirstOrDefault(c => c.Name.Equals(nameSend));
            var SendBot = chat.ChatBot.FirstOrDefault(c => c.NameBot.Equals(nameSend));


            if (Senduser != null || SendBot != null)
            {
                if (chat.ListMessage.Any())
                {
                    message.IdMessage= chat.ListMessage[^1].IdMessage+1;
                }
                else
                {
                    message.IdMessage = 0;
                }
                message.Content = content;
                message.dateTime = DateTime.Now;
                message.OutUser = Senduser==null ? SendBot.NameBot : Senduser.Name;
                message.user = user;
            }
            else
            {
                Console.WriteLine("Этого пользователя нет в чате");
                return null;
            }
            return message;

        }
    }
}
