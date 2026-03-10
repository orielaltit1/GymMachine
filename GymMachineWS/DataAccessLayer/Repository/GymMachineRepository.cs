using Models;
using System.Data;

namespace GymMachineWS.DataAccessLayer
{
    public class GymMachineRepository : Repository, IRepository<GymMachine>
    {
        public GymMachineRepository(IDbContext dbContext, ModelFactory factoryModles) : base(dbContext, factoryModles)
        {
        }

        public bool Create(GymMachine item)
        {
            string sql = $@"INSERT INTO GymMachine
                            ( 
                            MachineName, MachineId, MachineDescription, MachinePrice, MachineImage, BrandId
                            )
                            VALUES
                            (
                            @machineName, @machineId, @machineDescription, 
                            @bachinePrice, @machineImage, @brandId            
                            )";
            this.dbContext.AddParamter("@machineName", item.MachineName);
            this.dbContext.AddParamter("@machineId", item.MachineId.ToString());
            this.dbContext.AddParamter("@machineDescription", item.MachineDescription);
            this.dbContext.AddParamter("@machinePrice", item.MachinePrice);
            this.dbContext.AddParamter("@machineImage", item.MachineImage);
            this.dbContext.AddParamter("@brandId", item.BrandId.ToString());
            return this.dbContext.Insert(sql) > 0;
        }

        public bool Delete(string id)
        {
            string sql = $@"UPDATE GymMachine
                            SET IsActive = No
                            WHERE MachineId = @MachineId;";
            this.dbContext.AddParamter("@machineId", id);
            return this.dbContext.Delete(sql) > 0;
        }

        public List<GymMachine> GetAll(string sort="-1")
        {
            List<GymMachine> gymMachines = new List<GymMachine>();
            string sql = $@"SELECT * FROM GymMachine";
            if (sort == "1")
                sql += " order by MachinePrice";
            else if (sort == "2")
                sql += " order by MachinePrice desc";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    gymMachines.Add(this.factoryModles.GymMachineCreator.CreateMoldel(reader));
                }
            }
            return gymMachines;
        }


        public List<GymMachine> GetAll()
        {
            List<GymMachine> gymMachines = new List<GymMachine>();
            string sql = $@"SELECT * FROM GymMachine";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    gymMachines.Add(this.factoryModles.GymMachineCreator.CreateMoldel(reader));
                }
            }
            return gymMachines;
        }

        public List<GymMachine> GetAllActives()
        {
            List<GymMachine> gymMachines = new List<GymMachine>();
            string sql = $@"SELECT
                                GymMachine.*
                            FROM
                                GymMachine
                            WHERE
                                IsActive = True;";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    gymMachines.Add(this.factoryModles.GymMachineCreator.CreateMoldel(reader));
                }
            }
            return gymMachines;

        }

        public GymMachine GetById(string id)
        {
            string sql = $@"SELECT * FROM GymMachine WHERE MachineId=@machineId";
            this.dbContext.AddParamter("@machineId", id.ToString());
            using(IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.factoryModles.GymMachineCreator.CreateMoldel(reader);
            }
        }

        public bool Update(GymMachine item)
        {
            string sql = $@"UPDATE GymMachine SET MachineName=@machineName, MachineId=@machineId, 
                            MachineDescription=@machineDescription, MachinePrice=@machinePrice, 
                            MachineImage=@machineImage, BrandId=@brandId 
                            WHERE MachineId=@machineId";
            this.dbContext.AddParamter("@machineName", item.MachineName);
            this.dbContext.AddParamter("@machineId", item.MachineId.ToString());
            this.dbContext.AddParamter("@machineDescription", item.MachineDescription);
            this.dbContext.AddParamter("@machinePrice", item.MachinePrice);
            this.dbContext.AddParamter("@machineImage", item.MachineImage);
            this.dbContext.AddParamter("@brandId", item.BrandId.ToString());
            return this.dbContext.Update(sql) > 0;
        }

        public List<GymMachine>? GetMachineByBrand(string selectedBrandId, string sort= "-1")
            // -1 -no sort, 1 - sort low to hight, 2 - sort hight  to low
        {
            List<GymMachine> gymMachines = new List<GymMachine>();
            string sql = $@"SELECT * FROM GymMachine WHERE brandId=@selectedBrandId";
            this.dbContext.AddParamter("@selectedBrandId", selectedBrandId);
            if (sort == "1")
                sql += " order by MachinePrice";
            else if(sort == "2")
                    sql += " order by MachinePrice desc";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    gymMachines.Add(this.factoryModles.GymMachineCreator.CreateMoldel(reader));
                }
            }
            return gymMachines;
        }
    }
}
