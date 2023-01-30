using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace PhoneStore.DAL.Models
{
    public  class UserClaim
    {
        public int UserClaimId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
