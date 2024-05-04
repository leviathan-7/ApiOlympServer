using ApiServer.Models;
using EntityGraphQL.Schema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApiServer.Controllers
{
    public class NocRegionMutations
    {
        [GraphQLMutation("Add a new NocRegion to the system")]
        public Expression<Func<olympicsContext, NocRegion>> AddNewNocRegion(olympicsContext db, long id, string noc, string regionName)
        {
            var item = new NocRegion
            {
                Id = id,
                Noc = noc,
                RegionName = regionName,
            };
            db.NocRegions.Add(item);
            db.SaveChanges();

            return (ctx) => ctx.NocRegions.First(p => p.Id == item.Id);
        }

        [GraphQLMutation("Update NocRegion in the system")]
        public Expression<Func<olympicsContext, NocRegion>> UpdateNocRegion(olympicsContext db, long id, string noc, string regionName)
        {
            if (!db.NocRegions.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.NocRegions.First(x => x.Id == id);
            item.Noc = noc;
            item.RegionName = regionName;
            db.Update(item);
            db.SaveChanges();
            return (ctx) => ctx.NocRegions.First(p => p.Id == id);
        }

        [GraphQLMutation("Delete NocRegion in the system")]
        public Expression<Func<olympicsContext, NocRegion>> DeleteNocRegion(olympicsContext db, long id)
        {
            if (!db.NocRegions.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.NocRegions.First(x => x.Id == id);
            db.Remove(item);
            db.SaveChanges();
            return (ctx) => null;
        }
    }
}
