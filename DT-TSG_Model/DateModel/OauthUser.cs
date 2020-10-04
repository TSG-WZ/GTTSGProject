using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTTSG_Model.DateModel
{
    public class OauthUser
    {
      
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string OpenId { get; set; }
        public string Scope { get; set; }
       
        public string UserData { get; set; }

    }
}
