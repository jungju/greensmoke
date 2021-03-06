using GreenSmoke.Core;
using System.Runtime.Serialization;
using System.ServiceModel;


// 정책사항
namespace GreenSmoke.Core.Item
{
    // 카테고리에 대한 하나의 아이템이다.
    [DataContract]
    public sealed class Category : UseSubItem
    {
        private EnumItem _CurrentItem;

        public Category(string name,string categoryItemImagePath ,EnumItem currentItem)
        {
            _Name = name;
            _CurrentItem = currentItem;
            _ImagePath = categoryItemImagePath;

            base._CurrentType = EnumItem.CategoryItem;
        }

        public static Category UnknowCategoryItem(EnumItem currentItem)
        {
            Category newItem = new Category("Unknow",Config.Path.UnknowCategryImagePath ,currentItem);

            return newItem;
        }
    }
}