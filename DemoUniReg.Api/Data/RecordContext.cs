using Microsoft.EntityFrameworkCore;
using DemoUniReg.Api.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using StackExchange.Redis;
using DemoUniReg.Api.Infrastructures.Services;
using System.Threading.Tasks;

namespace DemoUniReg.Api.Data
{
    public class RecordContext(DbContextOptions<RecordContext> options, IConnectionMultiplexer redisConnection) : DbContext(options)
    {
        private readonly IConnectionMultiplexer _redisConnection = redisConnection;

        public DbSet<StudentDetail> StudentDetails { get; set; }
        public DbSet<StudentMIS> StudentMISs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity mappings, relationships, etc.
            modelBuilder.Entity<StudentDetail>().HasKey(s => s.StudentMIS);
            modelBuilder.Entity<StudentMIS>().HasKey(s => s.SchoolID); //Assuming StudentMIS is the primary key

            modelBuilder.Entity<StudentDetail>().ToTable("StudentDetails");
            modelBuilder.Entity<StudentMIS>().ToTable("StudentMISs");

            // Add any additional configuration here

            base.OnModelCreating(modelBuilder);
        }

        public async Task<StudentDetail?> GetStudentDetailByIdAsync(int id)
        {
            // Example of caching the result using Redis
            var redisDatabase = _redisConnection.GetDatabase();
            var cachedResult = await redisDatabase.StringGetAsync($"StudentDetail:{id}");

            if (!cachedResult.IsNullOrEmpty)
            {
                var deserializedResult = JsonConvert.DeserializeObject<StudentDetail>(cachedResult!);

                if (deserializedResult != null)
                {
                    return deserializedResult;
                }
            }

            var studentDetail = await StudentDetails.FindAsync(id);

            if (studentDetail != null)
            {
                // Cache the result in Redis
                await redisDatabase.StringSetAsync($"StudentDetail:{id}", JsonConvert.SerializeObject(studentDetail));
            }

            return studentDetail;
        }
    }
}
