using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler :
        IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(
            IUnitOfWork unitOfWork,
            IUserService userService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto!);

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed!";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = await _unitOfWork.LeaveTypeRepository.GetAsync(request.LeaveAllocationDto.LeaveTypeId);
                var employees = await _userService.GetEmployees();
                var period = DateTime.Now.Year;
                var allocations = new List<Domain.LeaveAllocation>();

                foreach (var emp in employees)
                {
                    if (await _unitOfWork.LeaveAllocationRepository.AllocationExists(emp.Id, leaveType.Id, period))
                        continue;

                    allocations.Add(new Domain.LeaveAllocation
                    {
                        EmployeeId = emp.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberOfDays = leaveType.DefaultDays,
                        Period = period,
                    });
                }

                await _unitOfWork.LeaveAllocationRepository.AddAllocations(allocations);

                response.Success = true;
                response.Message = "Allocations Successful";
            }
            return response;
        }
    }
}
