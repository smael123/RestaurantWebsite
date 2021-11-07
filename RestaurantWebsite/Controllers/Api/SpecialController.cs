using AutoMapper;
using RestaurantWebsite.Core.Domain;
using RestaurantWebsite.Core.Repositories;
using RestaurantWebsite.Dtos;
using RestaurantWebsite.Persistence;
using RestaurantWebsite.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantWebsite.Controllers.Api
{
    public class SpecialController : ApiController
    {
        private IMapper _mapper;
        private ISpecialRepository specialRepository;

        public SpecialController(ISpecialRepository specialRepository)
        {
            this.specialRepository = specialRepository;

            InitializeMapper();
        }

        public SpecialController()
        {
            this.specialRepository = new SpecialRepository(new RestaurantContext());

            InitializeMapper();
        }

        private void InitializeMapper()
        {
            //automapper initaizaliton
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Food, FoodDto>();
                cfg.CreateMap<FoodDto, Food>();

                //cfg.CreateMap<Extra, ExtraDto>();
                //cfg.CreateMap<ExtraDto, Extra>();

                cfg.CreateMap<Special, SpecialDto>();
                cfg.CreateMap<SpecialDto, Special>();
            });

            _mapper = config.CreateMapper();
        }

        // GET api/<controller>
        public async Task<IHttpActionResult> Get()
        {
            var specials = await specialRepository.GetAllWithFoods();

            var specialDtos = specials.Select(_mapper.Map<SpecialDto>);

            return Ok(specialDtos);
        }

        // GET api/<controller>/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var special = await specialRepository.GetWithFood(id);

            if (special == null) {
                return NotFound();
            }

            var specialDto = _mapper.Map<SpecialDto>(special);

            return Ok(specialDto);
        }

        //public IHttpActionResult Create

        // POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}