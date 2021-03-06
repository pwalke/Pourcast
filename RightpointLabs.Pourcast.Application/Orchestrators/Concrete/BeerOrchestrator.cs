﻿using System.Net.NetworkInformation;

namespace RightpointLabs.Pourcast.Application.Orchestrators.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RightpointLabs.Pourcast.Application.Orchestrators.Abstract;
    using RightpointLabs.Pourcast.Application.Payloads;
    using RightpointLabs.Pourcast.Application.Transactions;
    using RightpointLabs.Pourcast.Domain.Models;
    using RightpointLabs.Pourcast.Domain.Repositories;

    public class BeerOrchestrator : BaseOrchestrator, IBeerOrchestrator
    {
        private readonly IBeerRepository _beerRepository;
        private readonly IBreweryRepository _breweryRepository;

        private readonly ITapRepository _tapRepository;

        private readonly IKegRepository _kegRepository;
        private readonly IStyleRepository _styleRepository;

        public BeerOrchestrator(IBeerRepository beerRepository, IBreweryRepository breweryRepository, ITapRepository tapRepository, IKegRepository kegRepository, IStyleRepository styleRepository)
        {
            if (beerRepository == null) throw new ArgumentNullException("beerRepository");
            if(breweryRepository == null) throw new ArgumentNullException("breweryRepository");
            if (tapRepository == null) throw new ArgumentNullException("tapRepository");
            if (kegRepository == null) throw new ArgumentNullException("kegRepository");
            if(null == styleRepository) throw new ArgumentException("styleRepository");

            _beerRepository = beerRepository;
            _breweryRepository = breweryRepository;
            _tapRepository = tapRepository;
            _kegRepository = kegRepository;
            _styleRepository = styleRepository;
        }

        public IEnumerable<Beer> GetBeers()
        {
            return _beerRepository.GetAll();
        }

        public IEnumerable<BeerOnTap> GetBeersOnTap()
        {
            var taps = _tapRepository.GetAll();

            return taps.Select(tap => CreateBeerOnTap(tap));
        }

        public BeerOnTap GetBeerOnTap(string tapId)
        {
            var tap = _tapRepository.GetById(tapId);

            return CreateBeerOnTap(tap);
        }

        private BeerOnTap CreateBeerOnTap(Tap tap)
        {
            if (tap.HasKeg)
            {
                var keg = _kegRepository.GetById(tap.KegId);
                var beer = _beerRepository.GetById(keg.BeerId);
                var brewery = _breweryRepository.GetById(beer.BreweryId);
                var style = (string.IsNullOrEmpty(beer.StyleId)) ? null : _styleRepository.GetById(beer.StyleId);
                //TODO Maybe add a default color
                beer.Color = (null == style) ? string.Empty : style.Color;
                beer.Style = (null == style) ? string.Empty : style.Name;
                return new BeerOnTap() { Tap = tap, Keg = keg, Beer = beer, Brewery = brewery, Style = style };
            }
            else
            {
                return new BeerOnTap() { Tap = tap };
            }
        }

        public IEnumerable<Beer> GetByName(string name)
        {
            return _beerRepository.GetAllByName(name);
        }

        public IEnumerable<Beer> GetByBrewery(string breweryId)
        {
            return _beerRepository.GetByBreweryId(breweryId);
        }

        public Beer GetById(string id)
        {
            return _beerRepository.GetById(id);
        }

        [Transactional]
        public string CreateBeer(string name, double abv, double baScore, string styleId, string breweryId)
        {
            var id = string.Empty;

            id = _beerRepository.NextIdentity();
            var beer = new Beer(id, name)
            {
                ABV = abv,
                BAScore = baScore,
                BreweryId = breweryId,
                RPScore = 0,
                StyleId = styleId
            };
            _beerRepository.Add(beer);

            return id;
        }

        public IEnumerable<Beer> GetBeersByName(string name)
        {
            return _beerRepository.GetAllByName(name);
        }

        [Transactional]
        public void Save(Beer beer)
        {
            _beerRepository.Update(beer);
        }
    }
}