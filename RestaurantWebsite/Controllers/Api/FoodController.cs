using AutoMapper;
using RestaurantWebsite.Core;
using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using RestaurantWebsite.Dtos;
using RestaurantWebsite.Persistence;
using RestaurantWebsite.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace RestaurantWebsite.Controllers.Api
{
    public class FoodController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        //public FoodController(IUnitOfWork unitOfWork) {
        //    _unitOfWork = unitOfWork;

        //    //automapper initaizaliton
        //    var config = new MapperConfiguration(cfg => {
        //        cfg.CreateMap<Food, FoodDto>();
        //        cfg.CreateMap<FoodDto, Food>();

        //        cfg.CreateMap<Extra, ExtraDto>();
        //        cfg.CreateMap<ExtraDto, Extra>();

        //        //cfg.CreateMap<Special, SpecialDto>();
        //        //cfg.CreateMap<SpecialDto, Special>();
        //    });

        //    _mapper = config.CreateMapper();
        //}

        public FoodController() {
            _unitOfWork = new UnitOfWork();

            InitializeMapper();
        }

        private void InitializeMapper() {
            //automapper initaizaliton
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Food, FoodDto>();
                cfg.CreateMap<FoodDto, Food>();

                cfg.CreateMap<Extra, ExtraDto>();
                cfg.CreateMap<ExtraDto, Extra>();

                //cfg.CreateMap<Special, SpecialDto>();
                //cfg.CreateMap<SpecialDto, Special>();
            });

            _mapper = config.CreateMapper();
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            //return new string[] { "value1", "value2" };
            var foods = _unitOfWork.Foods.GetAllWithExtras();

            //var foodDtos = foods.Select(Mapper.Map<Food, FoodDto>);
            //var foodDtos = _mapper.Map<FoodDto>(foods);

            //_mapper.Map<FoodDto>(foods.Select());

            var foodDtos = foods.Select(_mapper.Map<FoodDto>);

            return Ok(foodDtos);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var food = _unitOfWork.Foods.GetWithExtra(id);

            if (food == null) {
                return NotFound();
            }

            var foodDto = _mapper.Map<FoodDto>(food);

            return Ok(foodDto);
        }

        //public IHttpActionResult Create(FoodDto foodDto)
        //{
        //    _unitOfWork.Foods.Add(_mapper.Map<Food>(foodDto));

        //    return Ok();
        //}

        // POST api/<controller>
        public IHttpActionResult Post(FoodDto foodDto)
        {
            var food = _mapper.Map<Food>(foodDto);

            _unitOfWork.Foods.Add(food);
            _unitOfWork.Complete();

            return Created(new Uri(Request.RequestUri + "/" + food.Id), foodDto);
        }

        // PUT api/<controller>/5
        public void Put(int id, FoodDto foodDto)
        {
            var foodInDb = _unitOfWork.Foods.SingleOrDefault(c => c.Id == id);

            _mapper.Map<Food>(foodDto);

            _unitOfWork.Complete();
        }

        // DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}