using Cursus_Data.Context;
using Cursus_Data.Models.Entities;
using Cursus_Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursus_Data.Repositories.Implements
{
    public class ImageRepository : IImageRepository
    {
        private readonly LMS_CursusDbContext _context;
        public ImageRepository(LMS_CursusDbContext context)
        {
            _context = context;
        }

        public async Task AddImage(Image image)
        {
            try
            {
                _context.Images.Add(image);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
