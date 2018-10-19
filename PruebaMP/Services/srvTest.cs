using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PruebaMP.Models;

namespace PruebaMP.Services
{
    public class srvTest
    {
        public test AddTest(test oTest)
        {
            using (PruebaMPEntities bd = new PruebaMPEntities())
            {
                bd.Entry(oTest).State = System.Data.Entity.EntityState.Added;
                bd.SaveChanges();
            }
            return oTest;
        }
    }
}