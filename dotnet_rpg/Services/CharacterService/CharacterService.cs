using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor; //injected service to get user claims data

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // User id from Token claims is frequently queried, so lets have a private method for it
        private int GetSecurityUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = _mapper.Map<Character>(newCharacter);
                character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetSecurityUserId());

                await _context.Characters.AddAsync(character);

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(newCharacter);
                serviceResponse.Message = "New Character Created";
            }
            catch (Exception ex)
            {

                serviceResponse.Message = $"Error Adding new Character : {ex}";
                serviceResponse.Success = false;
            }

            return serviceResponse;
            
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetSecurityUserId());

                if (character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = (_context.Characters.Where(c => c.User.Id == GetSecurityUserId())
                        .Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not Found";
                }
          
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = $"Cannot delete character: {ex}";
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            //serviceResponse.Data = (List<GetCharacterDto>)MyCharacters; // DO NOT DO use automapper instead
            var dbCharacters = await _context.Characters.Where(c => c.User.Id == GetSecurityUserId()).ToListAsync(); 
            serviceResponse.Data = (dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            //serviceResponse.Data = ((List<GetCharacterDto>)MyCharacters).FirstOrDefault(c => c.Id == id); //DONT DO use automapper instead

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetSecurityUserId()));

            return serviceResponse;
        }

        //TODO
        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            //EF Include() is needed to include related entities on the query results, by default EF includes null instead
            var characterToUpdate = await _context.Characters.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            
            var response = new ServiceResponse<GetCharacterDto>();

            if (characterToUpdate != null && characterToUpdate.User.Id ==  GetSecurityUserId())
            {
                _mapper.Map(updatedCharacter, characterToUpdate); //uses the same instances for mapping, needed to avoid EF entity tracker issues

                _context.Characters.Update(characterToUpdate);

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(characterToUpdate);
            }
            else {
                //response.Data = _mapper.Map<GetCharacterDto>(updatedCharacter);
                response.Success = false;
                response.Message = "Cannot update Character";
            }

            return response;
        }
    }
}
