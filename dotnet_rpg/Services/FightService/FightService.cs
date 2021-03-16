using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.FightService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;

        public FightService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto>
            {
                Data = new FightResultDto()
            };

            try
            {
                var characters =
                    await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
                    .Where(c => request.CharacterIds.Contains(c.Id)).ToListAsync();

                //fighting

                bool defeated = false;
                while (!defeated)
                {
                    foreach (Character attacker in characters)
                    {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed = string.Empty;
                        bool useWeapon = new Random().Next(2) == 0;
                        
                        if (useWeapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = DoWeaponAttack(attacker, opponent);
                        }
                        else
                        {
                            int randomSkill = new Random().Next(attacker.CharacterSkills.Count);

                            attackUsed = attacker.CharacterSkills[randomSkill].Skill.Name;

                            damage = DoSkillAttack(attacker, opponent, attacker.CharacterSkills[randomSkill]);
                        }

                        response.Data.Log.Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with {(damage >= 0 ? damage : 0)} damage.");

                        if (opponent.HitPoints <= 0)
                        {
                            defeated = true;
                            attacker.Victories++;
                            opponent.Defeats++;
                            response.Data.Log.Add($"{opponent.Name} has been defeated");
                            response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left!");
                            break;
                        }
                    }
                }

                //add figts counter and reset hp
                characters.ForEach(c =>
                {
                    c.Fights++;
                    c.HitPoints = 100;
                });

                _context.Characters.UpdateRange(characters);
                await _context.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex}";
            }

            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                //get the attacker skill
                var characterSkill = attacker.CharacterSkills.FirstOrDefault(cs => cs.Skill.Id == request.SkillId);

                if (characterSkill == null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesnt have such skill";

                    return response;
                }

                int damage = DoSkillAttack(attacker, opponent, characterSkill);

                if (opponent.HitPoints <= 0)
                {
                    response.Message = $"{opponent.Name} has died";
                }

                _context.Characters.Update(opponent);
                await _context.SaveChangesAsync();

                //TODO use automapper instead
                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex}";
            }

            return response;
        }

        private static int DoSkillAttack(Character attacker, Character opponent, CharacterSkill characterSkill)
        {
            //calculate some damage
            int damage = characterSkill.Skill.Damage + (new Random().Next(attacker.Magic));
            damage -= new Random().Next(opponent.Dexterity);

            if (damage > 0)
            {
                opponent.HitPoints -= damage;
            }

            return damage;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);
                int damage = DoWeaponAttack(attacker, opponent);

                if (opponent.HitPoints <= 0)
                {
                    response.Message = $"{opponent.Name} has died";
                }

                _context.Characters.Update(opponent);
                await _context.SaveChangesAsync();

                //TODO use automapper instead
                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    AttackerHP = attacker.HitPoints,
                    Opponent = opponent.Name,
                    OpponentHP = opponent.HitPoints,
                    Damage = damage
                };

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"{ex}";
            }

            return response;
        }

        private static int DoWeaponAttack(Character attacker, Character opponent)
        {
            //calculate some damage
            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Might));
            damage -= new Random().Next(opponent.Dexterity);

            if (damage > 0)
            {
                opponent.HitPoints -= damage;
            }

            return damage;
        }
    }
}
