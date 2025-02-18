using System;
using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class GetActivityList
{ 
    public class Query : IRequest<List<Activity>>;

    public class Handler(AppDbContext context, ILogger<GetActivityList> logger) : IRequestHandler<Query, List<Activity>>
    {
        public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await Task.Delay(1000, cancellationToken);
                    logger.LogInformation($"Task {i} has completed");
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Task was cancelled");
            }
            return await context.Activities.ToListAsync(cancellationToken);
        }
    }
}
