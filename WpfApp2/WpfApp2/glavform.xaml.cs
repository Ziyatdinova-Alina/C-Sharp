using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for glavform.xaml
    /// </summary>
    public partial class glavform : Window
    {
        XDocument doc1;
        XDocument doc2;
        XDocument doc3;
        public ObservableCollection<object> SUPPLIERSCollection;
        public ObservableCollection<object> DETAILSCollection;
        public ObservableCollection<object> SUPPLIESCollection;
        private int counter = 0;
        private int counter2 = 0;
        private bool yesno = false;


        public glavform()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem1_Click(object sender, RoutedEventArgs e)
        {
            counter = 1;

            dobavlenie5.Visibility = Visibility.Hidden;
            Suppliers.Visibility = Visibility.Visible;
            Details.Visibility = Visibility.Hidden;
            Supplies.Visibility = Visibility.Hidden;

            doc1 = XDocument.Load("..\\..\\Supppliers.xml");
            var SUPPLIERS = (from x in doc1.Element("Suppliers").Elements("Supplier")
                             orderby x.Element("SupplierCode").Value
                             select new
                             {
                                 КодПоставщика = x.Element("SupplierCode").Value,
                                 НазваниеПоставщика = x.Element("SupplierName").Value,
                                 АдресПоставщика = x.Element("SupplierAddress").Value,
                                 ТелефонПоставщика = x.Element("SupplierContact").Value
                             }).ToList();
            var SUPPLIERSCollection = new ObservableCollection<object>(SUPPLIERS);
            dg.ItemsSource = SUPPLIERSCollection;
        }
        private void MenuItem2_Click(object sender, RoutedEventArgs e)
        {
            counter = 2;

            dobavlenie5.Visibility = Visibility.Visible;
            Suppliers.Visibility = Visibility.Hidden;
            Details.Visibility = Visibility.Visible;
            Supplies.Visibility = Visibility.Hidden;

            doc2 = XDocument.Load("..\\..\\Details.xml");
            var DETAILS = (from x in doc2.Element("Details").Elements("Detail")
                            orderby x.Element("DetailCode").Value
                           select new
                            {
                                КодДетали = x.Element("DetailCode").Value,
                                НазваниеДетали = x.Element("DetailName").Value,
                                АртикулДетали = x.Element("DetailArticle").Value,
                                ЦенаДетали = x.Element("DetailPrices").Elements("DetailPrice").LastOrDefault()?.Element("PricePrice").Value,
                                ПримечаниеДетали = x.Element("DetailNote").Value
                            }).ToList();
            var DETAILSCollection = new ObservableCollection<object>(DETAILS);
            dg.ItemsSource = DETAILSCollection;

            if (counter2 == 1)
            {
                dg.Columns.Clear();
                counter2 = 0;
                var DETAILS2 = (from x in doc2.Element("Details").Elements("Detail")
                               orderby x.Element("DetailCode").Value
                               select new
                               {
                                   КодДетали = x.Element("DetailCode").Value,
                                   ЦенаДетали = x.Element("DetailPrices").Elements("DetailPrice").LastOrDefault()?.Element("PricePrice").Value,
                                   ДатаЦены = x.Element("DetailPrices").Elements("DetailPrice").LastOrDefault()?.Element("PriceDate").Value
                               }).ToList();
                var DETAILS2Collection = new ObservableCollection<object>(DETAILS2);
                dg.ItemsSource = DETAILS2Collection;

            }

        }
        private void MenuItem3_Click(object sender, RoutedEventArgs e)
        {
            counter = 3;

            dobavlenie5.Visibility = Visibility.Hidden;
            Suppliers.Visibility = Visibility.Hidden;
            Details.Visibility = Visibility.Hidden;
            Supplies.Visibility = Visibility.Visible;

            doc3 = XDocument.Load("..\\..\\Supplies.xml");
            var SUPPLIES = (from x in doc3.Element("Supplies").Elements("Supply")
                           orderby x.Element("SupplierCode").Value
                           select new
                           {
                               КодПоставщика = x.Element("SupplierCode").Value,
                               КодДетали = x.Element("DetailCode").Value,
                               Количество = x.Element("Quantity").Value,
                               Дата = x.Element("Date").Value
                           }).ToList();
            var SUPPLIESCollection = new ObservableCollection<object>(SUPPLIES);
            dg.ItemsSource = SUPPLIESCollection;


        }

        private void MenuItemDobav_Click(object sender, RoutedEventArgs e)
        {
            counter2 = 0;
            dobav.Visibility = Visibility.Visible;
            red.Visibility = Visibility.Hidden;
            del.Visibility = Visibility.Hidden;
            poisk.Visibility = Visibility.Hidden;
            price.Visibility = Visibility.Hidden;
        }
        private void MenuItemRed_Click(object sender, RoutedEventArgs e)
        {
            counter2 = 0;
            dobav.Visibility = Visibility.Hidden;
            red.Visibility = Visibility.Visible;
            del.Visibility = Visibility.Hidden;
            poisk.Visibility = Visibility.Hidden;
            price.Visibility = Visibility.Hidden;
        }
        private void MenuItemDel_Click(object sender, RoutedEventArgs e)
        {
            counter2 = 0;
            dobav.Visibility = Visibility.Hidden;
            red.Visibility = Visibility.Hidden;
            del.Visibility = Visibility.Visible;
            poisk.Visibility = Visibility.Hidden;
            price.Visibility = Visibility.Hidden;
        }
        private void MenuItemPoisk_Click(object sender, RoutedEventArgs e)
        {
            counter2 = 0;
            dobav.Visibility = Visibility.Hidden;
            red.Visibility = Visibility.Hidden;
            del.Visibility = Visibility.Hidden;
            poisk.Visibility = Visibility.Visible;
            price.Visibility = Visibility.Hidden;
        }
        private void MenuItemPrice_Click(object sender, RoutedEventArgs e)
        {
            counter2 = 1;
            dobav.Visibility = Visibility.Hidden;
            red.Visibility = Visibility.Hidden;
            del.Visibility = Visibility.Hidden;
            poisk.Visibility = Visibility.Hidden;
            price.Visibility = Visibility.Visible;
        }
        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void buttondobav_Click(object sender, RoutedEventArgs e)
        {
            if (counter == 1)
            {
                IEnumerable<XElement> tests =
                from el in doc1.Elements("Suppliers").Elements("Supplier")
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno == true)
                {
                    doc1.Element("Suppliers").Add(new XElement("Supplier",
                    new XElement("SupplierCode", dobavlenie1.Text),
                    new XElement("SupplierName", dobavlenie2.Text),
                    new XElement("SupplierAddress", dobavlenie3.Text),
                    new XElement("SupplierContact", dobavlenie4.Text)));


                    doc1.Save("..\\..\\Supppliers.xml");

                    var SUPPLIERS = (from x in doc1.Element("Suppliers").Elements("Supplier")
                                     orderby x.Element("SupplierCode").Value
                                     select new
                                     {
                                         КодПоставщика = x.Element("SupplierCode").Value,
                                         НазваниеПоставщика = x.Element("SupplierName").Value,
                                         АдресПоставщика = x.Element("SupplierAddress").Value,
                                         ТелефонПоставщика = x.Element("SupplierContact").Value
                                     }).ToList();
                    var SUPPLIERSCollection = new ObservableCollection<object>(SUPPLIERS);
                    dg.ItemsSource = SUPPLIERSCollection;

                    MessageBox.Show("Данные добавлены");
                    yesno = false;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
                dobavlenie1.Clear();
                dobavlenie2.Clear();
                dobavlenie3.Clear();
                dobavlenie4.Clear();
                dobavlenie5.Clear();
            }
            if (counter == 2)
            {
                IEnumerable<XElement> tests =
                from el in doc2.Elements("Details").Elements("Detail")
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno == true)
                {
                    doc2.Element("Details").Add(new XElement("Detail",
                    new XElement("DetailCode", dobavlenie1.Text),
                    new XElement("DetailName", dobavlenie2.Text),
                    new XElement("DetailArticle", dobavlenie3.Text),
                    new XElement("DetailPrices",
                        new XElement("DetailPrice",
                            new XElement("PricePrice", dobavlenie4.Text),
                            new XElement("PriceDate", "-")
                        )
                    ),
                    new XElement("DetailNote", dobavlenie5.Text)));

                    doc2.Save("..\\..\\Details.xml");

                    var DETAILS = (from x in doc2.Element("Details").Elements("Detail")
                                   orderby x.Element("DetailCode").Value
                                   select new
                                   {
                                       КодДетали = x.Element("DetailCode").Value,
                                       НазваниеДетали = x.Element("DetailName").Value,
                                       АртикулДетали = x.Element("DetailArticle").Value,
                                       ЦенаДетали = x.Element("DetailPrices").Elements("DetailPrice").LastOrDefault()?.Element("PricePrice").Value,
                                       ПримечаниеДетали = x.Element("DetailNote").Value
                                   }).ToList();
                    var DETAILSCollection = new ObservableCollection<object>(DETAILS);
                    dg.ItemsSource = DETAILSCollection;

                    MessageBox.Show("Данные добавлены");
                    yesno = false;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
                dobavlenie1.Clear();
                dobavlenie2.Clear();
                dobavlenie3.Clear();
                dobavlenie4.Clear();
                dobavlenie5.Clear();
            }
            if (counter == 3)
            {
                IEnumerable<XElement> tests =
                from el in doc3.Elements("Supplies").Elements("Supply")
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno == true)
                {
                    doc3.Element("Supplies").Add(new XElement("Supply",
                    new XElement("SupplierCode", dobavlenie1.Text),
                    new XElement("DetailCode", dobavlenie2.Text),
                    new XElement("Quantity", dobavlenie3.Text),
                    new XElement("Date", dobavlenie4.Text)));

                    doc3.Save("..\\..\\Supplies.xml");
                    var SUPPLIES = (from x in doc3.Element("Supplies").Elements("Supply")
                                    orderby x.Element("SupplierCode").Value
                                    select new
                                    {
                                        КодПоставщика = x.Element("SupplierCode").Value,
                                        КодДетали = x.Element("DetailCode").Value,
                                        Количество = x.Element("Quantity").Value,
                                        Дата = x.Element("Date").Value
                                    }).ToList();
                    var SUPPLIESCollection = new ObservableCollection<object>(SUPPLIES);
                    dg.ItemsSource = SUPPLIESCollection;

                    MessageBox.Show("Данные добавлены");
                    yesno = false;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
                dobavlenie1.Clear();
                dobavlenie2.Clear();
                dobavlenie3.Clear();
                dobavlenie4.Clear();
                dobavlenie5.Clear();
            }
        }

        private void buttonred_Click(object sender, RoutedEventArgs e)
        {
            if (counter == 1)
            {
                IEnumerable<XElement> tests =
                from el in doc1.Elements("Suppliers").Elements("Supplier")
                where (string)el.Element("SupplierCode") == dobavlenie1.Text
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno ==  true)
                {
                    doc1.Elements("Suppliers").Elements("Supplier").First(b => ((string)b.Element("SupplierCode")) == dobavlenie1.Text).SetElementValue("SupplierName", dobavlenie2.Text);
                    doc1.Elements("Suppliers").Elements("Supplier").First(b => ((string)b.Element("SupplierCode")) == dobavlenie1.Text).SetElementValue("SupplierAddress", dobavlenie3.Text);
                    doc1.Elements("Suppliers").Elements("Supplier").First(b => ((string)b.Element("SupplierCode")) == dobavlenie1.Text).SetElementValue("SupplierContact", dobavlenie4.Text);

                    doc1.Save("..\\..\\Supppliers.xml");

                    var SUPPLIERS = (from x in doc1.Element("Suppliers").Elements("Supplier")
                                     orderby x.Element("SupplierCode").Value
                                     select new
                                     {
                                         КодПоставщика = x.Element("SupplierCode").Value,
                                         НазваниеПоставщика = x.Element("SupplierName").Value,
                                         АдресПоставщика = x.Element("SupplierAddress").Value,
                                         ТелефонПоставщика = x.Element("SupplierContact").Value
                                     }).ToList();
                    var SUPPLIERSCollection = new ObservableCollection<object>(SUPPLIERS);
                    dg.ItemsSource = SUPPLIERSCollection;

                    MessageBox.Show("Данные обновлены");
                    yesno = false;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
                    dobavlenie1.Clear();
                    dobavlenie2.Clear();
                    dobavlenie3.Clear();
                    dobavlenie4.Clear();
                    dobavlenie5.Clear();

            }
            if (counter == 2)
            {
                IEnumerable<XElement> tests =
                from el in doc2.Elements("Details").Elements("Detail")
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno == true)
                {
                    doc2.Elements("Details").Elements("Detail").First(b => ((string)b.Element("DetailCode")) == dobavlenie1.Text).SetElementValue("DetailName", dobavlenie2.Text);
                    doc2.Elements("Details").Elements("Detail").First(b => ((string)b.Element("DetailCode")) == dobavlenie1.Text).SetElementValue("DetailArticle", dobavlenie3.Text);
                    doc2.Elements("Details").Elements("Detail").First(b => ((string)b.Element("DetailCode")) == dobavlenie1.Text).SetElementValue("DetailPrices", dobavlenie4.Text); ;
                    doc2.Elements("Details").Elements("Detail").First(b => ((string)b.Element("DetailCode")) == dobavlenie1.Text).SetElementValue("DetailNote", dobavlenie5.Text);


                    doc2.Save("..\\..\\Details.xml");

                    var DETAILS = (from x in doc2.Element("Details").Elements("Detail")
                                   orderby x.Element("DetailCode").Value
                                   select new
                                   {
                                       КодДетали = x.Element("DetailCode").Value,
                                       НазваниеДетали = x.Element("DetailName").Value,
                                       АртикулДетали = x.Element("DetailArticle").Value,
                                       ЦенаДетали = x.Element("DetailPrices").Value,
                                       ПримечаниеДетали = x.Element("DetailNote").Value
                                   }).ToList();
                    var DETAILSCollection = new ObservableCollection<object>(DETAILS);
                    dg.ItemsSource = DETAILSCollection;

                    MessageBox.Show("Данные обновлены");
                    yesno = false;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
                dobavlenie1.Clear();
                dobavlenie2.Clear();
                dobavlenie3.Clear();
                dobavlenie4.Clear();
                dobavlenie5.Clear();
            }
            if (counter == 3)
            {
                IEnumerable<XElement> tests =
                from el in doc3.Elements("Supplies").Elements("Supply")
                where (string)el.Element("SupplierCode") == dobavlenie1.Text
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno == true)
                {
                    doc3.Elements("Supplies").Elements("Supply").First(b => ((string)b.Element("SupplierCode")) == dobavlenie1.Text).SetElementValue("DetailCode", dobavlenie2.Text);
                    doc3.Elements("Supplies").Elements("Supply").First(b => ((string)b.Element("SupplierCode")) == dobavlenie1.Text).SetElementValue("Quantity", dobavlenie3.Text);
                    doc3.Elements("Supplies").Elements("Supply").First(b => ((string)b.Element("SupplierCode")) == dobavlenie1.Text).SetElementValue("Date", dobavlenie4.Text);


                    doc3.Save("..\\..\\Supplies.xml");
                    var SUPPLIES = (from x in doc3.Element("Supplies").Elements("Supply")
                                    orderby x.Element("SupplierCode").Value
                                    select new
                                    {
                                        КодПоставщика = x.Element("SupplierCode").Value,
                                        КодДетали = x.Element("DetailCode").Value,
                                        Количество = x.Element("Quantity").Value,
                                        Дата = x.Element("Date").Value
                                    }).ToList();
                    var SUPPLIESCollection = new ObservableCollection<object>(SUPPLIES);
                    dg.ItemsSource = SUPPLIESCollection;

                    MessageBox.Show("Данные обновлены");
                    yesno = false;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
                dobavlenie1.Clear();
                dobavlenie2.Clear();
                dobavlenie3.Clear();
                dobavlenie4.Clear();
                dobavlenie5.Clear();
            }
        }

        private void buttondel_Click(object sender, RoutedEventArgs e)
        {
            if (counter == 1)
            {
                IEnumerable<XElement> tests =
                from el in doc1.Elements("Suppliers").Elements("Supplier")
                where (string)el.Element("SupplierCode") == dobavlenie1.Text
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno == true)
                {
                    doc1.Elements("Suppliers").Elements("Supplier").First(b => ((string)b.Element("SupplierCode")) == dobavlenie1.Text).Remove();

                    doc1.Save("..\\..\\Supppliers.xml");

                    var SUPPLIERS = (from x in doc1.Element("Suppliers").Elements("Supplier")
                                     orderby x.Element("SupplierCode").Value
                                     select new
                                     {
                                         КодПоставщика = x.Element("SupplierCode").Value,
                                         НазваниеПоставщика = x.Element("SupplierName").Value,
                                         АдресПоставщика = x.Element("SupplierAddress").Value,
                                         ТелефонПоставщика = x.Element("SupplierContact").Value
                                     }).ToList();
                    var SUPPLIERSCollection = new ObservableCollection<object>(SUPPLIERS);
                    dg.ItemsSource = SUPPLIERSCollection;

                    MessageBox.Show("Данные удалены");
                    yesno = false;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
                    dobavlenie1.Clear();
                    dobavlenie2.Clear();
                    dobavlenie3.Clear();
                    dobavlenie4.Clear();
                    dobavlenie5.Clear();
            }
            if (counter == 2)
            {
                IEnumerable<XElement> tests =
                from el in doc2.Elements("Details").Elements("Detail")
                where (string)el.Element("DetailCode") == dobavlenie1.Text
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno == true)
                {
                    doc2.Elements("Details").Elements("Detail").First(b => ((string)b.Element("DetailCode")) == dobavlenie1.Text).Remove();

                    doc2.Save("..\\..\\Details.xml");

                    var DETAILS = (from x in doc2.Element("Details").Elements("Detail")
                                   orderby x.Element("DetailCode").Value
                                   select new
                                   {
                                       КодДетали = x.Element("DetailCode").Value,
                                       НазваниеДетали = x.Element("DetailName").Value,
                                       АртикулДетали = x.Element("DetailArticle").Value,
                                       ЦенаДетали = x.Element("DetailPrices").Value,
                                       ПримечаниеДетали = x.Element("DetailNote").Value
                                   }).ToList();
                    var DETAILSCollection = new ObservableCollection<object>(DETAILS);
                    dg.ItemsSource = DETAILSCollection;

                    MessageBox.Show("Данные удалены");
                    yesno = false;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
                dobavlenie1.Clear();
                dobavlenie2.Clear();
                dobavlenie3.Clear();
                dobavlenie4.Clear();
                dobavlenie5.Clear();

            }
            if (counter == 3)
            {
                IEnumerable<XElement> tests =
                from el in doc3.Elements("Supplies").Elements("Supply")
                where (string)el.Element("SupplierCode") == dobavlenie1.Text
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno == true)
                {
                    doc3.Elements("Supplies").Elements("Supply").First(b => ((string)b.Element("SupplierCode")) == dobavlenie1.Text).Remove();

                    doc3.Save("..\\..\\..Supplies.xml");
                    var SUPPLIES = (from x in doc3.Element("Supplies").Elements("Supply")
                                    orderby x.Element("SupplierCode").Value
                                    select new
                                    {
                                        КодПоставщика = x.Element("SupplierCode").Value,
                                        КодДетали = x.Element("DetailCode").Value,
                                        Количество = x.Element("Quantity").Value,
                                        Дата = x.Element("Date").Value
                                    }).ToList();
                    var SUPPLIESCollection = new ObservableCollection<object>(SUPPLIES);
                    dg.ItemsSource = SUPPLIESCollection;

                    MessageBox.Show("Данные удалены");
                    yesno = false;
                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
                dobavlenie1.Clear();
                dobavlenie2.Clear();
                dobavlenie3.Clear();
                dobavlenie4.Clear();
                dobavlenie5.Clear();

            }
        }

        private void buttonpoisk_Click(object sender, RoutedEventArgs e)
        {
            if (counter == 1)
            {
                if (dobavlenie1 != null && dobavlenie3.Text == "" && dobavlenie4.Text == "")
                {
                    var kod = (from x in doc1.Element("Suppliers").Elements("Supplier")
                               where (string)x.Element("SupplierCode") == dobavlenie1.Text
                               select new
                               {
                                   КодПоставщика = x.Element("SupplierCode").Value,
                                   НазваниеПоставщика = x.Element("SupplierName").Value,
                                   АдресПоставщика = x.Element("SupplierAddress").Value,
                                   ТелефонПоставщика = x.Element("SupplierContact").Value,
                               }).ToList();
                    dg.ItemsSource = kod;
                }


                if (dobavlenie1.Text == "" && dobavlenie2.Text == "" && dobavlenie3.Text == "группировать" && dobavlenie4.Text == "")
                {
                    var type = (from x in doc1.Element("Suppliers").Elements("Supplier")
                                group x by x.Element("SupplierCode").Value into g
                                select new
                                {
                                    группа = g.Key
                                }).ToList();
                    dg.ItemsSource = type;
                }
                if (dobavlenie1.Text == "" && dobavlenie3.Text != null && dobavlenie3.Text != "группировать" && dobavlenie4.Text == "")
                {
                    var type = (from x in doc1.Element("Suppliers").Elements("Supplier")
                                where (string)x.Element("SupplierCode") == dobavlenie3.Text
                                group x by x.Element("SupplierCode").Value into g
                                select new
                                {
                                    КодПоставщика = g.Key,
                                    Количество = g.Count()
                                }).ToList();
                    dg.ItemsSource = type;
                }
            }
            if (counter == 2)
            {
                if (dobavlenie1 != null && dobavlenie3.Text == "" && dobavlenie4.Text == "")
                {
                    var kod = (from x in doc2.Element("Details").Elements("Detail")
                               where (string)x.Element("DetailCode") == dobavlenie1.Text
                               select new
                               {
                                   КодДетали = x.Element("DetailCode").Value,
                                   НазваниеДетали = x.Element("DetailName").Value,
                                   АртикулДетали = x.Element("DetailArticle").Value,
                                   ЦенаДетали = x.Element("DetailPrices").Value,
                                   ПримечаниеДетали = x.Element("DetailNote").Value,
                               }).ToList();
                    dg.ItemsSource = kod;
                }
                if (dobavlenie1.Text == "" && dobavlenie3.Text == "группировать")
                {
                    var type = (from x in doc2.Element("Details").Elements("Detail")
                                group x by x.Element("DetailCode").Value into g
                                select new
                                {
                                    группа = g.Key
                                }).ToList();
                    dg.ItemsSource = type;
                }
                if (dobavlenie1.Text == "" && dobavlenie3.Text != null && dobavlenie3.Text != "группировать" && dobavlenie4.Text == "")
                {
                    var type = (from x in doc2.Element("Details").Elements("Detail")
                                where (string)x.Element("DetailCode") == dobavlenie3.Text
                                group x by x.Element("DetailCode").Value into g
                                select new
                                {
                                    КодДетали = g.Key,
                                    Количество = g.Count()
                                }).ToList();
                    dg.ItemsSource = type;
                }
            }
            if (counter == 3)
            {
                if (dobavlenie1 != null && dobavlenie3.Text == "" && dobavlenie4.Text == "")
                {
                    var kod = (from x in doc3.Element("Supplies").Elements("Supply")
                               where (string)x.Element("SupplierCode") == dobavlenie1.Text
                               select new
                               {
                                   КодПоставщика = x.Element("SupplierCode").Value,
                                   КодДетали = x.Element("DetailCode").Value,
                                   Количество = x.Element("Quantity").Value,
                                   Дата = x.Element("Date").Value,
                               }).ToList();
                    dg.ItemsSource = kod;
                }
                if (dobavlenie2 != null && dobavlenie3.Text == "" && dobavlenie4.Text == "")
                {
                    var kod = (from x in doc3.Element("Supplies").Elements("Supply")
                               where (string)x.Element("DetailCode") == dobavlenie2.Text
                               select new
                               {
                                   КодПоставщика = x.Element("SupplierCode").Value,
                                   КодДетали = x.Element("DetailCode").Value,
                                   Количество = x.Element("Quantity").Value,
                                   Дата = x.Element("Date").Value,
                               }).ToList();
                    dg.ItemsSource = kod;
                }
                if (dobavlenie1.Text == "" && dobavlenie3.Text == "группировать")
                {
                    var type = (from x in doc3.Element("Supplies").Elements("Supply")
                                group x by x.Element("SupplierCode").Value into g
                                select new
                                {
                                    группа = g.Key
                                }).ToList();
                    dg.ItemsSource = type;
                }
                if (dobavlenie1.Text == "" && dobavlenie3.Text != null && dobavlenie3.Text != "группировать" && dobavlenie4.Text == "" && dobavlenie2.Text == "")
                {
                    var type = (from x in doc3.Element("Supplies").Elements("Supply")
                                where (string)x.Element("SupplierCode") == dobavlenie3.Text
                                group x by x.Element("SupplierCode").Value into g
                                select new
                                {
                                    КодПоставщика = g.Key,
                                    Количество = g.Count()
                                }).ToList();
                    dg.ItemsSource = type;
                }
                if (dobavlenie1.Text == "" && dobavlenie2.Text == "" && dobavlenie3.Text == "" && dobavlenie4.Text != null)
                {
                    var type = (from x in doc3.Element("Supplies").Elements("Supply")
                                where (string)x.Element("DetailCode") == dobavlenie4.Text
                                group x by x.Element("DetailCode").Value into g
                                select new
                                {
                                    КодДетали = g.Key,
                                    Количество = g.Count()
                                }).ToList();
                    dg.ItemsSource = type;
                }

            }
        }

        private void buttonprice_Click(object sender, RoutedEventArgs e)
        {
            if (dobavlenie4.Text!="" && dobavlenie5.Text!="" && dobavlenie1.Text!="")
            {
                var detailPricesElement = doc2.Elements("Details").Elements("Detail").First(b => ((string)b.Element("DetailCode")) == dobavlenie1.Text).Element("DetailPrices");
                detailPricesElement.Add(new XElement("DetailPrice", new XElement("PricePrice", dobavlenie4.Text), new XElement("PriceDate", dobavlenie5.Text)));
                doc2.Save("..\\..\\Details.xml");

                IEnumerable<XElement> tests =
                from el in doc2.Elements("Details").Elements("Detail")
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno == true)
                {
                    var DETAILS2 = (from x in doc2.Element("Details").Elements("Detail")
                                    orderby x.Element("DetailCode").Value
                                    select new
                                    {
                                        КодДетали = x.Element("DetailCode").Value,
                                        ЦенаДетали = x.Element("DetailPrices").Elements("DetailPrice").LastOrDefault()?.Element("PricePrice").Value,
                                        ДатаЦены = x.Element("DetailPrices").Elements("DetailPrice").LastOrDefault()?.Element("PriceDate").Value
                                    }).ToList();
                    var DETAILS2Collection = new ObservableCollection<object>(DETAILS2);
                    dg.ItemsSource = DETAILS2Collection;

                    MessageBox.Show("Данные обновлены");
                    yesno = false;
                }
                
            }
            if (dobavlenie1.Text != "" && dobavlenie4.Text == "" && dobavlenie5.Text == "")
            {
                dg.Columns.Clear();
                var detailPrices = doc2.Elements("Details").Elements("Detail").First(b => ((string)b.Element("DetailCode")) == dobavlenie1.Text).Elements("DetailPrices");

                IEnumerable<XElement> tests =
                from el in detailPrices.Elements("DetailPrice")
                select el;
                foreach (XElement el in tests)
                    yesno = true;
                if (yesno == true)
                {
                    var DETAILS2 = (from x in detailPrices.Elements("DetailPrice")
                                    orderby x.Element("PricePrice").Value
                                    select new
                                    {
                                        ЦенаДетали = x.Element("PricePrice").Value,
                                        ДатаЦены = x.Element("PriceDate").Value
                                    }).ToList();
                    var DETAILS2Collection = new ObservableCollection<object>(DETAILS2);
                    dg.ItemsSource = DETAILS2Collection;

                    MessageBox.Show("Данные найдены");
                    yesno = false;
                }
            }

            if (dobavlenie1.Text == "")
            {
                MessageBox.Show("Ошибка");
            }
            dobavlenie1.Clear();
            dobavlenie2.Clear();
            dobavlenie3.Clear();
            dobavlenie4.Clear();
            dobavlenie5.Clear();

        }
    }
}
