using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteDeFrais.Domain.Properties
{
    public class Resources
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources()
        {
        }
        public static string Validated
        {
            get
            {
                return ResourceManager.GetString("Validated", resourceCulture);
            }
        }
        public static string Prepared
        {
            get
            {
                return ResourceManager.GetString("Prepared", resourceCulture);
            }
        }
        public static string Canceled
        {
            get
            {
                return ResourceManager.GetString("Canceled", resourceCulture);
            }
        }
        public static string Refused
        {
            get
            {
                return ResourceManager.GetString("Refused", resourceCulture);
            }
        }
        public static string Submitted
        {
            get
            {
                return ResourceManager.GetString("Submitted", resourceCulture);
            }
        }

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Inn4RH.Domain.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
    }
}