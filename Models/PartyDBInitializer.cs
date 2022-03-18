using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_App.Models
{
    public class PartyDBInitializer: CreateDatabaseIfNotExists<PartyContext>
    {
        protected override void Seed(PartyContext dd)
        {
            base.Seed(dd);
        }
    }
}