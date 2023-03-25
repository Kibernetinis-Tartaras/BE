﻿using BeMo.Models.DTOs.Requests;
using BeMo.Models.DTOs.Responses;
using BeMo.Models;
using Microsoft.AspNetCore.Mvc;
using BeMo.Repositories.Interfaces;

namespace BeMo.Controllers
{
    [ApiController]
    [Route("/api/challengeEnrollment")]
    public class UserChallengeEnrollmentController : ControllerBase
    {
        private readonly IRepository<Challenge> _challengeRepository;
        private readonly IUserChallengeRepository _userChallengeRepository;

        public UserChallengeEnrollmentController(
            IUserChallengeRepository userChallengeRepository,
            IRepository<Challenge> challengeRepository)
        {
            _userChallengeRepository = userChallengeRepository;
            _challengeRepository = challengeRepository;
        }
         
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("userChallenges")]
        public async Task<ActionResult<IEnumerable<UserChallengesResponse>>> GetAllUserChallenges(ObjectGetByIdRequest ObjectGetByIdRequest)
        {
            IEnumerable<User_Challenge>? userChallengeRelationships;
            IList<Challenge> challenges = new List<Challenge>();
            IList<UserChallengesResponse> challengesResponses = new List<UserChallengesResponse>();

            try
            {
                userChallengeRelationships = await _userChallengeRepository.GetAllByPropertyAsync(x => x.UserId == ObjectGetByIdRequest.Id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (userChallengeRelationships.Count() is 0) return NotFound($"User with the id: {ObjectGetByIdRequest.Id} does not have any challenges!");

            try
            {
                foreach (var relationship in userChallengeRelationships)
                {
                    var challenge = await _challengeRepository.GetByPropertyAsync(x => x.Id == relationship.ChallengeId);

                    // if challenge is null something is really fucked here
                    if (challenge == null) return StatusCode(StatusCodes.Status500InternalServerError);

                    UserChallengesResponse response = new UserChallengesResponse
                    {
                        Id = challenge.Id,
                        Name = challenge.Name,
                        Type = challenge.Type,
                        IsPublic = challenge.IsPublic,
                        StartDate = challenge.StartDate,
                        EndDate = challenge.EndDate,
                    };
                    challengesResponses.Add(response);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(challengesResponses);
        }
    }
}
