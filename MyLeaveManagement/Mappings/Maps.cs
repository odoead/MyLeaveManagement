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
            CreateMap<LeaveType,DetailsTypeViewModel>().ReverseMap();
            CreateMap<LeaveType, CreateTypeViewModel>().ReverseMap();
            CreateMap<LeaveHistory, LeaveHistoryViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationViewModel>().ReverseMap();
        }
    }
}
