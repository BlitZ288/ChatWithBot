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
            IContext context = new BinaryHelper();
            ChatApp chatApp = new ChatApp(context);
            var  AllChat = context.GetAllChats();
            User user;
            Console.WriteLine("Ваше имя:");
            string UserName = Console.ReadLine().Trim();            
            var ListUser = context.GetUsers();
            user = ListUser.Find(u => u.Name.Equals(UserName));
            if (user == null)
            {
                user = new User(UserName);
                ListUser.Add(user);
                context.CreatUser(ListUser);
            }           
            Console.WriteLine(" Cписок доступных команд\n create-chat\n start-chat \n");
            bool showMenu = true;
            while (showMenu)
            {
                string choice = Console.ReadLine().Trim();
                switch (choice)
                {
                    case "create-chat":
                        Console.WriteLine("Название чата:");
                        string NameChat = Console.ReadLine().Trim();
                        Chat Newchat = new Chat(user, NameChat);
                        chatApp.CreateChat(Newchat);
                        Console.WriteLine("Чат успешно создан");
                        
                        //Newchat = Newchat.CreateChat(user, AllChat,context);
                        /*Пока под вопросом
                        Newchat.AddLogChat(Newchat, choice, user);
                        */
                        DialogChat(user, Newchat, AllChat,context);
                        break;
                    case "start-chat":
                        Chat chat= new Chat();
                        if (chatApp.ListChats.Count == 0)
                        {
                            Console.WriteLine("Доступных чатов нет");
                            Console.WriteLine("Для создания чата введите:\n create-chat");
                            break;
                        }
                        int j = 1;
                        Console.WriteLine("Доступные чаты:");
                        foreach (var c in chatApp.ListChats)
                        {
                           Console.Write($"\t {j} {c.Name}");
                           Console.Write("\t Боты:");
                           foreach (var b in c.ChatBot)
                            {
                               Console.Write($"\t {b.NameBot} \n");
                            }
                            j++;
                           Console.WriteLine("");
                        }
                           Console.WriteLine("В какой хотите войти:");
                            try
                            {                           
                                int chociChat = Convert.ToInt32(Console.ReadLine().Trim()) - 1;
                                chat = AllChat[chociChat];
                                chat.AddLogChat(chat, choice, user);
                                DialogChat(user, chat, AllChat,context);
                                showMenu = false;
                                context.CreatChat(AllChat);
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Неверный формат ввода");
                            }
                        break;
                    default:
                        Console.WriteLine("Введинте одну из команд:\n create-chat \n start-chat");
                        break;
                }
            }
        }
        static void DialogChat( User user,Chat chat,List<Chat> chats,IContext context)
        {
            ChatApp chatApp = new ChatApp(context);
            Console.WriteLine("Список участников данного чата : ");
            foreach (var u in chat.Users)
            {
                Console.WriteLine($"{u.Name}");
            }
            Console.WriteLine(chatApp.GetHistoryChat(chat, user));
            //chat.GetHistoryChat(chat, user);
            string listCommand = " sign @username \n logut @username\n add-mes @username ``message``\n" +
              " del-mes messageId \n bot @botname @username /bot-command\n stop-сhat \n signb \n 0-Сохранить и выйти\n";
            Console.WriteLine("Доступные команды ");
            Console.WriteLine(listCommand);
            if (chat.ChatBot.Count > 0)
            {
                Console.WriteLine("Команды для ботов \n");
                foreach (var b in chat.ChatBot)
                {
                    Console.WriteLine($"Название бота {b.NameBot}");
                    Console.WriteLine(b.GetAllCommand() + "\n");
                }
            }
           
            bool flagStop = true;
            while (flagStop)
            {
                string[] CommandParts = Console.ReadLine().Trim().Split(" ");
                switch (CommandParts[0])
                {
                    case "sign":
                        string NameInvite = CommandParts[1].Replace("@", "");
                        user.SignUser(chat, NameInvite,context);
                        chat.AddLogChat(chat, CommandParts[0], user);
                        context.CreatChat(chats);
                        break;
                    case "logut":
                        NameInvite = CommandParts[1].Replace("@", "");
                        user.LogutUser(chat, user, NameInvite);
                        chat.AddLogChat(chat, CommandParts[0], user);
                        context.CreatChat(chats);
                        break;
                    case "add-mes":
                        string NameSend = CommandParts[1].Replace("@", "");
                        if (chat.Users.Contains(user))
                        {
                            Console.WriteLine("Сообщение:");
                            string Content = Console.ReadLine().Trim();
                            var message = Message.CreatMessage(Content,NameSend,chat,user);
                            if (message != null)
                            {
                                user.SendMessage(message, chat);
                                chat.AddLogChat(chat, CommandParts[0], user);
                                context.CreatChat(chats);
                                Console.WriteLine("Введите команду:");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Вы не можете писать в чать не являись участником");
                            Console.WriteLine("Чтобы присоединиться используйте команду sign @username");
                        }
                        break;
                    case "del-mes":
                        try
                        {
                            user.DelMessage(chat, Convert.ToInt32(CommandParts[1]) - 1, user);
                            chat.AddLogChat(chat, CommandParts[0], user);
                            context.CreatChat(chats);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный формат ввода");
                        }
                      
                        break;
                    case "bot":
                        try
                        {
                            NameSend = CommandParts[2].Replace("@", "");
                            if (chat.Users.Contains(user))
                            {
                                string botName = CommandParts[1].Replace("@", "");
                                if (chat.ChatBot.Where(b => b.NameBot == botName).Any())
                                {
                                    string Contetn = "";
                                    foreach (var b in chat.ChatBot)
                                    {
                                        if (b.NameBot == CommandParts[1])
                                        {
                                            Contetn = b.Move(CommandParts[^1]);
                                        }
                                    }
                                    if (!String.IsNullOrEmpty(Contetn))
                                    {
                                        var message = Message.CreatMessage(Contetn, botName, chat, user);
                                        user.SendMessage(message, chat);
                                        chat.AddLogChat(chat, CommandParts[0], user);
                                        context.CreatChat(chats);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Такого бота в чатe нет ((");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Вы не можете писать в чать не являись участником");
                                Console.WriteLine("Чтобы присоединиться используйте команду sign @username");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Неверный формат ввода");
                        }
                         catch (IndexOutOfRangeException )
                        {
                            Console.WriteLine("Неверный формат ввода");
                        }

                        break;
                    case "signb":
                        user.InviteBot(chat,context);
                        chat.AddLogChat(chat, CommandParts[0], user);
                        context.CreatChat(chats);
                        break;
                    case "stop-chat":
                        if(user.DelChat(chat, user, chats))
                        {
                            Console.WriteLine("Чат упешео удален");
                            chat.AddLogChat(chat, CommandParts[0], user);
                            context.CreatChat(chats);
                            flagStop = false;
                        }
                        else
                        {
                            Console.WriteLine("Вы не можете удалить чат, не являясь его участником");
                        }
                        break;
                    case "0":
                        flagStop = false;
                        break;
                    default:
                        Console.WriteLine("Неверная команда , пожалуйста выберите из списка доступных команд :");
                        Console.WriteLine(listCommand);
                        break;
                }
            }
        }   
    }

}
