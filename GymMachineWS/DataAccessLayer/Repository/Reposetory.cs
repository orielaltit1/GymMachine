namespace GymMachineWS.DataAccessLayer
{
    public abstract class Repository 
    {
        protected IDbContext dbContext;
        protected ModelFactory factoryModles;

        public Repository(IDbContext dbContext, ModelFactory factoryModles)
        {
            this.dbContext = dbContext;
            this.factoryModles = factoryModles;
        }

    }
}
