using System;
using System.Collections;
using System.Linq;
using System.Reflection;
/*
In App.xaml:
<Application.Resources>
<vm:ViewModelLocator xmlns:vm="clr-namespace:XdtTransform"
   x:Key="Locator" />
</Application.Resources>

In the View:
DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

You can also use Blend to do all this with the tool's support.
See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;

namespace XdtTransform.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainWindowVm>();
            SimpleIoc.Default.Register<ConfigLoaderVm>();
        }

        public MainWindowVm Main => ServiceLocator.Current.GetInstance<MainWindowVm>();

        public ConfigLoaderVm ConfigLoader => ServiceLocator.Current.GetInstance<ConfigLoaderVm>(Guid.NewGuid().ToString());
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}