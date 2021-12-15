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
    class User
    {
        public string Name { get; set; }
        /// public string ListMessage { get; set; }
        public void CreatChat(List<Chat> chat)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("Chat.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, chat);
            }
        }
        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns></returns>
        public static List<User> GetUsers()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Users.dat", FileMode.OpenOrCreate))
            {
                List<User> deserilizeUser = (List<User>)formatter.Deserialize(fs);

                return deserilizeUser;
            }

        }
        //public void SendChat(Chat chat, User user,string NameSend, int indexmesseg, bool flagNewChat)
        //{
        //    //string NameSend = choice[1].Replace("@", "");
        //    //Console.WriteLine("Сообщение:");
        //    string Content = Console.ReadLine().Trim();
        //    var Senduser = chat.Users.Where(c => c.Name == NameSend).FirstOrDefault();
        //    if (Senduser != null)
        //    {
        //        if (!flagNewChat)
        //        {
        //            indexmesseg = chat.ListMessage[^1].IdMessage;
        //        }
        //        indexmesseg++;

        //        Message message = new Message()
        //        {
        //            IdMessage = indexmesseg,
        //            Content = Content,
        //            dateTime = DateTime.Now,
        //            OutUser = Senduser,
        //            user = user
        //        };
        //        chat.ListMessage.Add(message);
        //        Console.WriteLine($"id={message.IdMessage} {user.Name} {message.dateTime} ({Senduser.Name}): {message.Content}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Этого пользователя нет в чате");
        //    }
        //}
        public User(string Name)
        {
            this.Name = Name;

        }
        public User()
        {

        }
    }
}
