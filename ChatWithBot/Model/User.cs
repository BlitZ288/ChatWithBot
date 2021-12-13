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
    public class User:IUser
    {
        public string Name { get; set; }
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
        public static void CreatUser(List<User> user)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Users.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, user);
            }

        }

        public void DeleteChat()
        {
            throw new NotImplementedException();
        }

        public void DeleteMessage()
        {
            throw new NotImplementedException();
        }

        public void ExitChat()
        {
            throw new NotImplementedException();
        }

        public void ReadMessage()
        {
            throw new NotImplementedException();
        }

        public void SendMessage()
        {
            throw new NotImplementedException();
        }
        public User(string Name)
        {
            this.Name = Name;

        }
        public User()
        {

        }
    }
}
