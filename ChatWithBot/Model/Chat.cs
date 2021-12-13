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

        public List<User> Users = new List<User>();
      
        public List<Message> ListMessage = new List<Message>();

        public Bot ChatBot { get; set; }

        public static List<Chat> Chats = new List<Chat>();
        public static List<Chat> GetAllChats()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Chat.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    List<Chat> deserilizeChat = (List<Chat>)formatter.Deserialize(fs);
                    return deserilizeChat;
                }
                catch
                {
                    Console.WriteLine("Чаты отсутсвуют");
                }
               
            }
            return null;
        }
    }
}
