//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace poker.Data.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlayerDB
    {
        public PlayerDB()
        {
            this.Leagues = new HashSet<LeagueDB>();
        }
    
        public int id { get; set; }
        public int rank { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int money { get; set; }
    
        public virtual ICollection<LeagueDB> Leagues { get; set; }
    }
}
