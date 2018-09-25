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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GreenSmokeMonitor.ViewControl.Item
{
    /// <summary>
    /// GroupZone.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GroupZone : UserControl
    {
        private string _GroupID;
        private string _GroupName;
        private Dictionary<string, ProductItem> _ProductItems = new Dictionary<string, ProductItem>();

        public string GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        public GroupZone(string groupID,string name)
        {
            InitializeComponent();

            _GroupID = groupID;
            _GroupName = name;
        }

        public void AddProduct(string id, string name, ImageSource productImageSource, bool isPower, bool isSafe)
        {
            ProductItem newProduct = new ProductItem(id, name, productImageSource, isPower,isSafe);
            
            _ProductBox.Children.Add(newProduct);

            _ProductItems.Add(id, newProduct);
        }

        public void ChangeCTValue(string id, int value)
        {
            _ProductItems[id].ChangeCTValue(value);
        }


    }
}
