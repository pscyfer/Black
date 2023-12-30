using Microsoft.AspNetCore.Components;
using Radzen;
using System.ComponentModel.DataAnnotations;

namespace UpTRobot.Client.Pages
{
    public partial class MyServices
    {

        IEnumerable<OrderDetail> orderDetails;
        [Inject]
        public NavigationManager _NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            orderDetails = OrderDetail.OrderDetails;
        }

       
    }

    public class OrderDetail
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public static IEnumerable<OrderDetail> OrderDetails


        {
            get
            {
                var list = new List<OrderDetail>();
                list.Add(new OrderDetail
                {
                    Id = 1,
                    Name = "Madarso",
                    Address = "madarsho.com"
                });
                list.Add(new OrderDetail
                {
                    Id = 2,
                    Name = "busnetapp",
                    Address = "busnetapp.com"
                });
                list.Add(new OrderDetail
                {
                    Id = 3,
                    Name = "bugeto  ",
                    Address = "bugeto.net"
                });
                return list;
            }


        }
    }
}
