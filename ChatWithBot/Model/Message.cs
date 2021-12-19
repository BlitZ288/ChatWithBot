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

       public static Message CreatMessage(int index ,string content, string nameSend,Chat chat,User user)
        {
            Message message = new Message();           
            var Senduser = chat.Users.Where(c => c.Name.Equals(nameSend)).FirstOrDefault();
            message.IdMessage = index;
            if (Senduser != null)
            {
                message.Content = content;
                message.dateTime = DateTime.Now;
                message.OutUser = Senduser.Name;
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
