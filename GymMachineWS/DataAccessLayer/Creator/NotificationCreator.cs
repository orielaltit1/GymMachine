using Models.Models;
using System.Data;
using System.Xml;

namespace GymMachineWS
{
    public class NotificationCreator : IModelCreator<Notification>
    {
        public Notification CreateMoldel(IDataReader reader)
        {
            return new Notification()
            {
                NotificationId = Convert.ToInt16(reader["MessegeId"]),
                ClientId = Convert.ToInt16(reader["MessegeText"]),
                NotificationText = Convert.ToString(reader["DraftingAMessge"]),
                CreatedDate = Convert.ToString(reader["RecipientOfTheMessege"]),
                ReadStatus = Convert.ToBoolean(reader["RecipientOfTheMessege"])
            };
        }
    }
}
