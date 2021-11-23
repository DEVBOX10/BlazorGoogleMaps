﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OneOf;

namespace GoogleMapsComponents.Maps
{
    public class ListableEntityBase<TEntityOptions>
        where TEntityOptions : ListableEntityOptionsBase
    {
        internal readonly JSObjRefWrapper _jsObjectRef;

        public readonly Dictionary<string, List<MapEventListener>> EventListeners;

        //public Guid Guid => _jsObjectRef.Guid;
        
        internal ListableEntityBase(JSObjRefWrapper jsObjectRef)
        {
            _jsObjectRef = jsObjectRef;
            EventListeners = new Dictionary<string, List<MapEventListener>>();
        }

        public ValueTask DisposeAsync()
        {
            //foreach (string key in EventListeners.Keys)
            //{
            //    //Probably superfluous...
            //    if ((EventListeners.TryGetValue(key, out var eventsList) && eventsList != null))
            //    {
            //        //foreach (MapEventListener eventListener in eventsList)
            //        //{
            //        //    await eventListener.DisposeAsync();
            //        //}

            //        eventsList.Clear();
            //    }
            //}

            //EventListeners.Clear();
            //return _jsObjectRef.DisposeAsync();

            throw new NotImplementedException();
        }

        public virtual ValueTask<Map> GetMap()
        {
            //return _jsObjectRef.InvokeAsync<Map>(
            //    "getMap");

            throw new NotImplementedException();
        }

        /// <summary>
        /// Renders the mao entity on the specified map or panorama. 
        /// If map is set to null, the map entity will be removed.
        /// </summary>
        /// <param name="map"></param>
        public virtual async Task SetMap(Map map)
        {
            //await _jsObjectRef.InvokeAsync("setMap", map);

            ////_map = map;

            throw new NotImplementedException();
        }

        public virtual async Task<MapEventListener> AddListener(string eventName, Action handler)
        {
            //JsObjectRef listenerRef = await _jsObjectRef.InvokeWithReturnedObjectRefAsync("addListener", eventName, handler);
            //MapEventListener eventListener = new MapEventListener(listenerRef);

            //if (!EventListeners.ContainsKey(eventName))
            //{
            //    EventListeners.Add(eventName, new List<MapEventListener>());
            //}
            //EventListeners[eventName].Add(eventListener);

            //return eventListener;

            throw new NotImplementedException();
        }

        public virtual async Task<MapEventListener> AddListener<V>(string eventName, Action<V> handler)
        {
            //JsObjectRef listenerRef = await _jsObjectRef.InvokeWithReturnedObjectRefAsync("addListener", eventName, handler);
            //MapEventListener eventListener = new MapEventListener(listenerRef);

            //if (!EventListeners.ContainsKey(eventName))
            //{
            //    EventListeners.Add(eventName, new List<MapEventListener>());
            //}
            //EventListeners[eventName].Add(eventListener);

            //return eventListener;

            throw new NotImplementedException();
        }

        public virtual async Task ClearListeners(string eventName)
        {
            //if (EventListeners.ContainsKey(eventName))
            //{
            //    await _jsObjectRef.InvokeAsync("clearListeners", eventName);

            //    //IMHO is better preserving the knowledge that Marker had some EventListeners attached to "eventName" in the past
            //    //so, instead to clear the list and remove the key from dictionary, I prefer to leave the key with an empty list
            //    EventListeners[eventName].Clear();
            //}

            throw new NotImplementedException();
        }

        public ValueTask InvokeAsync(string functionName, params object[] args)
        {
            //return _jsObjectRef.InvokeAsync(functionName, args);

            throw new NotImplementedException();
        }
    }
}
