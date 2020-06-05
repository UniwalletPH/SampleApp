using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Application;
using SampleApp.Application.Heroes.Queries;
using SampleApp.Application.HeroSkills.Commands;
using SampleApp.CommandTests.Base;
using SampleApp.Domain.Entities;
using SampleApp.Exceptions;
using System.Threading.Tasks;

namespace SampleApp.CommandTests
{
    [TestClass]
    public class CastUltimateSkillCommandTests
    {
        [TestMethod]
        public async Task CannotCastIfNoMana()
        {
            using (var _services = TestServiceProvider.InMemoryContext())
            {
                #region Arrange
                var _mediator = _services.GetService<IMediator>();
                var _idbContext = _services.GetService<ISampleAppDbContext>();
                var _dbContext = _services.GetService<DbContext>();

                var _silencer = new Hero
                {
                    Name = "Silencer",
                    Mana = 100,
                    Health = 200
                };

                _silencer.Skills.Add(new HeroSkill
                {
                    Skill = new Skill
                    {
                        Name = "Arcane Curse",
                        ManaCost = 125,
                        SkillType = SkillType.Active
                    }
                });

                _silencer.Skills.Add(new HeroSkill
                {
                    Skill = new Skill
                    {
                        Name = "Global Silence",
                        ManaCost = 300,
                        SkillType = SkillType.Active,
                        IsUltimate = true
                    }
                });

                _idbContext.Heros.Add(_silencer);

                await _dbContext.SaveChangesAsync();
                #endregion



                #region Act
                var _exception = await Assert.ThrowsExceptionAsync<InsufficientManaException>(async () =>
                        {
                            var _cmd = new CastUltimateSkillCommand
                            {
                                Hero = "Silencer"
                            };

                            await _mediator.Send(_cmd);
                        });
                #endregion

                #region Assert
                Assert.AreEqual("Insufficient Mana", _exception.Message); 
                #endregion
            }
        }

        [TestMethod]
        public async Task CanCastIfHasMana()
        {
            using (var _services = TestServiceProvider.InMemoryContext())
            {
                #region Arrange
                var _mediator = _services.GetService<IMediator>();
                var _idbContext = _services.GetService<ISampleAppDbContext>();
                var _dbContext = _services.GetService<DbContext>();

                var _silencer = new Hero
                {
                    Name = "Silencer",
                    Mana = 500,
                    Health = 200
                };

                _silencer.Skills.Add(new HeroSkill
                {
                    Skill = new Skill
                    {
                        Name = "Arcane Curse",
                        ManaCost = 125,
                        SkillType = SkillType.Active
                    }
                });

                _silencer.Skills.Add(new HeroSkill
                {
                    Skill = new Skill
                    {
                        Name = "Global Silence",
                        ManaCost = 300,
                        SkillType = SkillType.Active,
                        IsUltimate = true
                    }
                });

                _idbContext.Heros.Add(_silencer);

                await _dbContext.SaveChangesAsync();
                #endregion



                #region Act
                var _heroSkill = await _mediator.Send(new CastUltimateSkillCommand
                {
                    Hero = "Silencer"
                });

                var _castedSkill = await _mediator.Send(new CastSkillCommand
                {
                    Skill = _heroSkill.Skill
                });

                var _hero = await _mediator.Send(new FindHeroQuery { Name = "Silencer" });
                #endregion

                #region Assert
                Assert.AreEqual(200, _hero.Mana); 
                #endregion
            }
        }
    }
}
