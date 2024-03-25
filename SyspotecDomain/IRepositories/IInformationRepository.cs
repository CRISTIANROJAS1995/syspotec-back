using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IRepositories
{
    public interface IInformationRepository
    {
        Task<List<Information>?> GetAll();
        Task<Information?> GetById(int id);
        Task<int?> Add(Information model);
        Task<int?> Update(Information model);
        Task<int?> Delete(Information model);
    }
}
