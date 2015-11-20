using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;

namespace JosePedroSilva.TFSScrumExtensions.Extensions
{
    public static class WorkItemExtensions
    {
        #region Private Variables
        #endregion

        #region Public Variables
        #endregion

        #region Properties
        #endregion

        #region Constructors
        #endregion

        #region Private & Internal Methods
        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the field.
        /// </summary>
        /// <param name="wi">The wi.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value.</param>
        /// <param name="ignoreIfMissing">if set to <c>true</c> [ignore if missing].</param>
        /// <returns<c>true</c> if the field was updated, <c>false</c> otherwise</returns>
        public static Boolean UpdateField(this WorkItem wi, string fieldName, object value, bool ignoreIfMissing = true)
        {
            if(ignoreIfMissing && !wi.Fields.Contains(fieldName))
            {
                return false;
            }

            wi.Fields[fieldName].Value = value;

            return true;
        }

        #endregion

        #region Event handling Methods
        #endregion
    }
}
