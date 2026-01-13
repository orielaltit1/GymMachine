namespace GymMachineWS.DataAccessLayer
{
    public class RepositoryUnitOfWork
    {
        OleDbContext oleDbContext;
        ModelFactory modelFactory;

        ClientRepository clientRepository;
        GymMachineRepository gymMachineRepository;
        NotificationRepository messegeRepository;
        GymMachineBrandRepository brandRepository;
        OrderRepository orderRepository;
        CityRepository cityRepository;
        public RepositoryUnitOfWork()
        {
            this.oleDbContext = new OleDbContext();
            this.modelFactory = new ModelFactory();
        }

        public ClientRepository ClientRepository
        {
            get
            {
                if (this.clientRepository == null)
                {
                    this.clientRepository = new ClientRepository(this.oleDbContext, this.modelFactory);
                }
                return clientRepository;
            }
        }
        public CityRepository CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                {
                    this.cityRepository = new CityRepository(this.oleDbContext, this.modelFactory);
                }
                return cityRepository;
            }
        }
        public GymMachineRepository GymMachineRepository
        {
            get
            {
                if (this.gymMachineRepository == null)
                {
                    this.gymMachineRepository = new GymMachineRepository(this.oleDbContext, this.modelFactory);
                }
                return gymMachineRepository;
            }
        }

        public NotificationRepository MessegeRepository
        {
            get
            {
                if (this.messegeRepository == null)
                {
                    this.messegeRepository = new NotificationRepository(this.oleDbContext, this.modelFactory);
                }
                return messegeRepository;
            }
        }

        public GymMachineBrandRepository GymMachineBrandRepository
        {
            get
            {
                if (this.brandRepository == null)
                {
                    this.brandRepository = new GymMachineBrandRepository(this.oleDbContext, this.modelFactory);
                }
                return brandRepository;
            }
        }

        public OrderRepository OrderRepository
        {
            get
            {
                if (this.orderRepository == null)
                {
                    this.orderRepository = new OrderRepository(this.oleDbContext, this.modelFactory);
                }
                return orderRepository;
            }
        }


        public void ConnectDb()
        {
            this.oleDbContext.OpenConnection();
        }
        public void DisconnectDb()
        {
            this.oleDbContext.CloseConnection();
        }
    }
}
