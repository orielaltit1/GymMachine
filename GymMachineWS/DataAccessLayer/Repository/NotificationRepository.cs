using Models.Models;
using System.Data;

namespace GymMachineWS.DataAccessLayer
{
    public class NotificationRepository : Repository, IRepository<Notification>
    {
        public NotificationRepository(IDbContext dbContext, ModelFactory factoryModles) : base(dbContext, factoryModles)
        {
        }

        public bool Create(Notification item)
        {
            string sql = $@"INSERT INTO Notification
                            ( 
                            NotificationId, ClientId, NotificationText, CreatedDate, ReadStatus
                            )
                            VALUES
                            (
                            @NotificationId, @ClientId, @NotificationText, @CreatedDate, @ReadStatus
                            )";
            this.dbContext.AddParamter("@NotificationId", item.NotificationId.ToString());
            this.dbContext.AddParamter("@ClientId", item.ClientId.ToString());
            this.dbContext.AddParamter("@NotificationText", item.NotificationText);
            this.dbContext.AddParamter("@CreatedDate", item.CreatedDate);
            this.dbContext.AddParamter("@ReadStatus", item.ReadStatus.ToString());
            return this.dbContext.Insert(sql) > 0;

        }

        public bool Delete(string id)
        {
            string sql = $@"Delete FROM Notification 
                            WHERE NotificationId = @NotificationId";
            this.dbContext.AddParamter("@NotificationId", id);
            return this.dbContext.Delete(sql) > 0;
        }

        public List<Notification> GetAll()
        {
            List<Notification> notifications = new List<Notification>();
            string sql = "SELECT * FROM Notification";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    notifications.Add(this.factoryModles.NotificationCreator.CreateMoldel(reader));
                }
            }
            return notifications;
        }

        public Notification GetById(string id)
        {
            string sql = $@"SELECT *  FROM Notification where NotificationId=@NotificationId";
            this.dbContext.AddParamter("@NotificationId", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.factoryModles.NotificationCreator.CreateMoldel(reader);
            }
        }

        public bool Update(Notification item)
        {
            string sql = $@"UPDATE Notification SET NotificationId=@NotificationId, ClientId=@ClientId,
                            NotificationText=@NotificationText, CreatedDate=@CreatedDate, ReadStatus=@ReadStatus   
                            WHERE NotificationId=@NotificationId";
            //לבדוק שאין INJECTION
            this.dbContext.AddParamter("@NotificationId", item.NotificationId.ToString());
            this.dbContext.AddParamter("@ClientId", item.ClientId.ToString());
            this.dbContext.AddParamter("@NotificationText", item.NotificationText);
            this.dbContext.AddParamter("@CreatedDate", item.CreatedDate);
            this.dbContext.AddParamter("@ReadStatus", item.ReadStatus.ToString());
            return this.dbContext.Update(sql) > 0;
        }

        
    }
}
