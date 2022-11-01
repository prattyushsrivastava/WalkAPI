using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Walks.API.Models.Domain;
using Walks.API.Repositories;

namespace Walks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()

        {
            var regions = await regionRepository.GettAllAsync();
            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id=Guid.NewGuid(),
            //        Name="India",
            //        Code="WSS",
            //        Area=122343,
            //        Lat=-1.822,
            //        Long=222.33,
            //        Population=8118818


            //    }

            //};

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions);


            return Ok(regions);
        }
   

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region= await regionRepository.GetAsync(id);

            if (region == null)
                return NotFound();

            var regionDTO= mapper.Map<Models.DTO.Region>(region);

            return Ok(regionDTO);

        }


        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddregionRequest addregion)
        {
            //Request To Domain Model

            var region = new Models.Domain.Region()
            {
                Name = addregion.Name,
                Code = addregion.Code,
                Area = addregion.Area,
                Lat = addregion.Lat,
                Long = addregion.Long,
                Population = addregion.Population
            };

            //Pass Details to repo

            region = await regionRepository.AddAsync(region);

            //convert back to DTO

            var regionDTO = new Models.DTO.Region
            {
                Id=region.Id,
                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new {id=regionDTO.Id},regionDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get Region from database

            var region = await regionRepository.DeleteAsync(id);

            //if null invalid

            if(region == null)
            {
                return NotFound();
            }
            //convert response back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Name = region.Name, 
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            //Return OK response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id, [FromBody]Models.DTO.UpdateRegionRequest updateregreq)
        {
            //Convert DTO to domain model

            var region = new Models.Domain.Region()
            {
                Name = updateregreq.Name,
                Code = updateregreq.Code,
                Area = updateregreq.Area,
                Lat = updateregreq.Lat,
                Long = updateregreq.Long,
                Population = updateregreq.Population
            };

            //Update region using repo
            region = await regionRepository.UpdateAsync(id, region);
            // if null not found
            if (region == null)
                return NotFound();
            //convert domain to DTO
            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population
            };
            //Return OK response
            return Ok(regionDTO);



        }
    }
}
