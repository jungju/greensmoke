using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GreenSmokeMonitor.ClentLogic
{
    public class ClientLogic
    {
        ViewControl.BaseControl _BaseControl;
        Dictionary<string, ViewControl.Item.ProductItem> _ProductItem = new Dictionary<string, GreenSmokeMonitor.ViewControl.Item.ProductItem>();
        Dictionary<string, ViewControl.Item.PolicyItem> _PolicyItems = new Dictionary<string, GreenSmokeMonitor.ViewControl.Item.PolicyItem>();
        ServiceReference.LocalClient _ServerService;

        Dictionary<string, ServiceReference.Group> _Groups;
        Dictionary<string, ServiceReference.MultiStrip> _MultiStrips;
        Dictionary<string, ServiceReference.Adaptor> _Adaptors;

        public ClientLogic(Panel mainPanel)
        {
            _BaseControl = new GreenSmokeMonitor.ViewControl.BaseControl(mainPanel);

            try
            {
                _ServerService = new GreenSmokeMonitor.ServiceReference.LocalClient();
                _ServerService.Open();
            }
            catch
            {
            }

            GetItemList();
        }

        #region 테스트 코드

        private void Test()
        {
            //AddGroup("id1", "name");
            //AddGroup("id2", "name");
            //AddGroup("id3", "name");
            AddProduct("id1", "pd1", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif",true,true);
            AddProduct("id1", "pd2", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif", true, true);
            AddProduct("id1", "pd3", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif", true, true);
            AddProduct("id1", "pd4", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif", true, true);
            AddProduct("id1", "pd5", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif", true, true);
            AddProduct("id1", "pd6", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif", true, true);
            AddProduct("id1", "pd7", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif", true, true);
            AddProduct("id1", "pd8", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif", true, true);
            AddProduct("id1", "pd9", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif", true, true);
            AddProduct("id1", "pd10", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif", true, true);
            AddProduct("id1", "pd11", "신발", "http://icon.daum-img.net/top/2008/logo_daum2008.gif", true, true);
            AddPlicy("ids1","취침","http://icon.daum-img.net/top/2008/logo_daum2008.gif");
            AddPlicy("ids2", "식사", "http://icon.daum-img.net/top/2008/logo_daum2008.gif");
            AddPlicy("ids3", "휴식", "http://icon.daum-img.net/top/2008/logo_daum2008.gif");
            AddPlicy("ids4", "청소", "http://icon.daum-img.net/top/2008/logo_daum2008.gif");
        }

        #endregion

        void GetItemList()
        {
            //서버로 부터 리스트를 다운받은다.
            try
            {
                _Groups = _ServerService.GetGroupList();
                _MultiStrips = _ServerService.GetMultiStripList();
                _Adaptors = _ServerService.GetAdaptorList();

                foreach (string adaptorKey in _Adaptors.Keys)
                {
                    //어댑터를 추가한다.
                    if (_Adaptors[adaptorKey].IsEnable)
                    {
                        AddProduct("",
                            _Adaptors[adaptorKey].ConnectedProduct.ID,
                            _Adaptors[adaptorKey].ConnectedProduct.Name,
                            _Adaptors[adaptorKey].ConnectedProduct.ImagePath,
                            _Adaptors[adaptorKey].IsPowerOn,
                            _Adaptors[adaptorKey].IsSafe
                            );
                    }
                }
            }
            catch
            {
            }
        }


        //public void AddGroup(string groupID,string groupName)
        //{
        //    ViewControl.GroupZone groupZone = new GreenSmokeMonitor.ViewControl.GroupZone(groupID, groupName);
        //    _GroupZones.Add(groupID, groupZone);
        //    _ProductPanel.Children.Add(groupZone);
        //}

        public void AddProduct(string groupID,string productID,string productName,string productImagePath,bool isPower, bool isSafe)
        {
            BitmapImage image = new BitmapImage(new Uri(productImagePath));

            ViewControl.Item.ProductItem newProduct = new ViewControl.Item.ProductItem(productID, productName, image, isPower, isSafe);

            newProduct.SelectItem += new GreenSmokeMonitor.ViewControl.Item.ProductItem.SelectItemHandler(Product_SelectItem);

            //_ProductItem.Add(productID, newProduct);

            _BaseControl.DrawProduct(newProduct);
        }

        void Product_SelectItem(object sender, EventArgs e)
        {
            ViewControl.Item.ProductItem product = sender as ViewControl.Item.ProductItem;

            //Todo:여기서제품정보 받아오기

            _BaseControl.ViewProductInfo(product.Name,product.ProductImageSource, 10, 10, 10, product.IsPowerOn,product.IsSafeOn);
        }

        public void AddPlicy(string policyID, string policyName, string policyImagePath)
        {
            BitmapImage image = new BitmapImage(new Uri(policyImagePath));

            ViewControl.Item.PolicyItem scenario = new GreenSmokeMonitor.ViewControl.Item.PolicyItem(policyID, policyName);

            scenario.SelectChange += new GreenSmokeMonitor.ViewControl.Item.PolicyItem.SelectChangeHandler(scenario_SelectChange);

            _PolicyItems.Add(policyID, scenario);

            _BaseControl.DrawScenario(scenario);
        }

        void scenario_SelectChange(object sender, EventArgs e)
        {
            //TODO:시나리오 시작을 서버에 보냄
        }

        public void PowerOn(string productId)
        {
            _ProductItem[productId].PowerOn();
        }

        public void PowerOff(string productId)
        {
            _ProductItem[productId].PowerOff();
        }

        void GetCTValue()
        {
            //CT값을 받는다. #<ID>@<Value>...
            char[] itemUnit = {'#'};
            char[] valueUnit = {'@'};

            string ctValue = "";

            string[] itemValues = ctValue.Split(itemUnit);

            foreach(string itemValue in itemValues)
            {
                string[] idValue = itemValue.Split(valueUnit);

                string id = idValue[0];
                int value = int.Parse(idValue[1]);

                _ProductItem[id].ChangeCTValue(value);
            }
        }
    }
}
