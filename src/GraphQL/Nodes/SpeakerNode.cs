using ConferencePlanner.Application.Sessions.Queries.GetSessionsBySpeakerId;
using ConferencePlanner.Application.Speakers.Queries.GetSpeakerById;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.GraphQL.Nodes
{
    [Node]
    [ExtendObjectType(typeof(Speaker))]
    public class SpeakerNode
    {
        [BindMember(nameof(Speaker.SessionSpeakers), Replace = true)]
        public async Task<IEnumerable<Session>> GetSessionsAsync(
            [Parent] Speaker speaker,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(new GetSessionsBySpeakerIdQuery(speaker.Id), cancellationToken);

        [NodeResolver]
        public static Task<Speaker> GetSpeakerByIdAsync(
            GetSpeakerByIdQuery input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => mediator.Send(input, cancellationToken);
    }
}