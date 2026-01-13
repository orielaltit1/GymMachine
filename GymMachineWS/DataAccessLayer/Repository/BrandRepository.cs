using Models;
using System.Data;

namespace GymMachineWS.DataAccessLayer
{
    public class GymMachineBrandRepository : Repository, IRepository<GymMachineBrand>
    {
        public GymMachineBrandRepository(IDbContext dbContext, ModelFactory factoryModles) : base(dbContext, factoryModles)
        {
        }

        public bool Create(GymMachineBrand item)
        {
            string sql = $@"INSERT INTO Client
                            ( 
                            BrandName, BrandId, BrandLogo
                            )
                            VALUES
                            (
                            @brandName, @brandId, @brandLogo
                            )";
            this.dbContext.AddParamter("@brandName", item.BrandName);
            this.dbContext.AddParamter("@brandId", item.BrandId.ToString());
            this.dbContext.AddParamter("@brandLogo", item.BrandLogo);
            return this.dbContext.Insert(sql)>0;
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete FROM GymMachineBrand
                            WHERE BrandId = @brandId";
            this.dbContext.AddParamter("@brandId", id);
            return this.dbContext.Delete(sql) > 0;
        }

        public List<GymMachineBrand> GetAll()
        {
            List<GymMachineBrand> gymMachineBrands = new List<GymMachineBrand>();
            string sql = "SELECT * FROM GymMachineBrand";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    gymMachineBrands.Add(this.factoryModles.GymMachineBrandCreator.CreateMoldel(reader));
                }
            }
            return gymMachineBrands;
        }

        public GymMachineBrand GetById(string id)
        {
            string sql = $@"SELECT *  FROM GymMachineBrand where BrandId=@brandId";
            this.dbContext.AddParamter("@brandId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.factoryModles.GymMachineBrandCreator.CreateMoldel(reader);
            }
        }

        public bool Update(GymMachineBrand item)
        {
            string sql = $@"UPDATE Clients SET BrandName=@brandName, BrandId=@brandId, BrandLogo=@brandLogo
                            WHERE BrandId=@brandId";
            //לבדוק שאין INJECTION
            this.dbContext.AddParamter("@brandName", item.BrandName);
            this.dbContext.AddParamter("@brandId", item.BrandId.ToString());
            this.dbContext.AddParamter("@brandLogo", item.BrandLogo);
            return this.dbContext.Update(sql) > 0;
        }
    }
}
