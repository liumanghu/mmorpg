
using System.Data.Entity.Migrations;
using GameServer.DB.Context;

namespace GameServer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ExtremeWorldEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    } 
}