using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using CompanyName.ProjectName.NotificationManagement.Notifications.Dtos;

namespace CompanyName.ProjectName.NotificationManagement.Notifications
{
    public interface ICommandNotificationAppService : IApplicationService
    {
        /// <summary>
        /// 消息设置为已读
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SetReadAsync(SetReadInput input);
        
        /// <summary>
        /// 创建一个消息
        /// 测试使用
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateAsync(CreateNotificationInput input);
    }
}