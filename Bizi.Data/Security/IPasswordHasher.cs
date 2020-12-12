using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bizi.Data
{
    public interface IPasswordHasher
    {
        (string, string) HashPassword(string password);
    }
}
