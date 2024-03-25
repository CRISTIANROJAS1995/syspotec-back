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
    public class InformationRepository : IInformationRepository
    {
        private readonly ApplicationDbContext _context;

        public InformationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Information>?> GetAll()
        {
            var response = await _context.Information.OrderByDescending(c => c.Id).ToListAsync();
            if (response.Count > 0) {
                foreach (Information item in response)
                {
                    item.TitleEnglish = item.TitleEnglish.Trim();
                    item.TitleSpanish = item.TitleSpanish.Trim();
                    item.Text1English = item.Text1English.Trim();
                    item.Text1Spanish = item.Text1Spanish.Trim();
                    item.Text2English = item.Text2English.Trim();
                    item.Text2Spanish = item.Text2Spanish.Trim();
                    item.SubtitleEnglish = item.SubtitleEnglish.Trim();
                    item.SubtitleSpanish = item.SubtitleSpanish.Trim();
                }
            }
            return response;
        }

        public async Task<Information?> GetById(int id)
        {
            return await _context.Information.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int?> Add(Information model)
        {
            await _context.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Update(Information model)
        {
            _context.Update(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int?> Delete(Information model)
        {
            _context.Remove(model);
            return await _context.SaveChangesAsync();
        }


    }
}
