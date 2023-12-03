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
            CreateMap<LeaveHistory, LeaveHistoryViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationViewModel>().ReverseMap();
        }
    }
}
