using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Domain.Entities;

namespace SampleApp.Application.Heroes.Queries
{
    public class FindHeroQuery : IRequest<Hero>
    {
        #region Public members
        public string Name { get; set; }
        #endregion

        #region Handler
        public class FindHeroQueryHandler : IRequestHandler<FindHeroQuery, Hero>
        {
            private readonly IMediator mediator;
            private readonly ISampleAppDbContext dbContext;

            public FindHeroQueryHandler(IMediator mediator, ISampleAppDbContext dbContext)
            {
                this.mediator = mediator;
                this.dbContext = dbContext;
            }

            public async Task<Hero> Handle(FindHeroQuery request, CancellationToken cancellationToken)
            {
                return await dbContext.Heros.SingleOrDefaultAsync(a => a.Name == request.Name);
            }
        }
        #endregion
    }
}