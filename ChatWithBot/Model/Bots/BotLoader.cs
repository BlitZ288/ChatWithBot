using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Model.Bots
{
    [Serializable]
    class BotLoader:Bot
    {
        public override string NameBot { 
            get {
                return "BotLoader";
            }
            set
            {

            }
            
        } 
    }
}
