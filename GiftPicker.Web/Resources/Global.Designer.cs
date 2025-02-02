﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GiftPicker.Web.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Global {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Global() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GiftPicker.Web.Resources.Global", typeof(Global).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sorry, it seems that an error occurred.
        /// </summary>
        public static string GeneralError {
            get {
                return ResourceManager.GetString("GeneralError", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The username or password provided is incorrect.
        /// </summary>
        public static string IncorrectCredentials
        {
            get
            {
                return ResourceManager.GetString("IncorrectCredentials", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Successfully created.
        /// </summary>
        public static string ItemCreated
        {
            get
            {
                return ResourceManager.GetString("ItemCreated", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to The birthday for the selected year has already passed..
        /// </summary>
        public static string PassedBirthday
        {
            get
            {
                return ResourceManager.GetString("PassedBirthday", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to A voting session for selected user in {0} is already set up.
        /// </summary>
        public static string UserVotingExists
        {
            get
            {
                return ResourceManager.GetString("UserVotingExists", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to You cannot create voting for yourself..
        /// </summary>
        public static string UserVotingSelf
        {
            get
            {
                return ResourceManager.GetString("UserVotingSelf", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Voting stopped.
        /// </summary>
        public static string UserVotingStopped
        {
            get
            {
                return ResourceManager.GetString("UserVotingStopped", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to You have already voted..
        /// </summary>
        public static string VoteExists
        {
            get
            {
                return ResourceManager.GetString("VoteExists", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to Gift Picker.
        /// </summary>
        public static string WebsiteTitle {
            get {
                return ResourceManager.GetString("WebsiteTitle", resourceCulture);
            }
        }
    }
}
