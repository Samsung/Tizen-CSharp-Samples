using System;
using Xamarin.Forms;

namespace ImageUtilSample
{
    public sealed class SecurityProvider
    {
        /// <summary>
        /// Instance for lazy initialization of SecurityProvider.
        /// </summary>
        private static readonly Lazy<SecurityProvider> lazy = new Lazy<SecurityProvider>(() => new SecurityProvider());

        /// <summary>
        /// A Record Item Provider class instance which provides Record Items.
        /// When it is called for the first time, SecurityProvider will be created.
        /// </summary>
        public static SecurityProvider Instance { get => lazy.Value; }

        /// <summary>
        /// Instance of IMediaPlayerSecurity for get the implementation of each platform.
        /// </summary>
        private static IImageUtilSecurity imageutilSecurity;

        /// <summary>
        /// media storage privilege.
        /// </summary>
        private const string privilegeMediastorage = "http://tizen.org/privilege/mediastorage";

        /// <summary>
        /// external storage privilege.
        /// </summary>
        private const string privilegeExternalstorage = "http://tizen.org/privilege/externalstorage";

        /// <summary>
        /// SecurityProvider Constructor.
        /// A Constructor which will initialize the SecurityProvider instance.
        /// </summary>
        public void CheckPrivilege()
        {
            imageutilSecurity = DependencyService.Get<IImageUtilSecurity>();

            imageutilSecurity.CheckPrivilege(privilegeMediastorage);
            imageutilSecurity.CheckPrivilege(privilegeExternalstorage);
        }
    }
}
