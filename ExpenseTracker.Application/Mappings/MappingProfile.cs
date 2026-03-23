using AutoMapper;
using ExpenseTracker.Application.DTOs;
using ExpenseTracker.Core.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseTracker.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Expense, ExpenseDto>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<CreateExpenseDto, Expense>();
        }
    }
}