using ChatWithBot.Interface;
using ChatWithBot.Model;
using ChatWithBot.Model.Bots;
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
        public static void CreatUser(List<User> users)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Users.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, users);
            }

        }
        public static List<IBot> GetAllBot()
        {
            List<IBot> Bots = new List<IBot>()
            {
                new BotAnecdote(),
                new BotTime()

            };
            return Bots;
        }
        //public static void WriterBinarFile(List<Chat> chat)
        //{
        //    try
        //    {
        //        // создаем объект BinaryWriter
        //        using (BinaryWriter writer = new BinaryWriter(File.Open("Chat.dat", FileMode.OpenOrCreate)))
        //        {
        //            // записываем в файл значение каждого поля структуры
        //            foreach ( var c in chat)
        //            {
        //                writer.Write("~");
        //                writer.Write(c.Name);
        //                writer.Write("-");
        //                foreach (var u in c.Users)
        //                {
        //                    writer.Write(u.Name);
        //                }
        //                writer.Write("-");
        //                foreach(var mes in c.ListMessage)
        //                {
        //                    writer.Write(mes.IdMessage);
        //                    writer.Write(mes.dateTime.ToString());
        //                    writer.Write(mes.user.Name);
        //                    writer.Write(mes.OutUser);
        //                    writer.Write(mes.Content);
        //                }
        //                writer.Write("-");
        //                for(int i =0; i<c.)
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}
            
    }
}
