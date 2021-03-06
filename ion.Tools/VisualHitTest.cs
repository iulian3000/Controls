﻿namespace Ion.Tools
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// Class Description
    /// </summary>
    public class Visual
    {
        /// <summary>
        /// Gets the parent of a given UIElement
        /// </summary>
        /// <typeparam name="T"> Type of the parent </typeparam>
        /// <param name="child"> The UIElement </param>
        /// <returns> Returns the resust as T</returns>
        public static T GetParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (child != null && child.GetType() != typeof(T))
            {
                child = VisualTreeHelper.GetParent(child);
            }

            return child as T;
        }

        /// <summary>
        /// Gets the parent of a given UIElement at a specified POINT
        /// </summary>
        /// <typeparam name="T"> Type of the parent </typeparam>
        /// <param name="child">      The UIElement </param>
        /// <param name="point">      The POINT of Hittesting </param>
        /// <param name="firstFound">
        /// Returns the first found parent at specified POINT, default is False
        /// </param>
        /// <returns> Returns the result as <see cref="T" /> </returns>
        public static T GetParent<T>(DependencyObject child, Point point, bool firstFound = false) where T : DependencyObject
        {
            var hrVisualList = new List<DependencyObject>();

            VisualTreeHelper.HitTest(
                child as System.Windows.Media.Visual,
                null,
                hr =>
                {
                    hrVisualList.Add(hr.VisualHit);
                    return HitTestResultBehavior.Continue;
                },
                new PointHitTestParameters(point));

            if (firstFound)
                return GetParent<T>(hrVisualList.FirstOrDefault()) as T;

            var tmpList = new List<DependencyObject>();

            foreach (var item in hrVisualList)
            {
                var parent = GetParent<T>(item);

                if (!tmpList.Contains(parent) && parent != null)
                    tmpList.Add(parent);
            }

            return tmpList.FirstOrDefault() as T;
        }

        /// <summary>
        /// Tests if parent has specified child
        /// </summary>
        /// <param name="child">  Child parameter </param>
        /// <param name="parent"> Parent parameter </param>
        /// <returns> Returns true if <see cref="child"/> is child of the <see cref="panrent"/></returns>
        public static bool IsChild(DependencyObject child, DependencyObject parent)
        {
            var tmp = VisualTreeHelper.GetParent(child);
            if (tmp == null)
                return false;

            while (tmp != parent)
            {
                tmp = VisualTreeHelper.GetParent(tmp);
                if (tmp == null)
                    return false;
            }

            return true;
        }
    }
}