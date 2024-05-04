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
    public class GameMutations
    {
        [GraphQLMutation("Add a new Game to the system")]
        public Expression<Func<olympicsContext, Game>> AddNewGame(olympicsContext db, long id, long gamesYear, string gamesName, string season)
        {
            var item = new Game
            {
                Id = id,
                GamesYear = gamesYear,
                GamesName = gamesName,
                Season = season,
            };
            db.Games.Add(item);
            db.SaveChanges();

            return (ctx) => ctx.Games.First(p => p.Id == item.Id);
        }

        [GraphQLMutation("Update Game in the system")]
        public Expression<Func<olympicsContext, Game>> UpdateGame(olympicsContext db, long id, long gamesYear, string gamesName, string season)
        {
            if (!db.Games.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.Games.First(x => x.Id == id);
            item.GamesYear = gamesYear;
            item.GamesName = gamesName;
            item.Season = season;
            db.Update(item);
            db.SaveChanges();
            return (ctx) => ctx.Games.First(p => p.Id == id);
        }

        [GraphQLMutation("Delete Game in the system")]
        public Expression<Func<olympicsContext, Game>> DeleteGame(olympicsContext db, long id)
        {
            if (!db.Games.Any(x => x.Id == id))
                return (ctx) => null;
            var item = db.Games.First(x => x.Id == id);
            db.Remove(item);
            db.SaveChanges();
            return (ctx) => null;
        }
    }
}
