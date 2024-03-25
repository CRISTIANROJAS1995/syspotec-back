using SyspotecDomain.Dtos.User;
using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IPromotionRepository
    {
        Task<List<Promotion>?> GetAll();
        Task<Promotion?> GetById(int id);
        Task<int?> Add(Promotion model);
        Task<int?> Update(Promotion model);
        Task<int?> Delete(Promotion model);
    }
}
