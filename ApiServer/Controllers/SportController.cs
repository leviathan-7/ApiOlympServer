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
    public class SportMutations
    {
        [GraphQLMutation("Add a new Sport to the system")]
        public Expression<Func<olympicsContext, Sport>> AddNewSport(olympicsContext db, long id, string sportName)
        {
            var item = new Sport
            {
                Id = id,
                SportName = sportName,
            };
            db.Sports.Add(item);
            db.SaveChanges();

            return (ctx) => ctx.Sports.First(p => p.Id == item.Id);
        }

        [GraphQLMutation("Update Sport in the system")]
        public Expression<Func<olympicsContext, Sport>> UpdateSport(olympicsContext db, long id, string sportName)
        {
            if (!db.Sports.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.Sports.First(x => x.Id == id);
            item.SportName = sportName;
            db.Update(item);
            db.SaveChanges();
            return (ctx) => ctx.Sports.First(p => p.Id == id);
        }

        [GraphQLMutation("Delete Sport in the system")]
        public Expression<Func<olympicsContext, Sport>> DeleteSport(olympicsContext db, long id)
        {
            if (!db.Sports.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.Sports.First(x => x.Id == id);
            foreach (var I in db.Events.Where(i => i.SportId == id))
            {
                I.SportId = null;
                db.Update(I);
            }
            db.Remove(item);
            db.SaveChanges();
            return (ctx) => null;
        }
    }
}
