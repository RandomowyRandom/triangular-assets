using System.Collections.Generic;
using UnityEngine;

namespace TriangularAssets
{
    public static class TriangularMath
    {
        /// <summary>
        /// Calculates the average of a list of vectors.
        /// </summary>
        /// <param name="vectors">
        /// List of vectors to average.
        /// </param>
        /// <returns>
        /// Returns the average of the vectors.
        /// </returns>
        public static Vector2 GetAverageVector2(List<Vector2> vectors)
        {
            //TODO: Find a better way of calculating the average of a list of vectors.
            
            var sum = Vector2.zero;
            vectors.ForEach(v => sum += v);
            var average = sum / vectors.Count;
            
            var magnitudeSum = 0f;
            vectors.ForEach(v => magnitudeSum += v.magnitude);
            var averageMagnitude = magnitudeSum / vectors.Count;
            
            average = average.normalized * averageMagnitude;
            
            return average;
        }
    }
}