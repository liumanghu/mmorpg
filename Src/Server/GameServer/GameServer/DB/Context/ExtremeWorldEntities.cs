using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace GameServer.DB.Context
{
    public partial class ExtremeWorldEntities : DbContext
    {
        public ExtremeWorldEntities() : base("name=ExtremeWorldEntities")
        {
        }
    
        public virtual DbSet<TUser> Users { get; set; }
        public virtual DbSet<TPlayer> Players { get; set; }
        public virtual DbSet<TCharacter> Characters { get; set; }
        public virtual DbSet<TCharacterItem> CharacterItem { get; set; }
    }
}
