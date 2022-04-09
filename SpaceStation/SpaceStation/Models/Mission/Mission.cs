using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            List<string> items = new List<string>();

            foreach (var astronaut in astronauts)
            {
                if (planet.Items.Count == 0)
                {
                    break;
                }

                if (astronaut.CanBreath)
                {
                    foreach (var item in planet.Items)
                    {
                        if (!astronaut.CanBreath)
                        {
                            break;
                        }
                        astronaut.Breath();
                        astronaut.Bag.Items.Add(item);

                        // TODO : POTENTIAL EXCEPTION
                        items.Add(item);
                    }

                    foreach (var item in items)
                    {
                        planet.Items.Remove(item);
                    }
                    items.Clear();
                }
            }
        }
    }
}
