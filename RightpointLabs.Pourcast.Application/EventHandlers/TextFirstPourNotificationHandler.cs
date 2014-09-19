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
    using Twilio;

    public class TextFirstPourNotificationHandler : IEventHandler<PourStarted>
    {
        private readonly ITextMessageService _textMessageService;
        private readonly IEmailService _emailService;
        private readonly ITapRepository _tapRepository;
        private readonly IKegRepository _kegRepository;
        private readonly IBeerRepository _beerRepository;


        public TextFirstPourNotificationHandler(ITapRepository tapRepository, 
            IKegRepository kegRepository, 
            ITextMessageService textMessageService, 
            IEmailService emailService, 
            IBeerRepository beerRepository)
        {
            if (tapRepository == null) throw new ArgumentNullException("tapRepository");
            if (kegRepository == null) throw new ArgumentNullException("kegRepository");
            if (textMessageService == null) throw new ArgumentNullException("textMessageService");
            if (emailService == null) throw new ArgumentNullException("emailService");
            if (beerRepository == null) throw new ArgumentNullException("beerRepository");

            _tapRepository = tapRepository;
            _kegRepository = kegRepository;
            _textMessageService = textMessageService;
            _emailService = emailService;
            _beerRepository = beerRepository;
        }

        public void Handle(PourStarted domainEvent)
        {
            _textMessageService.SendTextMessage("3129613734", "From Pourcast");
        }
    }
}
