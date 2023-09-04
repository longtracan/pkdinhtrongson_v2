using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model
{
    public class Comment_Model
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ProductID { get; set; }
        public string Comment { get; set; }
        public System.DateTime Date { get; set; } 
    }
}
