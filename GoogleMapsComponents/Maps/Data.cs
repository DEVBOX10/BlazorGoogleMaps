﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using GoogleMapsComponents.Maps.Data;
using Microsoft.JSInterop;
using OneOf;

namespace GoogleMapsComponents.Maps
{
    /// <summary>
    /// A layer for displaying geospatial data. Points, line-strings and polygons can be displayed.
    /// Every Map has a Data object by default, so most of the time there is no need to construct one.
    /// The Data object is a collection of Features.
    /// </summary>
    public class MapData : MVCObject
    {
        /// <summary>
        /// Creates an empty collection, with the given DataOptions.
        /// </summary>
        /// <param name="options"></param>
        public async static Task<MapData> CreateAsync(IJSRuntime jsRuntime, DataOptions? opts = null)
        {
            var jsObjectRef = await jsRuntime.InvokeAsync<IJSObjectReference>(
                "googleMapsObjectManager.createMVCObject",
                "google.maps.Data",
                opts);

            var obj = new MapData(jsObjectRef);

            return obj;
        }

        /// <summary>
        /// Creates an empty collection, with the given DataOptions.
        /// </summary>
        internal MapData(IJSObjectReference jsObjectRef)
            : base(jsObjectRef)
        {
        }

        internal MapData(IJSObjRefWrapper jsObjectRef)
            : base(jsObjectRef)
        {
        }

        /// <summary>
        /// Adds a feature to the collection, and returns the added feature.
        /// If the feature has an ID, it will replace any existing feature in the collection with the same ID.If no feature is given, a new feature will be created with null geometry and no properties.If FeatureOptions are given, a new feature will be created with the specified properties.
        /// Note that the IDs 1234 and '1234' are equivalent. Adding a feature with ID 1234 will replace a feature with ID '1234', and vice versa.
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public ValueTask<Feature> Add(OneOf<Feature, FeatureOptions> feature)
        {
            return InvokeAsync<Feature>(
                "add",
                feature);
        }

        /// <summary>
        /// Adds GeoJSON features to the collection. Give this method a parsed JSON. 
        /// The imported features are returned. Throws an exception if the GeoJSON could not be imported.
        /// </summary>
        /// <param name="geoJson"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async ValueTask<IEnumerable<Feature>> AddGeoJson(object geoJson, GeoJsonOptions? options = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks whether the given feature is in the collection.
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public ValueTask<bool> Contains(Feature feature)
        {
            return InvokeAsync<bool>(
                "contains",
                feature);
        }

        // Repeatedly invokes the given function, passing a feature in the collection to the function on each invocation.
        // The order of iteration through the features is undefined.
        public async ValueTask ForEach(Func<Feature, Task> callback)
        {
            using var callbackObjectRef = DotNetObjectReference.Create(
                new JSInvokableAsyncAction<Feature>(callback));

            await this.InvokeVoidAsync(
                "extensionFunctions.invokeWithCallback",
                "forEach",
                callbackObjectRef);
        }

        /// <summary>
        /// Returns the position of the drawing controls on the map.
        /// </summary>
        /// <returns></returns>
        public ValueTask<ControlPosition> GetControlPosition()
        {
            return InvokeAsync<ControlPosition>(
                "getControlPosition");
        }

        /// <summary>
        /// Returns which drawing modes are available for the user to select, in the order they are displayed. 
        /// This does not include the null drawing mode, which is added by default. 
        /// Possible drawing modes are "Point", "LineString" or "Polygon".
        /// </summary>
        /// <returns></returns>
        public ValueTask<IEnumerable<string>> GetControls()
        {
            return InvokeAsync<IEnumerable<string>>(
                "getControls");
        }

        /// <summary>
        /// Returns the current drawing mode of the given Data layer. 
        /// A drawing mode of null means that the user can interact with the map as normal, and clicks do not draw anything. Possible drawing modes are null, "Point", "LineString" or "Polygon".
        /// </summary>
        /// <returns></returns>
        public ValueTask<string> GetDrawingMode()
        {
            return InvokeAsync<string>(
                "getDrawingMode");
        }

        /// <summary>
        /// Returns the feature with the given ID, if it exists in the collection. Otherwise returns undefined.
        /// Note that the IDs 1234 and '1234' are equivalent.Either can be used to look up the same feature.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ValueTask<Data.Feature> GetFeatureById(OneOf<int, string> id)
        {
            return InvokeAsync<Feature>(
                "getFeatureById",
                id.Value);
        }

        /// <summary>
        /// Returns the map on which the features are displayed.
        /// </summary>
        /// <returns></returns>
        public async ValueTask<Map?> GetMap()
        {
            var mapRef = await InvokeAsync<IJSObjectReference?>("getMap");
            return mapRef == null ? null : new Map(mapRef);
        }

        /// <summary>
        /// Gets the style for all features in the collection.
        /// </summary>
        /// <returns></returns>
        public Task<OneOf<Func<Data.Feature, Data.StyleOptions>, Data.StyleOptions>> GetStyle()
        {
            //return Helper.InvokeWithDefinedGuidAndMethodAsync<Data.Feature>(
            //    "googleMapDataJsFunctions.invoke",
            //    _guid.ToString(),
            //    "getStyle");

            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads GeoJSON from a URL, and adds the features to the collection.
        /// NOTE: The GeoJSON is fetched using XHR, and may not work cross-domain.If you have issues, we recommend you fetch the GeoJSON using your choice of AJAX library, and then call addGeoJson().
        /// </summary>
        /// <param name="url"></param>
        /// <param name="otpions"></param>
        /// <returns></returns>
        public ValueTask LoadGeoJson(string url)
        {
            return this.InvokeVoidAsync(
                "loadGeoJson",
                url);
        }

        /// <summary>
        /// Changes the style of a feature. These changes are applied on top of the style specified by setStyle(). 
        /// Style properties set to null revert to the value specified via setStyle().
        /// </summary>
        /// <param name="feature"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public ValueTask OverrideSytle(Data.Feature feature, Data.StyleOptions style)
        {
            return this.InvokeVoidAsync(
                "overrideSytle",
                feature,
                style);
        }

        /// <summary>
        /// Removes a feature from the collection.
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public ValueTask Remove(Feature feature)
        {
            return this.InvokeVoidAsync(
                "remove",
                feature);
        }

        /// <summary>
        /// moves the effect of previous overrideStyle() calls. 
        /// The style of the given feature reverts to the style specified by setStyle().
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public ValueTask RevertStyle(Feature? feature = null)
        {
            return this.InvokeVoidAsync(
                "revertStyle",
                feature);
        }

        /// <summary>
        /// Sets the position of the drawing controls on the map.
        /// </summary>
        /// <param name="controlPosition"></param>
        /// <returns></returns>
        public ValueTask SetControlPosition(ControlPosition controlPosition)
        {
            return this.InvokeVoidAsync(
                "setControlPosition",
                controlPosition);
        }

        /// <summary>
        /// Sets which drawing modes are available for the user to select, in the order they are displayed. 
        /// This should not include the null drawing mode, which is added by default. 
        /// If null, drawing controls are disabled and not displayed. 
        /// Possible drawing modes are "Point", "LineString" or "Polygon".
        /// </summary>
        /// <param name="controls"></param>
        /// <returns></returns>
        public ValueTask SetControls(IEnumerable<string> controls)
        {
            return this.InvokeVoidAsync(
                "setControls",
                controls);
        }

        /// <summary>
        /// Sets the current drawing mode of the given Data layer. 
        /// A drawing mode of null means that the user can interact with the map as normal, and clicks do not draw anything. 
        /// Possible drawing modes are null, "Point", "LineString" or "Polygon".
        /// </summary>
        /// <param name="drawingMode"></param>
        /// <returns></returns>
        public ValueTask SetDrawingMode(string drawingMode)
        {
            return this.InvokeVoidAsync(
                "setDrawingMode",
                drawingMode);
        }

        /// <summary>
        /// Renders the features on the specified map. 
        /// If map is set to null, the features will be removed from the map.
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public ValueTask SetMap(Map map)
        {
            return this.InvokeVoidAsync(
                "setMap",
                map.Reference);
        }

        /// <summary>
        /// Sets the style for all features in the collection. 
        /// Styles specified on a per-feature basis via overrideStyle() continue to apply.
        /// Pass either an object with the desired style options, or a function that computes the style for each feature.
        /// The function will be called every time a feature's properties are updated.
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public async ValueTask SetStyle(OneOf<Func<Data.Feature, Data.StyleOptions>, Data.StyleOptions> style)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Exports the features in the collection to a GeoJSON object.
        /// </summary>
        /// <returns></returns>
        public async ValueTask<JsonDocument> ToGeoJson()
        {
            throw new NotImplementedException();
        }
    }
}
