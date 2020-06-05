using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Entities;
using SampleApp.Exceptions;

namespace SampleApp.Application.HeroSkills.Commands
{
    public class CastUltimateSkillCommand : IRequest<HeroSkill>
    {
        #region Public members
        public string Hero { get; set; }
        #endregion

        #region Handler
        public class CastUltimateSkillCommandHandler : IRequestHandler<CastUltimateSkillCommand, HeroSkill>
        {
            private readonly IMediator mediator;
            private readonly ISampleAppDbContext dbContext;

            public CastUltimateSkillCommandHandler(IMediator mediator, ISampleAppDbContext dbContext)
            {
                this.mediator = mediator;
                this.dbContext = dbContext;
            }

            public async Task<HeroSkill> Handle(CastUltimateSkillCommand request, CancellationToken cancellationToken)
            {
                var _hero = await dbContext.Heros
                    .Include(a => a.Skills)
                    .ThenInclude(a => a.Skill)
                    .SingleOrDefaultAsync(a => a.Name == request.Hero);

                if (_hero == null)
                {
                    throw new Exception($"Hero not found with the name ({request.Hero})");
                }

                var _ultimate = _hero.Skills.SingleOrDefault(a => a.Skill.IsUltimate);

                if (_ultimate == null)
                {
                    throw new Exception("Ultimate skill not configured");
                }

                if (_hero.Mana < _ultimate.Skill.ManaCost)
                {
                    throw new InsufficientManaException();
                }

                _hero.Mana -= _ultimate.Skill.ManaCost;

                return _ultimate;
            }
        }
        #endregion

        #region Validator
        public class CastSkillCommandValidator : AbstractValidator<CastUltimateSkillCommand>
        {
            public CastSkillCommandValidator()
            {
                // RuleFor(a => a.Prop).NotNull().MaximumLength(100);
            }
        }
        #endregion
    }
}