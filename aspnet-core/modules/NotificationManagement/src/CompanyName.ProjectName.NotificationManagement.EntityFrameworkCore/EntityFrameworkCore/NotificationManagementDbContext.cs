using CompanyName.ProjectName.NotificationManagement.Notifications;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace CompanyName.ProjectName.NotificationManagement.EntityFrameworkCore
{
    [ConnectionStringName(NotificationManagementDbProperties.ConnectionStringName)]
    public class NotificationManagementDbContext : AbpDbContext<NotificationManagementDbContext>, INotificationManagementDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public NotificationManagementDbContext(DbContextOptions<NotificationManagementDbContext> options) 
            : base(options)
        {

        }
        public DbSet<Notification> Questions { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureNotificationManagement();
        }

      
    }
}