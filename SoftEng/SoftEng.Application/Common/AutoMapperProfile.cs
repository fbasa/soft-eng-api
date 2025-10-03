using AutoMapper;
using SoftEng.Domain.Response;

namespace SoftEng.Application.Common;

public sealed class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //TODO
        //CreateMap<TSource, TDestination>();
        CreateMap<GetStudentListResult, GetStudentResult>();
    }
}
