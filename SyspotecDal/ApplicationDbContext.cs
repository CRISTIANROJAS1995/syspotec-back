using SyspotecDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SyspotecDal
{
    public class ApplicationDbContext : DbContext, IDisposable
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }

        public DbSet<AppConfiguration> AppConfiguration { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<InstrumentInterest> InstrumentInterest { get; set; }
        public DbSet<MusicalInterest> MusicalInterest { get; set; }
        public DbSet<SyspotecDomain.Entities.Range> Range { get; set; }
        public DbSet<SocialInterest> SocialInterest { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<TypeSubscription> TypeSubscription { get; set; }
        public DbSet<TypeImage> TypeImage { get; set; }
        public DbSet<TypeReaction> TypeReaction { get; set; }
        public DbSet<UserOtp> UserOtp { get; set; }
        public DbSet<UserActivation> UserActivation { get; set; }
        public DbSet<UserBiography> UserBiography { get; set; }
        public DbSet<UserBlock> UserBlock { get; set; }
        public DbSet<UserFollower> UserFollower { get; set; }
        public DbSet<UserImage> UserImage { get; set; }
        public DbSet<UserMap> UserMap { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<HiBeat> HiBeat { get; set; }
        public DbSet<HiBeatTop> HiBeatTop { get; set; }
        public DbSet<HiBeatInstrumentInterest> HiBeatInstrumentInterest { get; set; }
        public DbSet<HiBeatMusicalInterest> HiBeatMusicalInterest { get; set; }
        public DbSet<Reaction> Reaction { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<PlayList> PlayList { get; set; }
        public DbSet<TypePlayList> TypePlayList { get; set; }
        public DbSet<RankingUser> RankingUser { get; set; }
        public DbSet<Challenge> Challenge { get; set; }
        public DbSet<ChallengeHiBeat> ChallengeHiBeat { get; set; }
        public DbSet<ChallengeWinner> ChallengeWinner { get; set; }
        public DbSet<ChallengeAward> ChallengeAward { get; set; }
        public DbSet<DailyAchievement> DailyAchievement { get; set; }
        public DbSet<UserDailyAchievement> UserDailyAchievement { get; set; }
    }
}
