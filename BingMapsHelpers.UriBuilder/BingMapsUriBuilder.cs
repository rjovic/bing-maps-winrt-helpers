using System;
using System.Globalization;

namespace BingMapsHelpers.UriBuilder
{
    public class BingMapsUriBuilder
    {
        private string _value;

        public BingMapsUriBuilder()
        {
            _value = "bingmaps:?";
        }

        /// <summary>
        /// Set Center point view on the map
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public BingMapsUriBuilder SetCenterPoint(double latitude, double longitude)
        {
            _value = _value + String.Format("{0}cp={1}~{2}", SetParametersDivider(), latitude.ToString(CultureInfo.InvariantCulture),
                                   longitude.ToString(CultureInfo.InvariantCulture));

            return this;
        }

        /// <summary>
        /// A rectangular area that specifies the bounding
        /// </summary>
        /// <param name="southlatitude"></param>
        /// <param name="northlatitude"></param>
        /// <param name="westlongitude"></param>
        /// <param name="eastlongitude"></param>
        /// <returns></returns>
        public BingMapsUriBuilder SetBoundingBox(double southlatitude, double northlatitude, double westlongitude, double eastlongitude)
        {
            _value = _value + String.Format("{0}bb={1}_{2}~{3}_{4}", SetParametersDivider(), southlatitude.ToString(CultureInfo.InvariantCulture),
                                            northlatitude.ToString(CultureInfo.InvariantCulture),
                                            westlongitude.ToString(CultureInfo.InvariantCulture),
                                            eastlongitude.ToString(CultureInfo.InvariantCulture));

            return this;
        }

        /// <summary>
        /// Search term which is a location, landmark or place and display it on the map.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public BingMapsUriBuilder Where(string address)
        {
            _value = _value + String.Format("{0}where={1}", SetParametersDivider(), address);
            return this;
        }

        /// <summary>
        /// Search term for local business or category of businesses.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public BingMapsUriBuilder Query(string term)
        {
            _value = _value + String.Format("{0}q={1}", SetParametersDivider(), term);
            return this;
        }

        /// <summary>
        /// Defines the zoom level of the map view. Valid values are 1-20 where 1 is zoomed all the way out.
        /// </summary>
        /// <param name="zoomLevel"></param>
        /// <returns></returns>
        public BingMapsUriBuilder SetZoomLevel(double zoomLevel)
        {
            if (zoomLevel <= 0 || zoomLevel > 20)
            {
                throw new ArgumentOutOfRangeException("Zoom level need to be between 1 and 20");
            }

            _value = _value + String.Format("{0}lvl={1}", SetParametersDivider(), zoomLevel.ToString(CultureInfo.InvariantCulture));
            return this;
        }

        /// <summary>
        /// Defines the map style. Aerial or road view
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public BingMapsUriBuilder SetMapStyle(MapStyleEnum style)
        {
            _value = _value + String.Format("{0}sty={1}", SetParametersDivider(), style.ToString().ToLower().Substring(0, 1));
            return this;
        }

        /// <summary>
        /// Specifies whether traffic information is included on the map.
        /// </summary>
        /// <param name="show"></param>
        /// <returns></returns>
        public BingMapsUriBuilder ShowTraffic(bool show)
        {
            _value = _value + String.Format("{0}trfc={1}", SetParametersDivider(), show ? "1" : "0");
            return this;
        }

        /// <summary>
        /// Defines the start and end of a route to draw on the map.
        /// </summary>
        /// <param name="fromLat"></param>
        /// <param name="fromLong"></param>
        /// <param name="toLat"></param>
        /// <param name="toLong"></param>
        /// <returns></returns>
        public BingMapsUriBuilder ShowRoute(double fromLat, double fromLong, double toLat, double toLong)
        {
            _value = _value + String.Format("{0}rtp=pos.{1}_{2}~pos.{3}_{4}", SetParametersDivider(),
                                   fromLat.ToString(CultureInfo.InvariantCulture),
                                   fromLong.ToString(CultureInfo.InvariantCulture),
                                   toLat.ToString(CultureInfo.InvariantCulture),
                                   toLong.ToString(CultureInfo.InvariantCulture));

            return this;
        }

        /// <summary>
        /// Defines the start and end of a route to draw on the map.
        /// </summary>
        /// <param name="fromLat"></param>
        /// <param name="fromLong"></param>
        /// <param name="toAddress"></param>
        /// <returns></returns>
        public BingMapsUriBuilder ShowRoute(double fromLat, double fromLong, string toAddress)
        {
            _value = _value + String.Format("{0}rtp=pos.{1}_{2}~adr.{3}", SetParametersDivider(),
                                            fromLat.ToString(CultureInfo.InvariantCulture),
                                            fromLong.ToString(CultureInfo.InvariantCulture), toAddress);

            return this;
        }

        /// <summary>
        /// Defines the start and end of a route to draw on the map.
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toLat"></param>
        /// <param name="toLong"></param>
        /// <returns></returns>
        public BingMapsUriBuilder ShowRoute(string fromAddress, double toLat, double toLong)
        {
            _value = _value + String.Format("{0}rtp=adr.{1}~pos.{2}_{3}", SetParametersDivider(), fromAddress,
                                            toLat.ToString(CultureInfo.InvariantCulture),
                                            toLong.ToString(CultureInfo.InvariantCulture));

            return this;
        }

        /// <summary>
        /// Defines the start and end of a route to draw on the map.
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <returns></returns>
        public BingMapsUriBuilder ShowRoute(string fromAddress, string toAddress)
        {
            _value = _value + String.Format("{0}rtp=adr.{1}~adr.{2}", SetParametersDivider(), fromAddress, toAddress);
            return this;
        }

        private string SetParametersDivider()
        {
            return _value.Length > 10 ? "&" : String.Empty;
        }

        /// <summary>
        /// Launch "Maps" application and show Bing maps
        /// </summary>
        public async void ShowMap()
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(_value));
        }
    }

    public enum MapStyleEnum
    {
        Aerial,
        Road
    }
}
