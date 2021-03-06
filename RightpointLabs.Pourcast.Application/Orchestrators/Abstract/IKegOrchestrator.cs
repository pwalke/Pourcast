﻿namespace RightpointLabs.Pourcast.Application.Orchestrators.Abstract
{
    using System.Collections.Generic;

    using RightpointLabs.Pourcast.Domain.Models;

    public interface IKegOrchestrator
    {
        IEnumerable<Keg> GetKegs();

        IEnumerable<Keg> GetKegs(bool isEmpty);
            
        Keg GetKeg(string kegId);
        
        IEnumerable<Keg> GetKegsOnTap();
            
        Keg GetKegOnTap(string tapId);

        string CreateKeg(string beerId, double capacity);
    }
}