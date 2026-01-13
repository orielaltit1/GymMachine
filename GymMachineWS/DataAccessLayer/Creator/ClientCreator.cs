using Models;
using System.Data;
namespace GymMachineWS;

public class ClientCreator : IModelCreator<Client>
{
    public Client CreateMoldel(IDataReader reader)
    {
        return new Client()
        {
            ClientAdress = Convert.ToString(reader["ClientAdress"]),
            CityId = Convert.ToInt16(reader["ClientCity"]),
            ClientEmail = Convert.ToString(reader["ClientEmil"]),
            ClientFirstName = Convert.ToString(reader["ClientFirstName"]),
            ClientGender = Convert.ToString(reader["ClientGender"]),
            ClientId = Convert.ToString(reader["ClientId"]),
            ClientLastName = Convert.ToString(reader["ClientLastName"]),
            ClientPassword = Convert.ToString(reader["ClientPassword"]),
            ClientSalt = Convert.ToString(reader["ClientSalt"]),
        };
    }
}
