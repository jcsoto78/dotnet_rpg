using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        //adding some mock up data
        private static readonly IList<Character> MyCharacters = new List<Character> {
            new Character(),
            new Character { Id = 1, Name = "Rolf", Class = RpgClass.Fighter },
            new Character { Id = 6, Name = "Sam", Class = RpgClass.Mage }
        };

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<bool>> AddCharacter(Character character)
        {
            //throw new NotImplementedException(); //TODO

            return new ServiceResponse<bool>() { Data = true};
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            //serviceResponse.Data = (List<GetCharacterDto>)MyCharacters; // TODO use automapper instead

            serviceResponse.Data = (MyCharacters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList(); // TODO use automapper instead

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            //serviceResponse.Data = ((List<GetCharacterDto>)MyCharacters).FirstOrDefault(c => c.Id == id); //use automapper instead

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(MyCharacters.FirstOrDefault(c => c.Id == id));

            return serviceResponse;
        }
    }
}
