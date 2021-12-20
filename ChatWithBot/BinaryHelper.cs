using ChatWithBot.Interface;
using ChatWithBot.Model;
using ChatWithBot.Model.Bots;
using System;
using System.Collections.Generic;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace ChatWithBot
{
    class BinaryHelper : IContext
    {
        public void CreatChat(List<Chat> chat)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("Chat.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, chat);
            }
        }
        public List<Chat> GetAllChats()
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
        public List<User> GetUsers()
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
        public void CreatUser(List<User> users)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("Users.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, users);
            }

        }
        public List<IBot> GetAllBot()
        {
            List<IBot> Bots = new List<IBot>()
            {
                new BotAnecdote(),
                new BotTime()

            };
            return Bots;
        }

        public static bool AddChat(Chat chats)
        {
                using (BinaryWriter writer = new BinaryWriter(File.Open("ChatT.dat", FileMode.Append)))
                {
                        writer.Write("~");
                        writer.Write(chats.Name);
                        writer.Write("end");
                        foreach (var u in chats.Users)
                        {
                            writer.Write(u.Name);               
                        }
                        writer.Write("end");
                        foreach (var l in chats.ChatLogUsers)
                        {
                            writer.Write(l.Key);
                            writer.Write(l.Value.StartChat.ToString());
                            writer.Write(l.Value.StopChat.ToString());
                        }
                        writer.Write("end");
                        foreach (var b in chats.ChatBot)
                        {
                            writer.Write(b.NameBot);
                        }
                        writer.Write("end");
                        foreach (var m in chats.ListMessage)
                        {
                            writer.Write(m.IdMessage.ToString());
                            writer.Write(m.Content);
                            writer.Write(m.dateTime.ToString());
                            writer.Write(m.user.Name);
                            writer.Write(m.OutUser);
                        }
                        writer.Write("end");
                    
                }
            return false;
        }
        public static bool AddUser(Chat chat , User user)
        {
            int count = 0;
            var file = File.Open("ChatT.dat", FileMode.Open, FileAccess.ReadWrite);
            using (BinaryReader reader = new BinaryReader(file))
            {
                while (reader.PeekChar() > -1)
                {
                    string r = reader.ReadString();
                    if (r.Equals("~"))
                    {
                        string nameChat = reader.ReadString();
                        if (nameChat.Equals(chat.Name))
                        {
                            while (true)
                            {
                                string end = reader.ReadString();
                                if (end.Equals("end"))
                                {
                                    count++;
                                    if (count == 2)
                                    {
                                        using (BinaryWriter writer = new BinaryWriter(file))
                                        {
                                            writer.Seek(-2, SeekOrigin.Current);
                                            var d = reader.ReadChar();
                                            writer.Write(user.Name);
                                       
                                             string temp2 = reader.ReadString();
                                             //writer.Write(end);
                                             //writer.Write(temp2);
                                            end = reader.ReadString();
                                             //writer.Write(end);
                                             end = reader.ReadString();
                                             //writer.Write(end);
                                             end = reader.ReadString();
                                             //writer.Write(end);
                                             end = reader.ReadString();
                                             end = reader.ReadString();
                                            
                                            //string temp = end;
                                            //var temp2 = reader.ReadString();
                                            //writer.Write(temp);
                                            //temp = reader.ReadString();
                                            //OverWrite(writer, reader, temp);
                                        }
                                        return true;
                                    }
                                }
                            }
                        }

                    }
                }
                return false;
            }
        }
        private static void OverWrite(BinaryWriter writer,BinaryReader reader,string temp)
        {
            
            while (reader.PeekChar() > -1)
            {
                var temp2 = reader.ReadChar();
                try
                {
                  
                    //writer.Write(temp);
                    //temp = reader.ReadString();
                    //writer.Write(temp2);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static List<Chat> GetChatts()
        {
            List<Chat> chats = new List<Chat>();
            using (BinaryReader reader = new BinaryReader(File.Open("ChatT.dat", FileMode.Open)))
            { 
                while (reader.PeekChar() > -1) {
                    string r = reader.ReadString();
                    if (r.Equals("~"))
                    {
                        string namechat = reader.ReadString();
                        chats.Add(ReadChat(reader, namechat));
                    }
                }
                return chats;
            }
        }
        public static Chat GetChatt(string nameChat)
        {
            Chat chat = new Chat();
            using (BinaryReader reader = new BinaryReader(File.Open("ChatT.dat", FileMode.Open)))
            {
                while (reader.PeekChar() > -1)
                {
                    string r = reader.ReadString();
                    if (r.Equals("~"))
                    {
                        string namechat = reader.ReadString();
                        if (namechat.Equals(nameChat))
                        {
                            chat = ReadChat(reader, namechat);
                        }
                        
                    }
                }
                return chat;
            }
        }

        private static Chat ReadChat(BinaryReader reader,string NameChat)
        {
            Chat chat = new Chat();
            chat.Name = NameChat;
            if (reader.ReadString().Equals("end"))
            {
                List<User> users = new List<User>();
                while (true)
                {
                    string tempName = reader.ReadString();
                    if (!tempName.Equals("end"))
                    {
                        User user = new User();
                        user.Name = tempName;
                        users.Add(user);
                    }
                    else
                    {
                        break;
                    }
                }
                chat.Users = users;
                Dictionary<string, LogsUser> ChatLogUsers = new Dictionary<string, LogsUser>();
                while (true)
                { 
                        string temp = reader.ReadString();
                        if (!temp.Equals("end"))
                        {
                            DateTime start = DateTime.Parse(reader.ReadString());
                            string tempStop = reader.ReadString();
                            DateTime? stop = tempStop.Equals("")|| tempStop.Equals("end") ? null : DateTime.Parse(tempStop);
                            LogsUser logsUser = new LogsUser()
                            {
                                StartChat = start,
                                StopChat = stop,

                            };
                            ChatLogUsers.Add(temp, logsUser);
                        }
                        else
                        {
                            break;
                        }
                }
                chat.ChatLogUsers = ChatLogUsers;
                List<IBot> bots = new List<IBot>();
                while (true)
                {
                    string temp = reader.ReadString();
                    if (!temp.Equals("end"))
                    {
                        switch (temp)
                        {
                            case "BotTime":
                                bots.Add(new BotTime());
                                break;
                            case "BotAnecdote":
                                bots.Add(new BotAnecdote());
                                break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                chat.ChatBot = bots;
                List<Message> messages = new List<Message>();
                while (true)
                {
                    string temp = reader.ReadString();
                    if (!temp.Equals("end"))
                    {
                        Message message = new Message();
                        message.IdMessage = Convert.ToInt32(temp);
                        message.Content = reader.ReadString();
                        message.dateTime = DateTime.Parse(reader.ReadString());
                        message.user = new User()
                        {
                            Name = reader.ReadString()
                        };
                        message.OutUser = reader.ReadString();
                        messages.Add(message);
                    }
                    else
                    {
                        break;
                    }
                }
                chat.ListMessage = messages;
            }
            return chat;
        }
    }

}

