using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Models
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NetUser { get; set; }

        public char Status { get; set; }
    }

}
