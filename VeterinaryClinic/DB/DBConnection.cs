using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinaryClinic.DB
{
    internal class DBConnection
    {
        public static VeterinaryСlinicEntities veterinary = new VeterinaryСlinicEntities();
        public static Users curret_user;
    }
}
