namespace LibraryAPI.Persistence
{
    internal class DbInitializer
    {
        public static void Initialize(LibraryDbContext conext) 
        { 
            conext.Database.EnsureCreated();
        }
    }
}
