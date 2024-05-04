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
    public class EventMutations
    {
        [GraphQLMutation("Add a new Event to the system")]
        public Expression<Func<olympicsContext, Event>> AddNewEvent(olympicsContext db, long id, long sportId, string eventName)
        {
            if (db.Events.Any(x => x.Id == id))
                return (ctx) => null;
            var item = new Event
            {
                Id = id,
                SportId = sportId,
                EventName = eventName,
            };
            db.Events.Add(item);
            db.SaveChanges();

            return (ctx) => ctx.Events.First(p => p.Id == item.Id);
        }

        [GraphQLMutation("Update Event in the system")]
        public Expression<Func<olympicsContext, Event>> UpdateEvent(olympicsContext db, long id, long sportId, string eventName)
        {
            if (!db.Events.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.Events.First(x => x.Id == id);
            item.SportId = sportId;
            item.EventName = eventName;
            db.Update(item);
            db.SaveChanges();
            return (ctx) => ctx.Events.First(p => p.Id == id);
        }

        [GraphQLMutation("Delete Event in the system")]
        public Expression<Func<olympicsContext, Event>> DeleteEvent(olympicsContext db, long id)
        {
            if (!db.Events.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.Events.First(x => x.Id == id);
            db.Remove(item);
            db.SaveChanges();
            return (ctx) => item;
        }
    }
}

