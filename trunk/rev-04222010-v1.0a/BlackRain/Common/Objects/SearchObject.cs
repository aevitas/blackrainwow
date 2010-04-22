using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace BlackRain.Common.Objects
{
    /***
     * Note: This functionality is based on Apoc's implementation of Object Searching. For more information, visit the link below.
     * http://www.mmowned.com/forums/programming/245380-bot-developers-object-searching-easy-way.html
     * 
     * Does include some changes to make it more C# 4.0, and uses LINQ instead of C# in some places.
     */

    /// <summary>
    /// Search parameters.
    /// </summary>
    public class SearchParams
    {
        public string Name;

        public int? MinLevel;
        public int? MaxLevel;
        public int? LevelExact;

        public int? ObjectType;

        public bool? Dead;
        public bool? CanAttack;

        public bool? IncludeMe;

        public double? MinDistance;
        public double? MaxDistance;

        public ulong? Guid;
    }

    internal static class Compare
    {
        public static bool Equal<T>(T? searchParam, T val) where T : struct
        {
            // If 'searchParam' has no value, return true (assume user doesn't want this param used)
            // Otherwise, make sure the actual values are equal.
            return !searchParam.HasValue || searchParam.Value.Equals(val);
        }

        public static bool Equal(string searchParam, string val)
        {
            // Same as the other Equal method. Except we need to adjust for strings.
            if (searchParam == null)
                return true;
            return val == searchParam;
        }

        public static bool LessOrEqual(int? param, int val)
        {
            if (!param.HasValue)
                return true;
            return param.Value <= val;
        }

        public static bool LessOrEqual(double? param, double val)
        {
            if (!param.HasValue)
                return true;
            return param.Value <= val;
        }

        public static bool GreaterOrEqual(int? param, int val)
        {
            if (!param.HasValue)
            {
                return true;
            }
            return param.Value >= val;
        }
        public static bool GreaterOrEqual(double? param, double val)
        {
            if (!param.HasValue)
                return true;
            return param.Value >= val;
        }
    }


    public class SearchObject<T> : IEnumerable<T> where T: WowObject
    {
        public SearchParams Parameters;
        private List<T> _objectList = new List<T>();

        public SearchObject([Optional, DefaultParameterValue(null)]SearchParams @params)
        {
            if (@params == null)
            {
                Parameters = new SearchParams();
            }
            else
            {
                Parameters = @params;
                Refresh();
            }

        }

        public SearchObject()
        {
            Parameters = new SearchParams();
        }

        /// <summary>
        /// Refreshes the Object Search.
        /// </summary>
        /// <param name="parameters"></param>
        public void Refresh([Optional, DefaultParameterValue(null)]SearchParams @parameters)
        {
            List<T> list = (@parameters == null) ? _objectList = DoSearch<T>(ObjectManager.Objects, Parameters) : _objectList = DoSearch<T>(ObjectManager.Objects, parameters);
        }

        /// <summary>
        /// Searches the Object list for objects matching the given criteria.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects">An enumerable list of objects to search in.</param>
        /// <param name="searchParams">The criteria the object(s) have to meet.</param>
        /// <returns></returns>
        public static List<T> DoSearch<T>(IEnumerable<WowObject> objects, SearchParams searchParams) where T : WowObject
        {
            IEnumerable<WowObject> filtered = from o in objects
                                              let lvl = o.Level
                                              where
                                                  Compare.Equal(searchParams.Guid, o.GUID) &&
                                                  Compare.Equal(searchParams.LevelExact, lvl) &&
                                                  Compare.Equal(searchParams.ObjectType, o.Type) &&
                                                  Compare.LessOrEqual(searchParams.MinLevel, lvl) &&
                                                  Compare.GreaterOrEqual(searchParams.MaxLevel, lvl) &&
                                                  (o is T) &&
                                                  Compare.Equal(searchParams.IncludeMe, o.IsMe)
                                              select o;

            return filtered.Select(o => o as T).ToList();
        }


        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return _objectList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion


    }
}
