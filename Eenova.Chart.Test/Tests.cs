using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Eenova.Chart.Elements;

namespace Eenova.Chart.Test
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void TestMarkupLineItem()
        {
            var item = new MarkupLineItem();
            item.PropertyChanged += (s, e) =>
            {
                Assert.IsTrue(e.PropertyName == "Position");
            };
            item.Position = 2;
        }
    }
}