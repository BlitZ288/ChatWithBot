using ChatWithBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatWithBot
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            Console.WriteLine("Доступные команды\n");
            Console.WriteLine("Ваше имя:");
            string NameUser = Console.ReadLine().Trim();
            user.Name = NameUser;
            List<User> ListUsers = new List<User>();
            try
            {
                if (!User.GetUsers().Contains(user))
                {
                    ListUsers.Add(user);
                    User.CreatUser(ListUsers);
                }
            }
            catch
            {
                ListUsers.Add(user);
                User.CreatUser(ListUsers);
            }
            Console.WriteLine(" create-chat\n start-chat \n");
            string choice = Console.ReadLine().Trim();
            switch (choice)
            {
                case "create-chat":                   
                    Console.WriteLine("Название чата:");
                    string NameChat = Console.ReadLine().Trim();
                    Console.WriteLine("Доступны боты:");
                    int i = 1;
                    foreach (var b in Bot.GetAllBott())
                    {

                        Console.WriteLine($"{i} {b.NameBot}");
                        i++;
                    }
                    Bot bot = new Bot();
                    bot = Bot.GetAllBott()[Convert.ToInt32(Console.ReadLine().Trim())-1];
                    Chat chat = new Chat();
                    chat.Name = NameChat;
                    chat.Users.Add(user);
                    chat.ChatBot = bot;                    
                    Chat.Chats.Add(chat);
                    user.CreatChat(Chat.Chats);                   
                    break;
                case "start-chat":
                    Chat.Chats = Chat.GetAllChats();                                     
                    if (Chat.Chats == null)
                    {
                        break;
                    }
                    int j = 1;
                    foreach (var c in Chat.Chats)
                    {
                        Console.WriteLine("Доступные чаты:");
                        Console.WriteLine($"\t {j} {c.Name} Боты: {c.ChatBot.NameBot}");
                    }
                    Console.WriteLine("В какой хотете войти:");
                    int chociChat = Convert.ToInt32(Console.ReadLine().Trim())-1;
                    //Chat.Chats[chociChat].Users.Add(user);
                    DialogChat(chociChat,user);
                    break;                   
            }
            Console.ReadKey();
            
            
        }
        static void DialogChat(int indexChat, User user)
        {
            bool flagNewChat = true;
            int indexmesseg = 0;
            Console.WriteLine("Список участников : ");
            foreach(var u in Chat.Chats[indexChat].Users)
            {
                Console.WriteLine($"{u.Name}");
            }
            Console.WriteLine("Доступные команды ");
            Console.WriteLine(" sign @username \n logut @username\n add-mes @username ``message``\n" +
              " del-mes messageId \n bot @botname @username /bot-command\n stop-сhat");           
            if (Chat.Chats[indexChat].ListMessage != null)
            {
                foreach (var m in Chat.Chats[indexChat].ListMessage)
                {
                    Console.WriteLine($"{m.dateTime} {m.user.Name}: {m.Content} ");
                }
                flagNewChat = false;
            }
            else
            {
                Console.WriteLine($"Сообщений пока нет");
                Chat.Chats[indexChat].ListMessage = new List<Message>();
            }
            bool flag = true;
            while (flag)
            {
                string[] choice = Console.ReadLine().Trim().Split(" ");
                switch (choice[0])
                {
                    case "sign":
                        string NameInvite = choice[1].Replace("@", "");
                        var Inviteuser= User.GetUsers();
                        if (Inviteuser.Any())
                        {
                            Chat.Chats[indexChat].Users.Add(Inviteuser.FirstOrDefault());
                            Console.WriteLine($"Пользователь {choice[1].Replace("@", "")} приглашен");
                        }
                        else
                        {
                            Console.WriteLine("Пользователя с таким именем нет");
                        }
                        break;
                    case "logut":
                         NameInvite = choice[1].Replace("@", "");                     
                         Inviteuser = Chat.Chats[indexChat].Users;
                        if (Inviteuser.Any())
                        {
                            Chat.Chats[indexChat].Users.Remove(Inviteuser.FirstOrDefault());
                            Console.WriteLine($"Пользователь {choice[1].Replace("@", "")} удален из чата");
                        }
                        else
                        {
                            Console.WriteLine("Пользователя с таким именем нет");
                        }
                        break;
                    case "add-mes":
                        string NameSend = choice[1].Replace("@", "");
                        var Senduser = Chat.Chats[indexChat].Users.Where(c => c.Name == NameSend).FirstOrDefault();
                        if (Senduser !=null)
                        {
                            if(flagNewChat)
                            {
                                indexmesseg = Chat.Chats[indexChat].ListMessage[^1].IdMessage;
                            }
                            indexmesseg++;
                        
                            Message message = new Message()
                            {
                                IdMessage=indexmesseg,
                                Content = choice[2],
                                dateTime = DateTime.Now,
                                user= user
                            };
                            Chat.Chats[indexChat].ListMessage.Add(message);                       
                            Console.WriteLine($"id={message.IdMessage} {user.Name} {message.dateTime} ({Senduser.Name}): {message.Content}");
                        }
                        else
                        {
                            Console.WriteLine("Этого пользователя нет в чате");
                        }
                        break;
                    case "0":
                        flag = false;
                        break;
                }
            }
            user.CreatChat(Chat.Chats);

        }
    }
}
