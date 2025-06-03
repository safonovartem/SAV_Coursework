using System.Net;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace SAV_Kurasch_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeChart();
            LoadCurrencyData();
        }
        private void InitializeChart()
        {
            // Настройка внешнего вида графика
            chart1.Titles.Add("Курсы валют ЦБ РФ");
            chart1.ChartAreas[0].AxisX.Title = "Валюта";
            chart1.ChartAreas[0].AxisY.Title = "Курс (руб)";
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
        }

        private void LoadCurrencyData()
        {
            try
            {
                // Получаем данные с сайта ЦБ РФ
                var url = "https://www.cbr.ru/scripts/XML_daily.asp";
                var webClient = new WebClient();
                var xml = webClient.DownloadString(url);

                // Парсим XML
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);

                // Собираем данные по валютам
                var currencies = new Dictionary<string, decimal>();
                var nodes = xmlDoc.SelectNodes("//Valute");

                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        var charCode = node.SelectSingleNode("CharCode")?.InnerText;
                        var value = node.SelectSingleNode("Value")?.InnerText;
                        var nominal = node.SelectSingleNode("Nominal")?.InnerText;

                        if (charCode != null && value != null && nominal != null)
                        {
                            // Преобразуем значение в decimal (с учетом разделителя дробной части)
                            var decimalValue = decimal.Parse(value);
                            var decimalNominal = decimal.Parse(nominal);
                            currencies.Add(charCode, decimalValue / decimalNominal);
                        }
                    }
                }

                // Отображаем данные на графике
                DisplayChartData(currencies);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayChartData(Dictionary<string, decimal> currencies)
        {
            // Очищаем предыдущие данные
            chart1.Series.Clear();

            // Создаем новую серию данных
            var series = new Series("Курсы валют")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                LabelFormat = "N2"
            };

            // Добавляем данные в серию
            foreach (var currency in currencies)
            {
                series.Points.AddXY(currency.Key, currency.Value);
            }

            // Добавляем серию на график
            chart1.Series.Add(series);

            // Настраиваем внешний вид точек данных
            foreach (DataPoint point in series.Points)
            {
                point.Label = point.YValues[0].ToString("N2");
                point.Font = new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold);
                point.LabelForeColor = System.Drawing.Color.DarkBlue;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadCurrencyData();
        }

        //[STAThread]
        //static void Main()
        //{
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.Run(new Form1());
        //}
    

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
