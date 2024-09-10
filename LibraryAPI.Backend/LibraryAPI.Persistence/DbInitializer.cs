namespace LibraryAPI.Persistence
{
    internal class DbInitializer
    {
        public static void Initialize(LibraryDbConext conext) 
        { 
            conext.Database.EnsureCreated();
        }
    }
}
