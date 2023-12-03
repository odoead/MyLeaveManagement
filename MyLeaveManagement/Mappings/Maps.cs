using AutoMapper;
using MyLeaveManagement.Data;
using MyLeaveManagement.Models;
using System.Runtime;

namespace MyLeaveManagement.Mappings
{
    public class Maps:Profile
    {


        public Maps()
        {
            CreateMap<LeaveType,LeaveTypeViewModel>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationViewModel>().ReverseMap();
            CreateMap<LeaveAllocation, EditLeaveAllocationViewModel>().ReverseMap();

        }
    }
}
