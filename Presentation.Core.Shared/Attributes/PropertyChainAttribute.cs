﻿using Presentation.Patterns.Interfaces;

namespace Presentation.Patterns.Attributes
{
    /// <summary>
    /// When a property, with this attribute, changes (i.e.
    /// OnPropertyChanged is called) it can cause another
    /// property change event to occur on another property.
    /// </summary>
    /// <example>
    /// We might have a Details property which is a list of
    /// items, by default listing all items. When a FilterByUser 
    /// property is set to True the Details should change to 
    /// represent just the user's Details. But as the Details 
    /// property might only have a getter (for example if 
    /// returning an ICollectionView) then we need a way to 
    /// tell the UI to refresh the Details on FilterByUser
    /// changing. We can therefore chain these propetries 
    /// to achieve this.
    /// </example>
    public sealed class PropertyChainAttribute : RuleAttribute
    {
        public PropertyChainAttribute(params string[] properties)
        {
            Properties = properties;
        }

        /// <summary>
        /// The array of properties to fire property change events
        /// on when the property associated with this attribute 
        /// changes
        /// </summary>
        public string[] Properties { get; }

        public override bool PostInvoke(object o)
        {
            if (Properties != null)
            {
                var vm = o as INotifyViewModel;
#if !NET4
                vm?.RaiseMultiplePropertyChanged(Properties);
#else
                if (vm != null)
                {
                    vm.RaiseMultiplePropertyChanged(Properties);
                }
#endif
            }
            return true;
        }
    }
}
