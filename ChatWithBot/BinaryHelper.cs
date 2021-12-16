﻿using ChatWithBot.Interface;
using ChatWithBot.Model;
using ChatWithBot.Model.Bots;
using System.Collections.Generic;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace ChatWithBot
{
    class BinaryHelper
    {
        public static void CreatChat(List<Chat> chat)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            
            using (FileStream fs = new FileStream("Chat.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, chat);
            }
        }
        public static List<Chat> GetAllChats()
        {
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
            
    }
}
