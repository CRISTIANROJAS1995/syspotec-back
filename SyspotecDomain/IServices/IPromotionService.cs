using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IServices
{
    public interface IPromotionService
    {
        Task<ResponseApiDto?> Add(Promotion request);
        Task<ResponseApiDto?> Update(Promotion request);
        Task<ResponseApiDto?> Delete(Promotion request);
        Task<List<Promotion>?> GetAll();
    }
}
