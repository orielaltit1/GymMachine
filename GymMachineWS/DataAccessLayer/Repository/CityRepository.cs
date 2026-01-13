using Models;
using System.Data;

namespace GymMachineWS.DataAccessLayer
{
    public class CityRepository : Repository, IRepository<City>
    {
        public CityRepository(IDbContext dbContext, ModelFactory factoryModles) : base(dbContext, factoryModles)
        {
        }

        public bool Create(City item)
        {
            string sql = $@"INSERT INTO Cities
                            ( 
                            CityId, CityName
                            )
                            VALUES
                            (
                            @CityId, @CityName
                            )";
            this.dbContext.AddParamter("@CityId", item.CityId);
            this.dbContext.AddParamter("@CityName", item.CityName);
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete FROM Cities
                            WHERE CityId = @CityId";
            this.dbContext.AddParamter("@CityId", id);
            return this.dbContext.Delete(sql) > 0;
        }

        public List<City> GetAll()
        {
            List<City> cities = new List<City>();
            string sql = "SELECT * FROM Cities";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    cities.Add(this.factoryModles.CityCreator.CreateMoldel(reader));
                }
            }
            return cities;
        }

        public City GetById(string id)
        {
            string sql = $@"SELECT *  FROM Cities where CityId=@CityId";
            this.dbContext.AddParamter("@CityId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.factoryModles.CityCreator.CreateMoldel(reader);
            }
        }

        public bool Update(City item)
        {
            string sql = $@"UPDATE Cities SET CityId=@CityId, CityName=@CityName
                            WHERE CityId=@CityId";
            //לבדוק שאין INJECTION
            this.dbContext.AddParamter("@CityId", item.CityId);
            this.dbContext.AddParamter("@CityName", item.CityName);
            return this.dbContext.Update(sql) > 0;
        }
    }
}
