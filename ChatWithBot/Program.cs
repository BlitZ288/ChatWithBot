using ChatWithBot.Interface;
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
            Console.WriteLine("Добро пожаловать");
            User user ;
            Console.WriteLine("Ваше имя:");
            string UserName = Console.ReadLine().Trim();            
            var ListUser = BinaryHelper.GetUsers();
            if (ListUser.Any() && ListUser.Where(u=>u.Name==UserName).Any())
            {
                user = ListUser.Where(u => u.Name == UserName).FirstOrDefault();
            }
            else
            {
                user = new User(UserName);
                ListUser.Add(user);
                BinaryHelper.CreatUser(ListUser);
            }
            Console.WriteLine(" create-chat\n start-chat \n");
            while (true)
            {
                string choice = Console.ReadLine().Trim();
                switch (choice)
                {
                    case "create-chat":
                        Chat chat = new Chat();
                        Console.WriteLine("Название чата:");
                        chat.Name = Console.ReadLine().Trim();
                        Console.WriteLine("Доступны боты:");
                        int i = 1;
                        var ListBots = BinaryHelper.GetAllBot();
                        foreach (var b in ListBots)
                        {
                            Console.WriteLine($"{i} {b.NameBot}");
                            i++;
                        } 
                        chat.Users.Add(user);
                        chat.ChatLogUsers.Add(user.Name, new LogsUser() { StartChat = DateTime.Now, StopChat = null });
                        chat.ChatBot.Add(ListBots[Convert.ToInt32(Console.ReadLine().Trim()) - 1]);
                        chat.LogActions.Add(new LogAction(DateTime.Now, choice,user.Name));
                        Chat.Chats = BinaryHelper.GetAllChats();
                        Chat.Chats.Add(chat);                    
                        BinaryHelper.CreatChat(Chat.Chats);
                        Console.WriteLine("Чат успешно создан");
                        break;
                    case "start-chat":
                        Chat.Chats = BinaryHelper.GetAllChats(); 
                        if (Chat.Chats == null || !Chat.Chats.Any())
                        {
                            Console.WriteLine("Доступных чатов нет");
                            break;
                        }
                        int j = 1;
                        Console.WriteLine("Доступные чаты:");
                        foreach (var c in Chat.Chats)
                        {
                            Console.Write($"\t {j} {c.Name}");
                            Console.Write("\t Боты:");
                            foreach (var b in c.ChatBot)
                            {
                                Console.Write($"\t {b.NameBot} \n");
                            }
                            j++;
                        }
                        Console.WriteLine("В какой хотете войти:");
                        int chociChat = Convert.ToInt32(Console.ReadLine().Trim())-1;
                        Chat.Chats[chociChat].LogActions.Add(new LogAction(DateTime.Now, choice, user.Name));                       
                        DialogChat(user, Chat.Chats[chociChat]);
                        break;                   
                }
            }
            
        }
        static void DialogChat( User user,Chat chat)
        {
            bool flagNewChat = true;
            int indexmesseg = 0;
           
            Console.WriteLine("Список участников данного чата : ");
            foreach (var u in chat.Users)
            {
                Console.WriteLine($"{u.Name}");
            }
            DialogHistoryChat(user, chat, ref flagNewChat); 
            Console.WriteLine("Доступные команды ");
            Console.WriteLine(" sign @username \n logut @username\n add-mes @username ``message``\n" +
              " del-mes messageId \n bot @botname @username /bot-command\n stop-сhat \n signb @botname\n 0-Сохранить и выйти");
            bool flagStop = true;
            while (flagStop)
            {
                string[] choice = Console.ReadLine().Trim().Split(" ");
                switch (choice[0])
                {
                    case "sign":
                        chat.LogActions.Add(new LogAction(DateTime.Now, choice[0], user.Name));
                        string NameInvite = choice[1].Replace("@", "");
                        var Inviteuser = BinaryHelper.GetUsers().Where(u => u.Name == choice[1]).FirstOrDefault();
                        if (!chat.Users.Where(u=>u.Name==Inviteuser.Name).Any())
                        {
                            if (!chat.ChatLogUsers.ContainsKey(NameInvite))
                            {
                                chat.ChatLogUsers.Add(NameInvite, new LogsUser() { StartChat = DateTime.Now, StopChat = null });
                            }
                            else
                            {
                                chat.ChatLogUsers[NameInvite].StopChat = null;
                            }
                            chat.Users.Add(Inviteuser);
                            Console.WriteLine($"Пользователь {choice[1].Replace("@", "")} приглашен");
                        }
                        else
                        {
                            Console.WriteLine("Пользователя с таким именем не найдент или уже в чате ");
                        }
                        break;
                    case "logut":
                        if (chat.Users.Where(u => u.Name == user.Name).Any())
                        {
                            chat.LogActions.Add(new LogAction(DateTime.Now, choice[0], user.Name));
                            NameInvite = choice[1].Replace("@", "");
                            var CickUer = chat.Users.Where(u => u.Name == NameInvite).FirstOrDefault();
                            if (CickUer != null)
                            {
                                chat.ChatLogUsers[CickUer.Name].StopChat = DateTime.Now;
                                chat.Users.Remove(CickUer);
                                Console.WriteLine($"Пользователь {choice[1].Replace("@", "")} удален из чата");
                            }
                            else
                            {
                                Console.WriteLine("Пользователя с таким именем нет");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Вы не являись участником");
                            Console.WriteLine("Чтобы присоединиться используйте команду sign @username");
                        }
                        break;
                    case "add-mes":
                        if (chat.Users.Where(u => u.Name == user.Name).Any())
                        {
                            string NameSend = choice[1].Replace("@", "");
                            Console.WriteLine("Сообщение:");
                            string Content = Console.ReadLine().Trim();
                            var Senduser = chat.Users.Where(c => c.Name == NameSend).FirstOrDefault();
                            if (Senduser != null)
                            {
                                if (!flagNewChat)
                                {
                                    indexmesseg = chat.ListMessage[^1].IdMessage;
                                }
                                indexmesseg++;

                                Message message = new Message()
                                {
                                    IdMessage = indexmesseg,
                                    Content = Content,
                                    dateTime = DateTime.Now,
                                    OutUser = Senduser.Name,
                                    user = user
                                };
                                chat.ListMessage.Add(message);
                                Console.WriteLine($"id={message.IdMessage} {user.Name} {message.dateTime} ({Senduser.Name}): {message.Content}");
                            }
                            else
                            {
                                Console.WriteLine("Этого пользователя нет в чате");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Вы не можете писать в чать не являись участником");
                            Console.WriteLine("Чтобы присоединиться используйте команду sign @username");
                        }
                        break;
                    case "del-mes":
                        Message ChoiceMessage = chat.ListMessage[Convert.ToInt32(choice[1]) - 1];
                        chat.LogActions.Add(new LogAction(DateTime.Now, choice[0], user.Name));
                        if (ChoiceMessage.user.Name == user.Name && DateTime.Now.Subtract(ChoiceMessage.dateTime) < new TimeSpan(60, 0, 0))
                        {
                                chat.ListMessage.Remove(ChoiceMessage);
                                Console.WriteLine("Сообщение успешно удаленно ");
                        } else
                         {
                           Console.WriteLine("Вы не можете удалить это сообщение. Вы не являетесь его владельцем либо прошло слишком много времени");
                         }
                       
                        break;
                    case "bot":                        
                        Message messageBot = new Message();                       
                        messageBot.user = chat.Users.Where(c => c.Name == choice[2].Replace("@", "")).FirstOrDefault();                       
                        if (messageBot.user != null)
                        {
                            if (!flagNewChat)
                            {
                                indexmesseg = chat.ListMessage[^1].IdMessage;
                            }
                            indexmesseg++;
                            messageBot.IdMessage = indexmesseg;                        
                            if (chat.ChatBot.Where(b => b.NameBot == choice[1].Replace("@", "")).Any())
                            {
                               
                                foreach (var b in chat.ChatBot)
                                {
                                    if (b.NameBot == choice[1])
                                    {
                                        messageBot.Content = b.Move(choice[^1]);
                                        messageBot.dateTime = DateTime.Now;
                                        messageBot.OutUser = b.NameBot;
                                    }                               
                                }
                                Console.WriteLine($"id={messageBot.IdMessage} { messageBot.OutUser} {messageBot.dateTime} ({ messageBot.user.Name}) : {messageBot.Content}");
                                chat.ListMessage.Add(messageBot);
                            }
                            else
                            {
                                Console.WriteLine("Такого бота в чат нет ((");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Вы не можете писать в чать не являись участником");
                        }
                        break;
                    case "signb":
                        var ListBot = BinaryHelper.GetAllBot().Where(b=>b.NameBot==choice[1]).FirstOrDefault();
                        if (ListBot != null && !chat.ChatBot.Contains(ListBot))
                        {
                            chat.ChatBot.Add(ListBot);
                        }
                        else
                        {
                            Console.WriteLine("Такого бота нет ((");
                        }
                        break;
                    case "stop-chat":
                        if (chat.Users.Where(u=>user.Name==u.Name).Any())
                        {
                            chat.LogActions.Add(new LogAction(DateTime.Now, choice[0], user.Name));
                            chat.ChatLogUsers[user.Name].StopChat = DateTime.Now;
                            chat.Users.Remove(user);
                            Chat.Chats.Remove(chat);
                            flagStop = false;
                        }
                        break;
                    case "0":
                        flagStop = false;
                        break;
                }
            }
            BinaryHelper.CreatChat(Chat.Chats);

        }
        static void DialogHistoryChat(User user, Chat chat,ref bool flag )
        {
            try
            {
                if (chat.ChatLogUsers[user.Name] != null)
                {
                    var mes = chat.ListMessage.Where(m => chat.ChatLogUsers[user.Name].StartChat <= m.dateTime);
                    if (chat.ChatLogUsers[user.Name].StopChat != null)
                    {
                        mes = chat.ListMessage.Where(m => m.dateTime < chat.ChatLogUsers[user.Name].StopChat);
                    }
                    if (mes.Any()) 
                    {
                        foreach (var m in mes)
                        {
                            Console.WriteLine($"id={m.IdMessage} {m.dateTime} {m.user.Name} ({m.OutUser}): {m.Content} ");
                        }
                        flag = false;
                    }
                }
            }
            catch{ 
                    Console.WriteLine($"Сообщений пока нет");
             }
            
        }
       
    }
    
}
