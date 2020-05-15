using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace travel.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("UserId")]
        public virtual ICollection<BookingTour> TourDetails { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<TravelService> TravelServices { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Đây là nơi e viết thêm các service
        //A vừa add 2 model vào service, dùng lệnh database-update để đồng bộ sang database
        //Mấy cái đấy đâu cần đâu @@ a ví dụ mẫu ở trên rồi kìa, Vì răng e lại add nhiều vậy
        //Em tạo Model rồi tạo Controller xong nó tự ra View cho em như lời anh nói đó :|

        public DbSet<Post> Posts { get; set; }  //Cái chi đây???????????????????????????
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<CategoryTour> CategoryTours { get; set; }
        public DbSet<BookingTour> TourDetails { get; set; } //2 thằng này e tự add hay họ tự tạo, họ tạo anh, em chưa làm gì trong file này cả
        public DbSet<TravelService> TravelServices { get; set; } //E tự add vào đây mới chạy được

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


    }
}