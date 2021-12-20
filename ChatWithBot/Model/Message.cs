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
            if (Senduser != null)
            {
                message.IdMessage = index;
                message.Content = content;
                message.dateTime = DateTime.Now;
                message.OutUser = Senduser.Name;
                message.user = user;
            }
            else
            {
                Console.WriteLine("Этого пользователя нет в чате");
            }
            return message;

        }
    }
}
