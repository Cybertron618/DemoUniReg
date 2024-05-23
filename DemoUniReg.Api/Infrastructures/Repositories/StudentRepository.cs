using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoUniReg.Api.Models;
using DemoUniReg.Api.Data;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Newtonsoft.Json;
using DemoUniReg.Api.Infrastructures.Repositories;
using DemoUniReg.Api.Infrastructures.Services;

namespace DemoUniReg.Api.Infrastructures.Repositories
{
    public class StudentRepository(RecordContext context, IConnectionMultiplexer redisConnection) : IStudentRepository
    {
        private readonly RecordContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly IConnectionMultiplexer _redisConnection = redisConnection ?? throw new ArgumentNullException(nameof(redisConnection));

        // StudentDetail Methods
        public async Task<IEnumerable<StudentDetail>> GetStudentDetailsAsync()
        {
            return await _context.StudentDetails.ToListAsync();
        }

        public async Task<StudentDetail?> GetStudentDetailByIdAsync(int id)
        {
            var redisDatabase = _redisConnection.GetDatabase();
            var redisValue = await redisDatabase.StringGetAsync($"student:{id}");

            if (!redisValue.IsNullOrEmpty)
            {
                return JsonConvert.DeserializeObject<StudentDetail?>(redisValue!);
            }

            var studentDetail = await _context.StudentDetails.FindAsync(id);
            if (studentDetail != null)
            {
                await redisDatabase.StringSetAsync($"student:{id}", JsonConvert.SerializeObject(studentDetail));
            }

            return studentDetail;
        }

        public async Task CreateStudentDetailAsync(StudentDetail studentDetail)
        {
            await _context.StudentDetails.AddAsync(studentDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentDetailAsync(StudentDetail studentDetail)
        {
            _context.Entry(studentDetail).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var redisDatabase = _redisConnection.GetDatabase();
            await redisDatabase.StringSetAsync($"student:{studentDetail.StudentMIS}", JsonConvert.SerializeObject(studentDetail));
        }

        public async Task DeleteStudentDetailAsync(int id)
        {
            var studentDetail = await _context.StudentDetails.FindAsync(id);
            if (studentDetail != null)
            {
                _context.StudentDetails.Remove(studentDetail);
                await _context.SaveChangesAsync();

                var redisDatabase = _redisConnection.GetDatabase();
                await redisDatabase.KeyDeleteAsync($"student:{id}");
            }
        }

        public async Task<bool> StudentDetailExistsAsync(int id)
        {
            var redisDatabase = _redisConnection.GetDatabase();
            var redisKeyExists = await redisDatabase.KeyExistsAsync($"student:{id}");

            if (!redisKeyExists)
            {
                return await _context.StudentDetails.AnyAsync(s => s.StudentMIS == id);
            }

            return true;
        }

        // StudentMIS Methods
        public async Task<IEnumerable<StudentMIS>> GetStudentMISsAsync()
        {
            return await _context.StudentMISs.ToListAsync();
        }

        public async Task<StudentMIS?> GetStudentMISByIdAsync(int id)
        {
            var redisDatabase = _redisConnection.GetDatabase();
            var redisValue = await redisDatabase.StringGetAsync($"studentMIS:{id}");

            if (!redisValue.IsNullOrEmpty)
            {
                return JsonConvert.DeserializeObject<StudentMIS?>(redisValue!);
            }

            var studentMIS = await _context.StudentMISs.FindAsync(id);
            if (studentMIS != null)
            {
                await redisDatabase.StringSetAsync($"studentMIS:{id}", JsonConvert.SerializeObject(studentMIS));
            }

            return studentMIS;
        }

        public async Task CreateStudentMISAsync(StudentMIS studentMIS)
        {
            await _context.StudentMISs.AddAsync(studentMIS);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudentMISAsync(StudentMIS studentMIS)
        {
            _context.Entry(studentMIS).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var redisDatabase = _redisConnection.GetDatabase();
            await redisDatabase.StringSetAsync($"studentMIS:{studentMIS.SchoolID}", JsonConvert.SerializeObject(studentMIS));
        }

        public async Task DeleteStudentMISAsync(int id)
        {
            var studentMIS = await _context.StudentMISs.FindAsync(id);
            if (studentMIS != null)
            {
                _context.StudentMISs.Remove(studentMIS);
                await _context.SaveChangesAsync();

                var redisDatabase = _redisConnection.GetDatabase();
                await redisDatabase.KeyDeleteAsync($"studentMIS:{id}");
            }
        }

        public async Task<bool> StudentMISExistsAsync(int id)
        {
            var redisDatabase = _redisConnection.GetDatabase();
            var redisKeyExists = await redisDatabase.KeyExistsAsync($"studentMIS:{id}");

            if (!redisKeyExists)
            {
                return await _context.StudentMISs.AnyAsync(s => s.SchoolID == id);
            }

            return true;
        }
    }
}
