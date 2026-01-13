using Models;
using System.Data;

namespace GymMachineWS
{
    public class GymMachineBrandCreator : IModelCreator<GymMachineBrand>
    {
        public GymMachineBrand CreateMoldel(IDataReader reader)
        {
            return new GymMachineBrand()
            {
                BrandId = Convert.ToInt16(reader["BrandId"]),
                BrandLogo = Convert.ToString(reader["BrandLogo"]),
                BrandName = Convert.ToString(reader["BrandName"]),
            };

        }
    }
}
