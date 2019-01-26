using Microsoft.EntityFrameworkCore;
using weGOPAY.weGOPAY.Core.Models.Requests;
using weGOPAY.weGOPAY.Core.Models.Settlements;
using weGOPAY.weGOPAY.Core.Models.Users;
using weGOPAY.weGOPAY.Core.Models.Wallets;
using weGOPAY.weGOPAY.Core.Models.WalletTransactions;

namespace weGOPAY.weGOPAY.Data
{
    public class weGOPAYDbContext : DbContext
    {
        public weGOPAYDbContext(DbContextOptions<weGOPAYDbContext> db): base(db) { }
        public DbSet<User> User { get; set; }
        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<WalletTransaction> WalletTransaction { get; set; }
       // public DbSet<Settlement> Settlement { get; set; }




        
    }
}
