using webapi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi.DTO
{
    public class TeamListDTO
    {
        public string Game { get; set; }
	    public string Name { get; set; }
        public TeamStatus Status { get; set; }
        public int MemberCount { get; set; }
    }
}