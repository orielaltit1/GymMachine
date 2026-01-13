using Models;
using System.Data;

namespace GymMachineWS.DataAccessLayer.Creator
{
    public class CityCreator : IModelCreator<City>
    {
        public City CreateMoldel(IDataReader reader)
        {
            return new City()
            {
                CityId = Convert.ToInt16(reader["CityId"]),
                CityName = Convert.ToString(reader["CityName"])
            };
        }
    }
}
