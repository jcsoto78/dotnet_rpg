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

        public async Task<bool> AddCharacter(Character character)
        {
            throw new NotImplementedException(); //TODO
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            return MyCharacters.ToList();
        }

        public async Task<Character> GetCharacterById(int id)
        {
           return MyCharacters.FirstOrDefault(c => c.Id == id); //returns null if not found
        }
    }
}
