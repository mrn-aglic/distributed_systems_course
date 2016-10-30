using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class Update { } 
    
    public class UpdateAck
    {
        public string Text { get; private set; }

        public UpdateAck(string text)
        {
            Text = text;
        }
    }
    
    public class Wait { }   
}
