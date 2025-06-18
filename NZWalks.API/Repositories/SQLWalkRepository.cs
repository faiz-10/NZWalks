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
            var walk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if (walk == null)
            {
                return null;
            }

            dbContext.Walks.Remove(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Apply filtering if specified
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrEmpty(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(w => w.Name.Contains(filterQuery));
                }
            }

            // Apply sorting if specified
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(w => w.Name) : walks.OrderByDescending(w => w.Name);
                }
                else if(sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(w => w.LengthInKm) : walks.OrderByDescending(w => w.LengthInKm);
                }
            }

            // Apply pagination
            var skipResults = (pageNumber - 1) * pageSize;

            //var walks = await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            var walk = await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w => w.Id == id);
            return walk;

        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(w => w.Id == id);
            if(existingWalk is null)
            {
                return null;
            }
            // Update the properties of the existing walk
            existingWalk.Name = walk.Name;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.RegionId = walk.RegionId;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.Description = walk.Description;
            existingWalk.WalkImageURL = walk.WalkImageURL;

            // Save changes to the database
            await dbContext.SaveChangesAsync();

            return existingWalk;

        }
    }
}
