using HR.LeaveManagement.Application.Persistence.Contracts;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly HRLeaveManagementDbContext _context;

        public LeaveRequestRepository(HRLeaveManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? approvalStatus)
        {
            leaveRequest.Approved = approvalStatus;
            _context.Entry(leaveRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            var LeaveRequest = await _context.LeaveRequests
                .Include(e => e.LeaveType)
                .ToListAsync();

            return LeaveRequest;
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            var LeaveRequest = await _context.LeaveRequests
                .Include(e => e.LeaveType)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));

            return LeaveRequest!;
        }
    }
}