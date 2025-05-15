using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.EventBus
{
    public class EventBus : IDisposable
    {
        private Dictionary<Type, List<Delegate>> _events;
        
        private Dictionary<Type, int> _thisFrameEvents;
        
        public EventBus()
        {
            _events = new Dictionary<Type, List<Delegate>>();
            
            _thisFrameEvents = new Dictionary<Type, int>();
        }
        
        public void Subscribe<T>(Action<T> action)
        {
            if (!_events.TryGetValue(typeof(T), out List<Delegate> actions))
            {
                _events[typeof(T)] = new List<Delegate>();
            }
            
            _events[typeof(T)].Add(action);
        }
        
        public void Unsubscribe<T>(Action<T> action)
        {
            if (_events.TryGetValue(typeof(T), out List<Delegate> actions))
            {
                actions.Remove(action);

                if (actions.Count == 0)
                {
                    _events.Remove(typeof(T));
                }
            }
        }
        
        public void Publish<T>(T eventData)
        {
            _thisFrameEvents[typeof(T)] = Time.frameCount;
            
            if (_events.TryGetValue(typeof(T), out List<Delegate> actions))
            {
                foreach (var action  in actions)
                {
                    try
                    {
                        ((Action<T>)action).Invoke(eventData);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Event bust {typeof(T)} call error: {e.Message}");
                    }
                }
            }
        }

        public bool WasCalledThisFrame<T>()
        {
            if (_thisFrameEvents.TryGetValue(typeof(T), out int frameCount))
            {
                return frameCount == Time.frameCount;
            }
            
            return false;
        }

        public void Dispose()
        {
            _events.Clear();
            
            _thisFrameEvents.Clear();
        }
    }
}