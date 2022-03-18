using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_App.Models
{
    public class PartyContext: DbContext
    {
        public DbSet<Party> Partys { get; set; }
    }
}