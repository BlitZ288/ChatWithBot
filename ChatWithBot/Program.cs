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
            User user1 = new User()
            {
                Name = "Kiril"
            };
            //BinaryHelper.AddUser(BinaryHelper.GetChatt("FirstChat"), user1);
           // var a= BinaryHelper.GetChatts();
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
                        Chat Newchat = new Chat();
                        Newchat = Newchat.CreateChat(user, AllChat,context);
                        //BinaryHelper.AddChat(Newchat);
                      //var s=  BinaryHelper.GetChatts();
                        //BinaryHelper.GetChatt("SecondChat");
                        
                        DialogChat(user, Newchat, AllChat,context);
                        break;
                    case "start-chat":
                        Chat chat= new Chat();
                        if (chat.StartChat(AllChat))
                        {
                            int chociChat = Convert.ToInt32(Console.ReadLine().Trim()) - 1;
                            chat = AllChat[chociChat];
                            DialogChat(user, chat, AllChat,context);
                            showMenu = false;
                            context.CreatChat(AllChat);
                        }
                        else
                        {
                            Console.WriteLine("Для создания чата введите:\n create-chat");
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
            bool flagNewChat = true;
            int indexmesseg = 0;
           
            Console.WriteLine("Список участников данного чата : ");
            foreach (var u in chat.Users)
            {
                Console.WriteLine($"{u.Name}");
            }
            flagNewChat = chat.GetHistoryChat(chat, user);
           // DialogHistoryChat(user, chat, ref flagNewChat); 
            Console.WriteLine("Доступные команды ");
            Console.WriteLine(" sign @username \n logut @username\n add-mes @username ``message``\n" +
              " del-mes messageId \n bot @botname @username /bot-command\n stop-сhat \n signb @botname\n 0-Сохранить и выйти\n");
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
                string[] choice = Console.ReadLine().Trim().Split(" ");
                switch (choice[0])
                {
                    case "sign":
                        string NameInvite = choice[1].Replace("@", "");
                        user.SignUser(chat, NameInvite,context);                        
                        break;
                    case "logut":
                        NameInvite = choice[1].Replace("@", "");
                        user.LogutUser(chat,user, NameInvite);                       
                        break;
                    case "add-mes":
                        string NameSend = choice[1].Replace("@", "");
                       
                        if (chat.Users.Contains(user))
                        {
                            if (!flagNewChat)
                            {
                                indexmesseg = chat.ListMessage[^1].IdMessage;
                            }
                            indexmesseg++;
                            Console.WriteLine("Сообщение:");
                            string Content = Console.ReadLine().Trim();
                            var message = Message.CreatMessage(indexmesseg,Content,NameSend,chat,user);
                            if (message != null)
                            {
                                user.SendMessage(message, chat);
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
                        user.DelMessage(chat, Convert.ToInt32(choice[1]) - 1, user);                      
                        break;
                    case "bot":  
                        NameSend = choice[2].Replace("@", "");
                       
                        if (chat.Users.Contains(user))
                        {

                            if(chat.ChatBot.Where(b => b.NameBot == choice[1].Replace("@", "")).Any())
                            {
                                if (!flagNewChat)
                                {
                                    indexmesseg = chat.ListMessage[^1].IdMessage;
                                }
                                indexmesseg++;
                                string Contetn = "";
                                foreach (var b in chat.ChatBot)
                                {
                                    if (b.NameBot == choice[1])
                                    {
                                        Contetn = b.Move(choice[^1]);
                                    }
                                }
                                var message = Message.CreatMessage(indexmesseg,Contetn, NameSend, chat, user);
                                user.SendMessage(message, chat);
                            }
                            else
                            {
                                Console.WriteLine("Такого бота в чат нет ((");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Вы не можете писать в чать не являись участником");
                            Console.WriteLine("Чтобы присоединиться используйте команду sign @username");
                        }                       
                        break;
                    case "signb":
                        user.InviteBot(chat,context);                       
                        break;
                    case "stop-chat":
                            user.DelChat(chat, user, chats);
                            flagStop = false;
                        break;
                    case "0":
                        flagStop = false;
                        break;
                }
            }
        }   
    }
    
}
