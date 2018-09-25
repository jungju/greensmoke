using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GreenSmokeMonitor.ViewControl
{
    /// <summary>
    /// BaseControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BaseControl : UserControl
    {
        public BaseControl(Panel basePanel)
        {
            InitializeComponent();

            basePanel.Children.Add(this);
        }

        public void DrawScenario(UIElement uielement)
        {
            _StackPolicyBox.Children.Add(uielement);
        }

        public void DrawProduct(UIElement uielement)
        {
            _StackProductBox.Children.Add(uielement);
        }

        public void ViewProductInfo(string name,ImageSource image, int runningTime, int currentCT, int totalCT, bool isPower, bool isSafe)
        {
            _UConProductInfo.SetInfo(name, image,runningTime, currentCT, totalCT, isPower, isSafe);
            Storyboard storyboard = Resources["_SBOpenProductInfo"] as Storyboard;
            storyboard.Begin(this);
        }
    }
}
