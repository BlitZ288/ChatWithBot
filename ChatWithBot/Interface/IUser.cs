using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model
{
    public interface IUser
    {
        public void CreatChat(List<Chat> chats);
        public void DeleteMessage();
        public void SendMessage();
        public void ReadMessage();
        public void DeleteChat();
        public void ExitChat();

       
    }
}
