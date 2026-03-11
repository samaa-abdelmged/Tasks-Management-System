using Microsoft.EntityFrameworkCore;
using Tasks_Management_System.Infrastructure.Data;
using Tasks_Management_System.Application.Interfaces.Sections;
using Tasks_Management_System.Domain.Entities;

namespace Tasks_Management_System.Infrastructure.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly AppDbContext _context;

        public SectionRepository(AppDbContext context)
        {
            _context = context;
        }

        // إنشاء سكشن جديد
        public async Task<Section> CreateSectionAsync(Section section)
        {
            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();
            return section;
        }

        // جلب السكاشن اللي المستخدم Owner ليها
        public async Task<List<Section>> GetUserSectionsAsync(int userId)
        {
            return await _context.Sections
                        .Include(s => s.Owner)
                        .Where(s => s.OwnerId == userId)
                        .ToListAsync();
        }

        // جلب السكاشن المشتركة مع المستخدم
        public async Task<List<SectionShare>> GetSharedSectionsAsync(int userId)
        {
            return await _context.SectionShares
                        .Include(ss => ss.Section)
                        .ThenInclude(s => s.Owner)
                        .Where(ss => ss.UserId == userId)
                        .ToListAsync();
        }

        // جلب سكشن معين بالـ Id
        public async Task<Section> GetSectionByIdAsync(int sectionId)
        {
            return await _context.Sections
                        .Include(s => s.Tasks)
                        .Include(s => s.SharedWithUsers)
                        .FirstOrDefaultAsync(s => s.Id == sectionId);
        }

        // تعديل سكشن
        public async Task UpdateSectionAsync(Section section)
        {
            _context.Sections.Update(section);
            await _context.SaveChangesAsync();
        }

        // حذف سكشن
        public async Task DeleteSectionAsync(Section section)
        {
            _context.Sections.Remove(section);
            await _context.SaveChangesAsync();
        }

        // مشاركة سكشن مع مستخدم
        public async Task ShareSectionAsync(SectionShare sectionShare)
        {
            await _context.SectionShares.AddAsync(sectionShare);
            await _context.SaveChangesAsync();
        }

        // جلب مستخدم بالـ Email
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        //  للتحقق من الاسم
        public async Task<Section> GetSectionByNameAsync(int userId, string name)
        {
            return await _context.Sections
                .FirstOrDefaultAsync(s => s.OwnerId == userId && s.Name == name);
        }

        public async Task<bool> IsSectionSharedWithUser(int sectionId, int userId)
        {
            return await _context.SectionShares
                .AnyAsync(ss => ss.SectionId == sectionId && ss.UserId == userId);
        }
    }
}