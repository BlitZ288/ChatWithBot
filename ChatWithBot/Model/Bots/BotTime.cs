using ChatWithBot.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model.Bots
{
    [Serializable]
    class BotTime :IBot
    {
        public  string NameBot
        {
            get
            {
                return "BotTime";
            }
            set
            {

            }

        }
        public string Move(string command)
        {
            try{
                command = command.Replace("/", "");
                if (command.Equals("current"))
                {
                    return DateTime.Now.ToString("HH:mm");
                }
                else
                {
                    return DateTime.Now.AddMinutes(Convert.ToDouble(command)).ToString("HH:mm");
                }
             }
            catch
            {
                return String.Empty;
            }

        }
        public StringBuilder GetAllCommand()
        {
            StringBuilder command = new StringBuilder("");
            command.Append("/current-Узнать текущие время");
            command.Append("\n" + "/Через time");
            return command;
        }
    }
}
