using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace publisher_subscriber_design_battern_Order_event
{
    public class OrderArgs : EventArgs
    {
        public int OrderId { get; }
        public string OrderName { get; }
        public double OrderPrice { get; }

        public OrderArgs (int orderId, string orderName, double orderPrice)
        {
            this.OrderId = orderId;
            this.OrderName = orderName;
            this.OrderPrice = orderPrice;
        }
    }

    public class Order
    {
        public event EventHandler<OrderArgs> OnOrderCreated;

       public void CreateOrder(int OrderID , string OrderName ,double OrderPrice)
        {
            Console.WriteLine("New Order Created Now :\n");
            OnOrderCreated?.Invoke(this, new OrderArgs(OrderID, OrderName, OrderPrice));
        }
    }

    public interface Subscribers
    {
        void Subscribe(Order order);
        void unSubscribe(Order order);
        void HandelOrder(object sender, OrderArgs e);
    }


    public class EmailServers : Subscribers 
    {
        
        public void Subscribe(Order order)
        {
            order.OnOrderCreated += HandelOrder;
        }

        public void unSubscribe(Order order)
        {
            order.OnOrderCreated-= HandelOrder;
        }

        public void HandelOrder(object sender, OrderArgs e)
        {
            Console.WriteLine("Email Order Servers is handel");
            Console.WriteLine($"Order ID : {e.OrderId} ");
            Console.WriteLine($"Order Name : {e.OrderName}");
            Console.WriteLine($"Order Price : { e.OrderPrice}");
            Console.WriteLine("=================================\n");
        }
    }


    public class SMSServers : Subscribers
    {
        public void Subscribe(Order order)
        {
            order.OnOrderCreated += HandelOrder;
        }

        public void unSubscribe(Order order)
        {
            order.OnOrderCreated -= HandelOrder;
        }

        public void HandelOrder(object sender, OrderArgs e)
        {
            Console.WriteLine("SMSservers Order Servers is handel");
            Console.WriteLine($"Order ID : {e.OrderId} ");
            Console.WriteLine($"Order Name : {e.OrderName}");
            Console.WriteLine($"Order Price : {e.OrderPrice}");
            Console.WriteLine("=================================\n");
        }
    }

    public class ShippingServers : Subscribers
    {
        public void Subscribe(Order order)
        {
            order.OnOrderCreated += HandelOrder;
        }

        public void unSubscribe(Order order)
        {
            order.OnOrderCreated -= HandelOrder;
        }

        public void HandelOrder(object sender, OrderArgs e)
        {
            Console.WriteLine("Shipping Serves Order Servers is handel");
            Console.WriteLine($"Order ID : {e.OrderId} ");
            Console.WriteLine($"Order Name : {e.OrderName}");
            Console.WriteLine($"Order Price : {e.OrderPrice}");
            Console.WriteLine("=================================\n");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Order order = new Order();

            EmailServers emailServers = new EmailServers();

            SMSServers sMSServers = new SMSServers();

            ShippingServers shippingServers = new ShippingServers();

            shippingServers.Subscribe(order);
            sMSServers.Subscribe(order);
            emailServers.Subscribe(order);
            order.CreateOrder(1, "Phone", 1200);

            order.CreateOrder(2, "Pc", 19.550);

            Console.ReadLine();
        }
    }
}
