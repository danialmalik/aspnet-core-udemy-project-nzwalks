using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(
            string? filterOn = null,
            string? filterQuery = null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 100
        )
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            // Filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }

                // switch (filterOn.ToLower())
                // {
                //     case "name":
                //         walks = walks.Where(x => x.Name.ToLower().Contains(filterQuery.ToLower()));
                //         break;
                //     case "description":
                //         walks = walks.Where(x => x.Description.ToLower().Contains(filterQuery.ToLower()));
                //         break;
                //     case "lengthinkm":
                //         walks = walks.Where(x => x.LengthInKm.ToString().ToLower().Contains(filterQuery.ToLower()));
                //         break;
                //     case "region":
                //         walks = walks.Where(x => x.Region.Name.ToLower().Contains(filterQuery.ToLower()));
                //         break;
                //     case "difficulty":
                //         walks = walks.Where(x => x.Difficulty.Name.ToLower().Contains(filterQuery.ToLower()));
                //         break;
                //     default:
                //         break;
                // }
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x=>x.Name) : walks.OrderByDescending(x=>x.Name);
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x=>x.LengthInKm) : walks.OrderByDescending(x=>x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;


            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            // return await dbContext.Walks.Include(x => x.Difficulty).Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks
                .Include(x => x.Difficulty)
                .Include("Region")
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null)
            {
                return null;
            }
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;

            await dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
