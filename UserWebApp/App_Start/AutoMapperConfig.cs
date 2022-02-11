using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserWebApp.Models;

namespace UserWebApp.App_Start
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; set; }

        public static void init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SubCategory, SubCategoryDto>();
            });

            Mapper = config.CreateMapper();
        }
    }
}