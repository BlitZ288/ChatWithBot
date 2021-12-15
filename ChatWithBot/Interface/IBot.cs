using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWithBot.Interface
{
    interface IBot
    {
        public  string NameBot { get; set; }
        public string Move(string command);
    }
}
