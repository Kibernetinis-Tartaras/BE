using BeMo.Models;
using BeMo.Models.DTOs.Requests;
using BeMo.Models.DTOs.Responses;
using BeMo.Repositories;
using BeMo.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeMo.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ChallengeController : ControllerBase
    {
        private readonly IRepository<Challenge> _repository;
        
        public ChallengeController(IRepository<Challenge> repository) 
        {
            _repository = repository;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Create")]
        public async Task<ActionResult<ChallengePostResponse>> Create(ChallengePostRequest challengePostRequest)
        {
            bool challengeExists;
            try
            {
               challengeExists = await _repository.ExistsByPropertyAsync(x => x.Id == challengePostRequest.id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (challengeExists is true) return BadRequest($"Challenge with the Id={challengePostRequest.id} is already added");

            Challenge challenge = new Challenge
            {
                Id = challengePostRequest.id,
                Name = challengePostRequest.name,
                Type = (ActivityType)challengePostRequest.type,
                IsPublic = challengePostRequest.isPublic,
                StartDate = challengePostRequest.startDate,
                EndDate = challengePostRequest.endDate,
            };

            try
            {
                await _repository.InsertAsync(challenge);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            ChallengePostResponse challengeResponse = new ChallengePostResponse
            {
                success = true,
            };

            return Ok(challengeResponse);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("GetById")]
        public async Task<ActionResult<ChallengeResponse>> GetById([FromQuery] ObjectGetByIdRequest ObjectGetByIdRequest)
        {
            ChallengeResponse? challenge;
            try
            {
                challenge = await _repository.GetByPropertyAsync(x => x.Id == ObjectGetByIdRequest.Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (challenge is null) return NotFound($"Challenge with the Id={ObjectGetByIdRequest.Id} was not found!");

            return Ok(challenge);
        }
    }
}
