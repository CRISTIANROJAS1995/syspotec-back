using SyspotecDomain.Dtos;
using SyspotecDomain.Dtos.Generic;
using SyspotecDomain.Dtos.User;
using SyspotecDomain.Entities;
using SyspotecDomain.IRepositories;
using SyspotecUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Web;

namespace SyspotecDal.Repository
{
    public class HomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<Information?> GetById(int id)
        //{
        //    return await _context.Information.FirstOrDefaultAsync(r => r.Id == id);
        //}

    }
}
