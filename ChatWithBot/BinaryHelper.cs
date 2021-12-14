using ChatWithBot.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot
{
    class BinaryHelper
    {

        public static void CreatChat(List<Chat> chat)
        {

            BinaryFormatter formatter = new BinaryFormatter();
            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream("Chat.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, chat);
            }
        }
        public static List<Chat> GetAllChats()
        {
            //var fa = System.Reflection.Assembly.GetEntryAssembly().Location;
            List<Chat> deserilizeChat = new List<Chat>();
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Chat.dat", FileMode.OpenOrCreate))
            {
                try
                {
                    deserilizeChat = (List<Chat>)formatter.Deserialize(fs);
                    return deserilizeChat;
                }
                catch
                {
                    return deserilizeChat;
                }
            }
           
        }
        public static List<User> GetUsers()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Users.dat", FileMode.OpenOrCreate))
            {
                List<User> deserilizeUser = new List<User>();
                try
                {
                   deserilizeUser = (List<User>)formatter.Deserialize(fs);
                    return deserilizeUser;
                }
                catch (SerializationException e)
                {
                    return deserilizeUser;
                }

            }

        }
        public static void CreatUser(User user)
        {
            List<User> TempUserList = new List<User>() { user };
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Users.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, TempUserList);
            }

        }

    }
}
