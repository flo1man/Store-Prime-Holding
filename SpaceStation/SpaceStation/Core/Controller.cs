using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronautRepository;
        private PlanetRepository planetRepository;

        public Controller()
        {
            this.astronautRepository = new AstronautRepository();
            this.planetRepository = new PlanetRepository();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut = null;

            if (type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == "Meteorologist")
            {
                astronaut = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException("Astronaut type doesn't exists!");
            }

            astronautRepository.Add(astronaut);

            return $"Successfully added {type}: {astronautName}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            Planet planet = new Planet(planetName);

            foreach (var item in items.Where(x => !string.IsNullOrEmpty(x) || !string.IsNullOrWhiteSpace(x)))
            {
                planet.Items.Add(item);
            }

            planetRepository.Add(planet);

            return $"Successfully added Planet: {planetName}!";
        }

        public string ExplorePlanet(string planetName)
        {
            var planet = planetRepository.FindByName(planetName);

            var astronauts = astronautRepository.Models.Where(x => x.Oxygen > 60).ToList();

            if (!astronauts.Any())
            {
                throw new InvalidOperationException("You need at least one astronaut to explore the planet");
            }

            var mission = new Mission();

            mission.Explore(planet, astronauts);

            return $"Planet: {planetName} was explored! Exploration finished with {astronauts.Where(x => x.Oxygen == 0).Count()} dead astronauts!";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{planetRepository.Models.Where(x => x.Items.Count == 0).Count()} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach (var astronaut in astronautRepository.Models)
            {
                var items = astronaut.Bag.Items.Any() ? string.Join(", ", astronaut.Bag.Items) : "none";

                sb.AppendLine($"Name: {astronaut.Name}")
                   .AppendLine($"Oxygen: {astronaut.Oxygen}")
                   .AppendLine($"Bag items: {items}");
            }

            return sb.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            var astronaut = astronautRepository.FindByName(astronautName);

            if (astronaut == null)
            {
                throw new InvalidOperationException($"Astronaut {astronautName} doesn't exists!");
            }

            astronautRepository.Remove(astronaut);

            return $"Astronaut {astronautName} was retired!";
        }
    }
}
