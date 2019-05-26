using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Resources
{
    public enum ResourceType
    {
        Gold,
        Wood,
        Steel
    }

    [Serializable]
    public struct ResourceAmount
    {
        public ResourceType Type;
        public int Amount;
    }

    public class ResourceManager : MonoBehaviour
    {
        [Serializable]
        public struct StartingResourceType
        {
            public ResourceType Type;
            public int Amount;
        }

        public StartingResourceType[] StartingResourceAmount;

        private Dictionary<ResourceType, List<Action<int>>> _resourceBinding = new Dictionary<ResourceType, List<Action<int>>>();
        private Dictionary<ResourceType, int> _resourceDictionary;

        private void Awake()
        {
            _resourceDictionary = new Dictionary<ResourceType, int>();
            if (StartingResourceAmount != null)
            {
                foreach (StartingResourceType startingResource in StartingResourceAmount)
                {
                    _resourceDictionary.Add(startingResource.Type, startingResource.Amount);
                }
            }
        }

        public void AddResource(ResourceType type, int amount)
        {
            if (_resourceDictionary.ContainsKey(type))
                _resourceDictionary[type] += amount;
            else
                _resourceDictionary[type] = amount;


            CallResourceBinding(type);
        }

        public bool HasEnoughResourceArray(ResourceAmount[] costArray)
        {
            if (costArray == null) return true;
            foreach (ResourceAmount cost in costArray)
            {
                if (!HasEnoughResource(cost.Type, cost.Amount))
                    return false;
            }

            return true;
        }

        public bool HasEnoughResource(ResourceType type, int amount)
        {
            if (!_resourceDictionary.ContainsKey(type))
                return false;

            if (_resourceDictionary[type] >= amount)
            {
                return true;
            }
            return false;
        }

        public bool TryPayResource(ResourceType type, int amount)
        {
            if (HasEnoughResource(type, amount))
            {
                _resourceDictionary[type] -= amount;
                CallResourceBinding(type);
                return true;
            }
            return false;
        }

        public Action BindResource(ResourceType type, Action<int> callback)
        {
            if (!_resourceBinding.ContainsKey(type))
                _resourceBinding[type] = new List<Action<int>>();

            _resourceBinding[type].Add(callback);
            if (_resourceDictionary.ContainsKey(type))
            {
                int resourceAmount = _resourceDictionary[type];
                callback(resourceAmount);
            }

            return () =>
            {
                _resourceBinding[type].Remove(callback);
            };
        }

        private void CallResourceBinding(ResourceType type)
        {
            if (_resourceBinding.ContainsKey(type) && _resourceDictionary.ContainsKey(type))
            {
                int resourceAmount = _resourceDictionary[type];
                List<Action<int>> resourceBindingList = _resourceBinding[type];
                foreach (Action<int> resourceBinding in resourceBindingList)
                {
                    resourceBinding?.Invoke(resourceAmount);
                }
            }
        }
    }
}
