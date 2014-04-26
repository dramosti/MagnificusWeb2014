using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HLP.Magnificus.View.Web.Triggers
{
    public class SetFocusTrigger : TargetedTriggerAction<Control>
    {
        /// <summary>
        /// Set focus to target control
        /// </summary>
        /// <param name="parameter">Not used</param>
        protected override void Invoke(object parameter)
        {
            if (Target == null) return;
            Dispatcher.BeginInvoke(() => { System.Windows.Browser.HtmlPage.Plugin.Focus(); });
            Dispatcher.BeginInvoke(() => { Target.Focus(); });
        }
    }
}
