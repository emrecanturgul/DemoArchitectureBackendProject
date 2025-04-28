using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserOperationClaimRepository.Constants
{
    public class UserOperationClaimMessages
    {
        public static string Added = "yetki basariyla olusturuldu";
        public static string Update = "yetki basariyla guncellendi";
        public static string Delete = "yetki basariyla silindi";
        public static string OperationClaimAlreadyExist = "bu kullaniciya bu yetki zaten verilmis";
        public static string OperationClaimNotFound = "bu yetki bulunamadi";
        public static string UserNotFound = "bu kullanici bulunamadi";
    }
}
