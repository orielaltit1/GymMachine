using GymMachineWS.DataAccessLayer;
using Microsoft.AspNetCore.SignalR;
using Models;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;

namespace GymMachineWS
{
    public class ClientRepository : Repository, IRepository<Client>
    {
        public ClientRepository(IDbContext dbContext, ModelFactory factoryModles) : base(dbContext, factoryModles)
        {

        }

        public bool Create(Client item)
        {
            string sql = $@"INSERT INTO Clients
                            ( 
                            ClientFirstName, ClientLastName, ClientId, 
                            ClientGender, ClientEmail, ClientPassword, ClientPicture
                            ClientAdress, CityId, ClientSalt
                            )
                            VALUES
                            (
                            @ClientFirstName, @ClientLastName, @ClientId, 
                            @ClientGender, @ClientEmail, @ClientPassword, @ClientPicture
                            @ClientAdress, @CityId, @ClientSalt
                            )";
            this.dbContext.AddParamter("@ClientFirstName", item.ClientFirstName);
            this.dbContext.AddParamter("@ClientLastName", item.ClientLastName);
            this.dbContext.AddParamter("@ClientId", item.ClientId);
            this.dbContext.AddParamter("@ClientGender", item.ClientGender);
            this.dbContext.AddParamter("@ClientEmail", item.ClientEmail);
            this.dbContext.AddParamter("@ClientPassword", item.ClientPassword);
            this.dbContext.AddParamter("@ClientPicture", item.ClientPicture);
            this.dbContext.AddParamter("@ClientAdress", item.ClientAdress);
            this.dbContext.AddParamter("@CityId", item.CityId);
            string salt = GanerateSalt();
            this.dbContext.AddParamter("@ClientPassword", CalculateHash(item.ClientPassword, salt));
            this.dbContext.AddParamter("@ClientSalt", salt);
            return this.dbContext.Insert(sql) > 0;
        }

        private string CalculateHash(string password, string salt)
        {
            string s = password + salt;
            byte[] pass = System.Text.Encoding.UTF8.GetBytes(s);//UTF8 טבלה של הסימנים(כל האותיות מכל השפות וסימנים כמו שטרודל והאשטאג ועוד)
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(pass);
                return Convert.ToBase64String(bytes);
            }
        }

        private string GanerateSalt()//מחשב את המלח
        {
            byte[] saltByte = new byte[16];
            RandomNumberGenerator.Fill(saltByte);
            return Convert.ToBase64String(saltByte);
        }

        public bool Delete(string id)
        {
            string sql = $@"Delete FROM Clients 
                            WHERE ClientId = @clientID";
            this.dbContext.AddParamter("@clientID", id);
            return  this.dbContext.Delete(sql)>0;
        }
        public List<Client> GetAll()
        {
            List<Client> clients = new List<Client>();
            string sql = "SELECT * FROM Clients";
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                while (reader.Read())
                {
                    clients.Add(this.factoryModles.ClientCreator.CreateMoldel(reader));
                }
            }
            return clients;
        }

        public Client GetById(string id)
        {
            string sql = $@"SELECT *  FROM Clients where clientId=@ClientId";
            this.dbContext.AddParamter("@clientID", id.ToString());
            using (IDataReader reader = this.dbContext.Select(sql))
            {
                reader.Read();
                return this.factoryModles.ClientCreator.CreateMoldel(reader);
            }
        }
           
        public bool Update(Client item)
        {
            string sql = $@"UPDATE Clients SET ClientFirstName=@ClientFirstName, ClientLastName=@ClientLastName,
                            ClientGender=@ClientGender, ClientEmail=@ClientEmail,  
                            ClientPassword=@ClientPassword, ClientPicture=@ClientPicture, ClientAdress=@ClientAdress, CityId=@CityId
                            WHERE ClientId=@ClientId";
            //לבדוק שאין INJECTION
            this.dbContext.AddParamter("@ClientFirstName", item.ClientFirstName);
            this.dbContext.AddParamter("@ClientLastName", item.ClientLastName);
            this.dbContext.AddParamter("@ClientGender", item.ClientGender);
            this.dbContext.AddParamter("@ClientEmail", item.ClientEmail);
            this.dbContext.AddParamter("@ClientPassword", item.ClientPassword);
            this.dbContext.AddParamter("@ClientPicture", item.ClientPicture);
            this.dbContext.AddParamter("@ClientAdress", item.ClientAdress);
            this.dbContext.AddParamter("@CityId", item.CityId);
            this.dbContext.AddParamter("@ClientId", item.ClientId);
            return this.dbContext.Update(sql) > 0;


        }

        public string Login(string email, string password)
        {
            string sql = $@"SELECT ClientId, ClientPassword, ClientSalt FROM Clients
                            WHERE ClientEmail=@ClientEmail";
            this.dbContext.AddParamter("@ClientEmail", email);
            string hash = string.Empty;
            string salt = string.Empty;
            string readerId= string.Empty;
            using(IDataReader reader = this.dbContext.Select(sql))
            {
                if(reader.Read() == true)
                {
                    salt = reader["ClientSalt"].ToString();
                    hash = reader["ClientPassword"].ToString();
                    readerId = reader["ClientId"].ToString();
                }
            }
            if(hash == CalculateHash(password, salt))
            {
                return readerId;
            }
            return null;

            
        }

        
    }
}
