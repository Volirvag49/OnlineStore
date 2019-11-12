namespace Store.Database.EF
{
    public static class DbInitializer
    {

        public static void Initialize(DefaultContext db)
        {
            //db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
