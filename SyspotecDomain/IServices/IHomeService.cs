using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Hibeat;
using SyspotecDomain.Dtos.Home;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IServices
{
    public interface IHomeService
    {
        Task<List<dynamic>?> SearchData(PaginationDto pagination, HiBeatFilterDto request);
        Task<List<ReactionResponseDto>?> GetNotifications(string userId);
        Task<CountNotificationDto> GetCountNotifications(string userId);
        Task<ResponseApiDto?> SendEmailBuys(string userId);
    }
}
