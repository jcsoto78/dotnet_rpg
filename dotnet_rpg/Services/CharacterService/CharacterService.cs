using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                await _context.Characters.AddAsync(_mapper.Map<Character>(newCharacter));

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
                _context.Characters.Remove(await _context.Characters.FirstOrDefaultAsync(c => c.Id == id));

                await _context.SaveChangesAsync();

                serviceResponse.Data = (_context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            }
            catch (Exception ex)
            {

                serviceResponse.Success = false;
                serviceResponse.Message = $"Cannot delete character: {ex}";
            }

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int securityUserId)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            //serviceResponse.Data = (List<GetCharacterDto>)MyCharacters; // TODO use automapper instead
            var dbCharacters = await _context.Characters.Where(c => c.User.Id == securityUserId).ToListAsync(); 
            serviceResponse.Data = (dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            //serviceResponse.Data = ((List<GetCharacterDto>)MyCharacters).FirstOrDefault(c => c.Id == id); //DONT DO use automapper instead

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(await _context.Characters.FirstOrDefaultAsync(c => c.Id == id));

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var characterToUpdate = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            
            var response = new ServiceResponse<GetCharacterDto>();

            if (characterToUpdate != null)
            {
                _mapper.Map(updatedCharacter, characterToUpdate); // uses the same instances for mapping, needed to avoid entity tracker issues

                _context.Characters.Update(characterToUpdate);

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(characterToUpdate);
            }
            else {
                response.Data = _mapper.Map<GetCharacterDto>(updatedCharacter);
                response.Success = false;
                response.Message = "Cannot update Character";
            }

            return response;
        }
    }
}
