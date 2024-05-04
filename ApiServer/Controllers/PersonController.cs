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
    public class PersonMutations
    {
        [GraphQLMutation("Add a new Person to the system")]
        public Expression<Func<olympicsContext, Person>> AddNewPerson(olympicsContext db, long id, string fullName, string gender, long height, long weight)
        {
            if (db.People.Any(x => x.Id == id))
                return (ctx) => null;
            var item = new Person
            {
                Id = id,
                FullName = fullName,
                Gender = gender,
                Height = height,
                Weight = weight,
            };
            db.People.Add(item);
            db.SaveChanges();

            return (ctx) => ctx.People.First(p => p.Id == item.Id);
        }

        [GraphQLMutation("Update Person in the system")]
        public Expression<Func<olympicsContext, Person>> UpdatePerson(olympicsContext db, long id, string fullName, string gender, long height, long weight)
        {
            if (!db.People.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.People.First(x => x.Id == id);
            item.FullName = fullName;
            item.Gender = gender;
            item.Height = height;
            item.Weight = weight;
            db.Update(item);
            db.SaveChanges();
            return (ctx) => ctx.People.First(p => p.Id == id);
        }

        [GraphQLMutation("Delete Person in the system")]
        public Expression<Func<olympicsContext, Person>> DeletePerson(olympicsContext db, long id)
        {
            if (!db.People.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.People.First(x => x.Id == id);
            db.Remove(item);
            db.SaveChanges();
            return (ctx) => item;
        }
    }
}
