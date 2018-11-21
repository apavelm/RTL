using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RTLTest.Services.AutoMapper;
using RTLTestTask.ApiModels;
using RTLTestTask.Db;

namespace RTLTest.Services
{
    public class DataService : IDataService
    {
        private readonly RTLDbContext _dbContext;
        private readonly ModelMapper _modelMapper;

        public DataService(RTLDbContext dbContext, ModelMapper modelMapper)
        {
            _dbContext = dbContext;
            _modelMapper = modelMapper;
        }

        public async Task<TVShowResponse> GetShow(int id)
        {
            var result = await _dbContext.TVShows
                .AsNoTracking()
                .Include(a => a.ShowCasts)
                .ThenInclude(b => b.Cast)
                .ProjectTo<TVShowResponse>(_modelMapper.Configuration)
                .FirstOrDefaultAsync(s => s.Id == id);
            
            if (result == null) return null;

            result.Casts = result.Casts.OrderByDescending(s => s.DoB).ToList();

            return result;
        }

        public async Task<List<TVShowResponse>> GetShowList(int page = 1, int pageSize = 20)
        {
            if (page < 1) page = 1;

            var resultList =  await _dbContext.TVShows
                .AsNoTracking()
                .Include(a => a.ShowCasts)
                .ThenInclude(b => b.Cast)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TVShowResponse>(_modelMapper.Configuration)
                .ToListAsync();

            if (resultList == null) return new List<TVShowResponse>();

            foreach (var item in resultList)
            {
                item.Casts = item.Casts.OrderByDescending(s => s.DoB).ToList();
            }

            return resultList;
        }
    }
}
