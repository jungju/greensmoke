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

namespace GreenSmokeMonitor.ViewControl.Item
{
    /// <summary>
    /// PolicyItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PolicyItem : UserControl
    {
        public delegate void SelectChangeHandler(object sender, EventArgs e);
        public event SelectChangeHandler SelectChange; 

        private string _ID;
        private bool _ThisOn = false;

        public string ID
        {
            get { return _ID; }
        }

        public string ScenarioName
        {
            get { return _ConModeName.Content.ToString(); }
            set { _ConModeName.Content = value; }
        }

        public bool ThisOn
        {
            set { _ThisOn = value; }
        }

        public PolicyItem(string id, string name)
        {
            InitializeComponent();

            _ID = id;
            _ConModeName.Content = name;

            _ConScenarioItem.MouseDown += new MouseButtonEventHandler(_ScenarioItem_MouseDown);
        }

        void _ScenarioItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_ThisOn)
            {
                _ThisOn = true;

                Storyboard modeOnStoryboard = Resources["_SBModeOn"] as Storyboard;

                modeOnStoryboard.Begin(_ConScenarioItem);

                SelectChange(this, EventArgs.Empty);
            }
            else
            {
                _ThisOn = false;

                Storyboard modeOffStoryboard = Resources["_SBModeOff"] as Storyboard;

                modeOffStoryboard.Begin(_ConScenarioItem);

                SelectChange(this, EventArgs.Empty);
            }
        }
    }
}
