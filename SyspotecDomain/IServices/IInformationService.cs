using SyspotecDomain.Dtos;
using SyspotecDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyspotecDomain.IServices
{
    public interface IInformationService
    {
        Task<ResponseApiDto?> Add(Information request);
        Task<ResponseApiDto?> Update(Information request);
        Task<ResponseApiDto?> Delete(Information request);
        Task<List<Information>?> GetAll();
    }
}
