using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using SampleApp.Domain.Entities;

namespace SampleApp.Application.HeroSkills.Commands
{
    public class CastSkillCommand : IRequest<Skill>
    {
        #region Public members
        public Skill Skill { get; set; }
        #endregion

        #region Handler
        public class CastSkillCommandHandler : IRequestHandler<CastSkillCommand, Skill>
        {
            private readonly IMediator mediator;

            public CastSkillCommandHandler(IMediator mediator)
            {
                this.mediator = mediator;
            }

            public async Task<Skill> Handle(CastSkillCommand request, CancellationToken cancellationToken)
            {
                // do some magic here - execute skill

                return request.Skill;
            }
        }
        #endregion

        #region Validator
        public class CastSkillCommandValidator : AbstractValidator<CastSkillCommand>
        {
            public CastSkillCommandValidator()
            {
                // RuleFor(a => a.Prop).NotNull().MaximumLength(100);
            }
        }
        #endregion
    }
}