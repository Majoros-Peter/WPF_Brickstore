using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfApp1
{
    internal class Part
    {
        public static bool operator !=(Part left, Part right)
        {
            return left.ItemID == right.ItemID;
        }

        public static bool operator ==(Part left, Part right) => true;

        public static List<Part> PartList = [];
        public Part(XElement elem)
        {
            ItemID = elem.Element("ItemID").Value;
            ItemName = elem.Element("ItemName").Value;
            CategoryName = elem.Element("CategoryName").Value;
            ColorName = elem.Element("ColorName").Value;
            Qty = Convert.ToInt32(elem.Element("Qty").Value);

            PartList.Add(this);
        }

        public string ItemID { get; init; }
        public string ItemName { get; init; }
        public string CategoryName { get; init; }
        public string ColorName { get; init; }
        public int Qty { get; init; }
    }
}
