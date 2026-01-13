using GymMachineWS.DataAccessLayer.Creator;

namespace GymMachineWS.DataAccessLayer
{
    public class ModelFactory
    {
        ClientCreator clientCreator;
        GymMachineBrandCreator gymMachineBrandCreator;
        GymMachineCreator gymMachineCreator;
        NotificationCreator notificationCreator;
        OrderCreator orderCreator;
        CartItemCreator itemCreator;
        CityCreator cityCreator;

        public CartItemCreator CartItemCreator
        {
            get
            {
                if (this.itemCreator == null)
                    this.itemCreator = new CartItemCreator();
                return this.itemCreator;
            }
        }
        public ClientCreator ClientCreator
        {
            get
            {
                if(this.clientCreator == null)
                    this.clientCreator = new ClientCreator();
                return this.clientCreator;
            }
        }
        public GymMachineCreator GymMachineCreator
        {
            get
            {
                if (this.gymMachineCreator == null)
                    this.gymMachineCreator = new GymMachineCreator();
                return this.gymMachineCreator;
            }
        }
        public GymMachineBrandCreator GymMachineBrandCreator
        {
            get
            {
                if( this.gymMachineBrandCreator == null)
                    this.gymMachineBrandCreator = new GymMachineBrandCreator();
                return this.gymMachineBrandCreator;
            }
        }
        public NotificationCreator NotificationCreator
        {
            get
            {
                if(this.notificationCreator == null)
                    this.notificationCreator = new NotificationCreator();
                return this.notificationCreator;
            }
        }
        public OrderCreator OrderCreator
        {
            get
            {
                if(this.orderCreator == null)
                    this.orderCreator = new OrderCreator();
                return this.orderCreator;
            }
        }
        public CityCreator CityCreator
        {
            get
            {
                if (this.cityCreator == null)
                    this.cityCreator = new CityCreator();
                return this.cityCreator;
            }
        }
    }
}
