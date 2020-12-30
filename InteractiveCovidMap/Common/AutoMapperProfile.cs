using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CovidTracking.HelperModels;
using CovidTracking.Models;

namespace CovidTracking.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CurrentStateJson, CurrentState>();
        }

    }
}
