using Models;
using System.Data;
namespace GymMachineWS
{
    public class GymMachineCreator : IModelCreator<GymMachine>
    {
        public GymMachine CreateMoldel(IDataReader reader)
        {
            GymMachine gymMachine = new GymMachine();
            gymMachine.MachineName = Convert.ToString(reader["MachineName"]);
            gymMachine.MachineId = Convert.ToString(reader["MachineId"]);
            gymMachine.MachineDescription = Convert.ToString(reader["MachineDescription"]);
            gymMachine.MachinePrice = Convert.ToString(reader["MachinePrice"]);
            gymMachine.MachineImage = Convert.ToString(reader["MachineImage"]);
            gymMachine.BrandId = Convert.ToString(reader["BrandId"]);
            return gymMachine;
        }
        
    }
}
