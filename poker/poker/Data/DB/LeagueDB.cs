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
    
    public partial class LeagueDB
    {
        public LeagueDB()
        {
            this.Players = new HashSet<PlayerDB>();
            this.Rooms = new HashSet<RoomDB>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<PlayerDB> Players { get; set; }
        public virtual ICollection<RoomDB> Rooms { get; set; }
    }
}
