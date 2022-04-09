using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly List<IEquipment> equipmentList;

        public EquipmentRepository()
        {
            this.equipmentList = new List<IEquipment>();
        }

        public IReadOnlyCollection<IEquipment> Models => equipmentList.AsReadOnly();

        public void Add(IEquipment model)
        {
            equipmentList.Add(model);
        }

        public IEquipment FindByType(string type)
        {
            // TODO :
            return equipmentList.FirstOrDefault(x => x.GetType().Name == type);
        }

        public bool Remove(IEquipment model)
        {
            return equipmentList.Remove(model);
        }
    }
}
