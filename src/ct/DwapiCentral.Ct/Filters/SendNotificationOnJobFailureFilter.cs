using DwapiCentral.Ct.Application.EventHandlers;
using DwapiCentral.Ct.Application.Events;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;
using MediatR;

namespace DwapiCentral.Ct.Filters
{
    public class SendNotificationOnJobFailureFilter : JobFilterAttribute, IApplyStateFilter
    {
        private readonly IMediator _mediator;

        public SendNotificationOnJobFailureFilter(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            if (context.NewState is FailedState)
            {
                // A job has failed; send a notification
                var jobId = context.BackgroundJob.Id;
                var jobName = context.BackgroundJob.Job.Type.FullName;

                var notificationService = new HangfireJobFailNotificationEvent { Message = $"Job {jobName} (ID: {jobId}) has failed." };

                _mediator.Publish(notificationService);
            }
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {


        }
    }
}
