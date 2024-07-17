using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly HRLeaveManagementDbContext _context;

        public LeaveAllocationRepository(HRLeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _context.AddRangeAsync(allocations);
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
                                        && q.LeaveTypeId == leaveTypeId
                                        && q.Period == period);
        }


        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocation = await _context.LeaveAllocations
                .Include(e => e.LeaveType)
                .ToListAsync();

            return leaveAllocation;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocation = await _context.LeaveAllocations
                .Include(e => e.LeaveType)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            return leaveAllocation!;
        }
    }
}
