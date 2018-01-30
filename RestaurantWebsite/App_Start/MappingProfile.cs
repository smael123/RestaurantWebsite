using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Dtos;

namespace RestaurantWebsite.App_Start
{
    public class MappingProfile
    {
        //Mapper.CreateMap<Food, FoodDto>();

        protected readonly IMapper _mapper;

        protected MappingProfile() {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Food, FoodDto>();
                cfg.CreateMap<FoodDto, Food>();

                cfg.CreateMap<Extra, ExtraDto>();
                cfg.CreateMap<ExtraDto, Extra>();

                cfg.CreateMap<Special, SpecialDto>();
                cfg.CreateMap<SpecialDto, Special>();
            });

            _mapper = config.CreateMapper();

            //IMapper mapper = config.CreateMapper();
            //var source = new Food();
            //var dest = mapper.Map<Food, FoodDto>(new Food());

            //mapper.Map<Food, FoodDto>(new Food());
            //mapper.Map<FoodDto, Food>(new FoodDto());
            //mapper.Map<Extra, ExtraDto>(new Extra());
            //mapper.Map<ExtraDto, Extra>(new ExtraDto());
            //mapper.Map<Special, SpecialDto>(new Special());
            //mapper.Map<SpecialDto, Special>(new SpecialDto());
        }

        
    }

       
}