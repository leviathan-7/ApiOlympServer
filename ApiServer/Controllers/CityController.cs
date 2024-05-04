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
    public class CityMutations
    {
        [GraphQLMutation("Add a new City to the system")]
        public Expression<Func<olympicsContext, City>> AddNewCity(olympicsContext db, long id, string cityName)
        {
            var item = new City
            {
                Id = id,
                CityName = cityName,
            };
            db.Cities.Add(item);
            db.SaveChanges();

            return (ctx) => ctx.Cities.First(p => p.Id == item.Id);
        }

        [GraphQLMutation("Update City in the system")]
        public Expression<Func<olympicsContext, City>> UpdateCity(olympicsContext db, long id, string cityName)
        {
            if (!db.Cities.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.Cities.First(x => x.Id == id);
            item.CityName = cityName;
            db.Update(item);
            db.SaveChanges();
            return (ctx) => ctx.Cities.First(p => p.Id == id);
        }

        [GraphQLMutation("Delete City in the system")]
        public Expression<Func<olympicsContext, City>> DeleteCity(olympicsContext db, long id)
        {
            if (!db.Cities.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.Cities.First(x => x.Id == id);
            db.Remove(item);
            db.SaveChanges();
            return (ctx) => null;
        }
    }
}
