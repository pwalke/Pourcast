namespace RightpointLabs.Pourcast.Application.EventHandlers
{
    using RightpointLabs.Pourcast.Domain.Events;
    using RightpointLabs.Pourcast.Domain.Repositories;
    using RightpointLabs.Pourcast.Domain.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TextFirstPourNotificationHandler : IEventHandler<PourStarted>
    {
        public TextFirstPourNotificationHandler(ITapRepository tapRepository, IKegRepository kegRepository, IEmailService emailService, IBeerRepository beerRepository)
        {
        }

        public void Handle(PourStarted domainEvent)
        {

        }

    }
}
